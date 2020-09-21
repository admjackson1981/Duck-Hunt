using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;


public class Bullet_Test : MonoBehaviour
{
    public bool canTrigger;
    public bool triggerButton;
    public AudioClip emptyShot;
    public AudioClip reload;
    public List<UnityEngine.XR.InputDevice> leftHandControllers = new List<UnityEngine.XR.InputDevice>();

    public int gunShots = 2;
    private bool primaryButton;
    private bool onTarget = false;
    private RaycastHit hit;
    private AudioSource audiosource;
    private bool canReload = false;
    private bool gunInHand = false;
    private GameObject[] bulletImage;
  
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        var desiredCharaterisitcs = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharaterisitcs, leftHandControllers);
        bulletImage = GameObject.FindGameObjectsWithTag("Bullet");
        Debug.Log(bulletImage.Length);
      
    }

    // Update is called once per frame
    void Update()
    {
       
        leftHandControllers[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerButton);
        leftHandControllers[0].TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton);

        // mechanics, for stoppging repeated re-load
        if(gunInHand && !primaryButton)
        {
            canReload = true;
        }

        //this mechanism stops rapud firing based on holding down the trigger. 
        if (gunInHand && !triggerButton)
        {
            canTrigger = true;
        }

        if (gunInHand && primaryButton && canReload)
        {
            Reload();

        }

       
        if(gunInHand && triggerButton && gunShots == 0 && canTrigger )
             {
           
            audiosource.PlayOneShot(emptyShot);
          
            canTrigger = false;
             }

        if (gunInHand && triggerButton && gunShots > 0 && canTrigger)
        {
            
            audiosource.Play();
            gunShots -= 1;
            bulletImage[gunShots].GetComponent<Image>().enabled = false;

          
        }



    }
    void Reload()
    {
        audiosource.PlayOneShot(reload);
        gunShots = 2;
        canReload = false;
        bulletImage[0].GetComponent<Image>().enabled = true;
        bulletImage[1].GetComponent<Image>().enabled = true;

    }
    void FixedUpdate()
    {
            
            int layerMask = 1 << 8;
       
        
            if (Physics.Raycast(transform.position, transform.forward, out hit,Mathf.Infinity, layerMask))
            {
                onTarget = true;
             
                if (gunInHand && triggerButton && canTrigger)
                {
                    canTrigger = false;
                 
                    DestroyDuck();
                   

                }
            }
        else
          {
            onTarget = false;
          }
            
        
    }
    void DestroyDuck()
    {   
        if(onTarget)
        {

           
            Rigidbody Remove = hit.transform.GetComponent<Rigidbody>();
            Remove.useGravity = true;
            Remove.isKinematic = false;
            Destroy(hit.transform.GetComponent<Duck>());
            hit.rigidbody.AddForce(-hit.normal * 50);
            


        }
          
     
        
    }
  
    public void poo(bool d)
    {
        canTrigger = d;
    }
    public void holdingGun(bool holding)
    {
        gunInHand = holding;
    }
   
}

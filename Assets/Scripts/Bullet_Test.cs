using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


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
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        var desiredCharaterisitcs = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharaterisitcs, leftHandControllers);
       
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

        if (primaryButton && canReload)
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
           

        }



    }
    void Reload()
    {
        audiosource.PlayOneShot(reload);
        gunShots = 2;
        canReload = false;

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

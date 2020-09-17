using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Bullet_Test : MonoBehaviour
{
    public bool canTrigger;
    public bool triggerButton;
   

    private bool onTarget = false;
    private RaycastHit hit;
     AudioSource audiosource;
     public List<UnityEngine.XR.InputDevice> leftHandControllers = new List<UnityEngine.XR.InputDevice>();
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
      

        if (triggerButton)
        {
            audiosource.Play();
        }

     
       
    }
    void FixedUpdate()
    {
            
          int layerMask = 1 << 8;
        // Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        
            if (Physics.Raycast(transform.position, transform.forward, out hit,Mathf.Infinity, layerMask))
        {
            onTarget = true;
         

            if (canTrigger && triggerButton)
            {

                // StartCoroutine("ShotEffect");
                DestroyDuck();

            }
        }
        onTarget = false;
        
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
   
}

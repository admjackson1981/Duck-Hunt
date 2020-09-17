using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move oject uncrease x... sped vairable.
         transform.Translate(-Vector3.right * 3.5f * Time.deltaTime);

        if(transform.position.x >= 15.0f)
        {
            Destroy(gameObject);
        }
    }
}

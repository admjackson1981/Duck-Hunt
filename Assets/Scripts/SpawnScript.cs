using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject prefab_Brid;
    private float spawnTime= 2.0f;
    private gameManager gm;
    // Start is called before the first frame update
    void Start()
    {
       gm  = FindObjectOfType<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {

      

       

    }
    void FixedUpdate()
    {
       // if(gm.Level == 1)
       // {
       //     spawnTime = Random.Range(2.0f, 5.0f);
       // }
       //
          spawnTime -= Time.deltaTime;

        if(spawnTime <=0.0f)
        {
             Instantiate(prefab_Brid);
              spawnTime = 2.0f;
        }
    }
}

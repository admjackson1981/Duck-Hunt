using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject prefab_Brid;
    private float spawnTime= 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        spawnTime -= Time.deltaTime;

        if(spawnTime <=0.0f)
        {
             Instantiate(prefab_Brid);
              spawnTime = 2.0f;
        }

        Debug.Log(spawnTime);

    }
}

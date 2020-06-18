using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GameManage.addSpawn(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

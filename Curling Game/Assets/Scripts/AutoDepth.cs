using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDepth : MonoBehaviour
{
    public float offSet = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.y+ offSet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

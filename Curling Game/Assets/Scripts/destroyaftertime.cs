using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyaftertime : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        StartCoroutine(dest());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator dest() {
        yield return new WaitForSeconds(30f);
        GameObject.Destroy(gameObject);
    }
}

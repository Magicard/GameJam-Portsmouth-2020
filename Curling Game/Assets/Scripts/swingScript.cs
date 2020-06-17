using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swingScript : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject inCollision;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        inCollision = collision.gameObject;
        Debug.Log(inCollision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        inCollision = null;
        Debug.Log(inCollision);

    }
}

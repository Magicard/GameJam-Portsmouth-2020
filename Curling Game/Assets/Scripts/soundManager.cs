using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public GameObject player;
    public AudioSource sweep;
    public AudioSource shoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (!sweep.isPlaying)
            {
                sweep.Play();

            }
           
        }
        if(Input.GetMouseButtonDown(0))
        {
            shoot.Play();
            if (gameObject.GetComponent<PlayerController>().hasBall == true)
            {
                Debug.Log("shouldplay");
                
            }
        }
    }
}

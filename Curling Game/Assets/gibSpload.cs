using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gibSpload : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> gibs = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sprite spr in gibs) {
            
            GameObject o = new GameObject();
            o.transform.position = gameObject.transform.position;
            o.AddComponent<SpriteRenderer>();
            o.GetComponent<SpriteRenderer>().sprite = spr;

            o.AddComponent<Rigidbody2D>();
            float rx = UnityEngine.Random.Range(-5,5);
            float ry = UnityEngine.Random.Range(-5,5);
            //Vector2 force = new Vector2(0.2f, UnityEngine.Random.Range(0, 360));
            o.GetComponent<Rigidbody2D>().gravityScale = 0;
            o.GetComponent<Rigidbody2D>().drag = 2f;
            o.GetComponent<Rigidbody2D>().AddForce(new Vector3(UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360),0) *0.02f, ForceMode2D.Impulse);
            

            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

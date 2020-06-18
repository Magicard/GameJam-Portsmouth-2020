﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facingScript : MonoBehaviour
{
    public Camera cam;
    public Animator anim;
    public GameObject broom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_pos;
        Transform target = gameObject.transform; //Assign to the object you want to rotate
        Vector3 object_pos;
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = cam.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        
        if (angle>= 0 && angle<= 90)
        {
            //TopRightBehind
            anim.Play("Idle");
            broom.transform.position = new Vector3(broom.transform.position.x, broom.transform.position.y, transform.position.z - 1f);
            if (Input.GetMouseButton(1))
            {
                broom.GetComponent<Animator>().Play("swipe");
            }
            else
            {
                broom.GetComponent<Animator>().Play("nofr");
            }

        }
        else if(angle>=91 && angle<=180)
        {
            broom.transform.position = new Vector3(broom.transform.position.x, broom.transform.position.y, transform.position.z - 1f);
                //TopLeftBehind
                anim.Play("IdleBl");
            if (Input.GetMouseButton(1))
            {
                broom.GetComponent<Animator>().Play("swipefl");
            }
            else
            {
                broom.GetComponent<Animator>().Play("no");
            }

        }
        else if (angle <= 1 && angle >= -90)
        {
            //BottomRightFwrd
            anim.Play("IdleFwrd");
            if(Input.GetMouseButton(1))
            {
                broom.GetComponent<Animator>().Play("swipe");
            }
            else
            {
                broom.GetComponent<Animator>().Play("nofr");
            }

        }
            
        else if (angle <= -90 && angle >= -180)
        {
            //BottomLeftFwrd
            anim.Play("IdleFl");
            if (Input.GetMouseButton(1))
            {
                broom.GetComponent<Animator>().Play("swipefl");
            }
            else 
            {
                broom.GetComponent<Animator>().Play("no");
            }
        }
    }
}

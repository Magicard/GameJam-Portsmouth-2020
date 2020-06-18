using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facingScript : MonoBehaviour
{
    public Camera cam;
    public Animator anim;
    public GameObject broom;
    public int spriteCounter = characterSelect.spriteIndex2;
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
            switch(characterSelect.spriteIndex2)
            {
                case 0:
                    anim.Play("Idle1");
                    break;
                case 1:
                    anim.Play("Idle2");
                    break;
                case 2:
                    anim.Play("Idle3");
                    break;
                case 3:
                    anim.Play("Idle4");
                    break;
                case 4:
                    anim.Play("Idle5");
                    break;
                case 5:
                    anim.Play("Idle6");
                    break;
            }
            
            broom.transform.position = new Vector3(broom.transform.position.x, broom.transform.position.y, transform.position.z + 8f);
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
            broom.transform.position = new Vector3(broom.transform.position.x, broom.transform.position.y, transform.position.z + 8f);
            //TopLeftBehind
            switch (characterSelect.spriteIndex2)
            {
                case 0:
                    anim.Play("IdleBl1");
                    break;
                case 1:
                    anim.Play("IdleBl2");
                    break;
                case 2:
                    anim.Play("IdleBl3");
                    break;
                case 3:
                    anim.Play("IdleBl4");
                    break;
                case 4:
                    anim.Play("IdleBl5");
                    break;
                case 5:
                    anim.Play("IdleBl6");
                    break;
            }
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
            broom.transform.position = new Vector3(broom.transform.position.x, broom.transform.position.y, transform.position.z -1f);
            //BottomRightFwrd
            switch (characterSelect.spriteIndex2)
            {
                case 0:
                    anim.Play("IdleFl1");
                    break;
                case 1:
                    anim.Play("IdleFl2");
                    break;
                case 2:
                    anim.Play("IdleFl3");
                    break;
                case 3:
                    anim.Play("IdleFl4");
                    break;
                case 4:
                    anim.Play("IdleFl5");
                    break;
                case 5:
                    anim.Play("IdleFl16");
                    break;
            }
            if (Input.GetMouseButton(1))
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
            broom.transform.position = new Vector3(broom.transform.position.x, broom.transform.position.y, transform.position.z - 1f);
            //BottomLeftFwrd
            switch (characterSelect.spriteIndex2)
            {
                case 0:
                    anim.Play("IdleFwrd1");
                    break;
                case 1:
                    anim.Play("IdleFwrd2");
                    break;
                case 2:
                    anim.Play("IdleFwrd3");
                    break;
                case 3:
                    anim.Play("IdleFwrd4");
                    break;
                case 4:
                    anim.Play("IdleFwrd5");
                    break;
                case 5:
                    anim.Play("IdleFwrd6");
                    break;
            }
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

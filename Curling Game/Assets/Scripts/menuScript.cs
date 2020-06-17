using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public GameObject start;
    public GameObject controls;
    public GameObject settings;
    public GameObject exit;

    public Camera cam;

    public AudioSource sound;

    public int counter=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown("s"))
        {
            sound.Play();
            counter++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("w"))
        {
            sound.Play();
            counter--;
        }

        if (counter>= 4)
        {
            counter = 0;
        }
        else if (counter<= -1)
        {
            counter = 3;
        }

        switch (counter)
        {
            case 0:
                start.GetComponent<Animator>().Play("button");
                controls.GetComponent<Animator>().Play("no");
                settings.GetComponent<Animator>().Play("no");
                exit.GetComponent<Animator>().Play("no");
                break;
            case 1:
                controls.GetComponent<Animator>().Play("button");
                start.GetComponent<Animator>().Play("no");
                settings.GetComponent<Animator>().Play("no");
                exit.GetComponent<Animator>().Play("no");
                break;
            case 2:
                settings.GetComponent<Animator>().Play("button");
                controls.GetComponent<Animator>().Play("no");
                start.GetComponent<Animator>().Play("no");
                exit.GetComponent<Animator>().Play("no");
                break;
            case 3:
                exit.GetComponent<Animator>().Play("button");
                controls.GetComponent<Animator>().Play("no");
                settings.GetComponent<Animator>().Play("no");
                start.GetComponent<Animator>().Play("no");
                break;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider!= null)
        {
            sound.Play();
            if (hit.collider.gameObject.name == "Start")
            {
                counter = 0;
            }
            else if (hit.collider.gameObject.name == "Controlls")
            {
                counter = 1;
            }
            else if (hit.collider.gameObject.name == "Settings")
            {
                counter = 2;
            }
            else if (hit.collider.gameObject.name == "exit")
            {
                counter = 3;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0))
        {
            switch (counter)
            {
                case 0:
                    SceneManager.LoadScene(1);
                    break;
                case 1:
                    SceneManager.LoadScene(2);
                    break;
                case 2:
                    SceneManager.LoadScene(3);
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
            
        }

    }
}

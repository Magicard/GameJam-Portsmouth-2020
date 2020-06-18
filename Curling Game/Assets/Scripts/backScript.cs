using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backScript : MonoBehaviour
{
    public GameObject back;

    public Camera cam;

    public AudioSource sound;

    public int counter = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        switch (counter)
        {
            case 0:
                back.GetComponent<Animator>().Play("button");
                break;
            case 1:
                back.GetComponent<Animator>().Play("no");
                break;

        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            sound.Play();
            if (hit.collider.gameObject.name == "Back")
            {
                counter = 0;
            }
            else
            {
                counter = 1;
            }

        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0))
        {
            switch (counter)
            {
                case 0:
                    SceneManager.LoadScene(0);
                    break;
            }

        }

    }
}

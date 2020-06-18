using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterSelect : MonoBehaviour
{
    public GameObject rArrow;
    public GameObject lArrow;
    public GameObject goButton;

    public List<Sprite> characters;

    public GameObject playerCharacter;

    public int spriteIndex;
    public int counter= 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.collider.gameObject.name == "arrow_left")
            {
                counter = 0;
            }
            else if (hit.collider.gameObject.name == "arrow_right")
            {
                counter = 1;
            }
            else if (hit.collider.gameObject.name == "go")
            {
                counter = 2;
            }
            
        }
        else
        {
            counter = 3;
        }

        switch (counter)
        {
            case 0:
                lArrow.GetComponent<Animator>().Play("button");
                rArrow.GetComponent<Animator>().Play("no");
                goButton.GetComponent<Animator>().Play("no");
                break;
            case 1:
                rArrow.GetComponent<Animator>().Play("button");
                lArrow.GetComponent<Animator>().Play("no");
                goButton.GetComponent<Animator>().Play("no");
                break;
            case 2:
                lArrow.GetComponent<Animator>().Play("no");
                rArrow.GetComponent<Animator>().Play("no");
                goButton.GetComponent<Animator>().Play("button");
                break;
            case 3:
                lArrow.GetComponent<Animator>().Play("no");
                rArrow.GetComponent<Animator>().Play("no");
                goButton.GetComponent<Animator>().Play("no");
                break;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            switch (counter)
            {
                case 0:
                    spriteIndex--;
                    break;
                case 1:
                    spriteIndex++;
                    break;
                case 2:
                    SceneManager.LoadScene(1);
                    break;

            }

        }

        if (spriteIndex >= 6)
        {
            spriteIndex = 0;
        }
        else if (spriteIndex <= -1)
        {
            spriteIndex = 5;
        }

        playerCharacter.GetComponent<SpriteRenderer>().sprite = characters[spriteIndex];
    }
}

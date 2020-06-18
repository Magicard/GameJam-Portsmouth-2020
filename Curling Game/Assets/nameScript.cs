using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameScript : MonoBehaviour
{

    public static string name;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Text txt_Input = GameObject.Find("pee").GetComponent<Text>();

        string ObjectsText = txt_Input.text;
        Debug.Log(ObjectsText);
        name = ObjectsText;
    }
}

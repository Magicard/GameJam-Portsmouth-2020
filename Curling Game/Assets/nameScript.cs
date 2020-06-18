using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameScript : MonoBehaviour
{

    public static string nam;
    public Text txt_Input;
    // Start is called before the first frame update
    void Start()
    {
        //txt_Input = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        

        string ObjectsText = txt_Input.text;
        Debug.Log(ObjectsText);
        nam = ObjectsText;
        Debug.Log(nam);
    }
}

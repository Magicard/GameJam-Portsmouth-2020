using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class leaderboardManager : MonoBehaviour
{
    [SerializeField]
    public List<Text> textList = new List<Text>(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //for(int i = 0; i < GameManage.players.Count; i++) {
        //    textList[i].text = GameManage.players.ElementAt(i).Value.IDName + " " + GameManage.players.ElementAt(i).Value.kills.ToString();
       // }
    }
}

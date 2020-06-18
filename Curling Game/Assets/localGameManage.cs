using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localGameManage : MonoBehaviour
{
    public static Dictionary<string, PlayerController> players = new Dictionary<string, PlayerController>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void addPlayer(string _id, PlayerController _player) {
        players.Add(_id, _player);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.UI;

public class GameManage : NetworkBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    [SerializeField]
    public static Dictionary<string,PlayerController> players = new Dictionary<string, PlayerController>();
    public static  List<Transform> spawnPoints = new List<Transform>();


    // Start is called before the first frame update
    public override void OnStartServer() {
        base.OnStartServer();
        //StartCoroutine(spawnObj());
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer) {
            foreach (KeyValuePair<string, PlayerController> kv in players) {
                List<Text> tx = kv.Value.leaderBoard.GetComponent<leaderboardManager>().textList;
                //leaderboardManager lb = kv.Value.leaderBoard.GetComponent<leaderboardManager>();
                for (int i = 0; i < players.Count; i++) {
                   // Text t = new Text();
                   // tx.Add()
                    tx[i].text = players.ElementAt(i).Value.IDName + " " + players.ElementAt(i).Value.kills.ToString();

                }
                kv.Value.RpcSetBoard(tx);

            }
        }
            
        
    }

    void resetSpawns() {
        foreach (KeyValuePair<string, PlayerController> kv in players) {
            kv.Value.RpcResetSpawn(new Vector3(0,0,0));
        }
    }

    public static void addPlayer(string _id, PlayerController _player) {
        players.Add(_id, _player);
    }

    public static void addSpawn(Transform trans) {
        spawnPoints.Add(trans);
    }

    IEnumerator spawnObj() {
        yield return new WaitForSeconds(2f);
        GameObject ball = Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(ball);
        StartCoroutine(spawnObj());
        //CmdSpawnObj();
    }

    

    [Command]
    void CmdSpawnObj() {

        StartCoroutine(spawnObj());
    }

    

    

   
}

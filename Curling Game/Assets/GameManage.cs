using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManage : NetworkBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public static Dictionary<string,PlayerController> players = new Dictionary<string, PlayerController>();

    // Start is called before the first frame update
    public override void OnStartServer() {
        base.OnStartServer();
        //StartCoroutine(spawnObj());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void addPlayer(string _id, PlayerController _player) {
        players.Add(_id, _player);
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

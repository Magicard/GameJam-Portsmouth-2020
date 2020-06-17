using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManage : NetworkBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    public override void OnStartServer() {
        base.OnStartServer();
        //StartCoroutine(spawnObj());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnObj() {
        yield return new WaitForSeconds(2f);
        GameObject ball = Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(ball);
        StartCoroutine(spawnObj());
        //CmdSpawnObj();
    }

    public void respawn(NetworkConnection conn) {

        CmdRespawn(conn);
    }

    [Command]
    void CmdSpawnObj() {

        StartCoroutine(spawnObj());
    }

    void CmdRespawn(NetworkConnection conn) {
        
            GameObject player = Instantiate(playerPrefab, new Vector3(-2, -2, 0), Quaternion.identity);
            player.GetComponent<PlayerController>().manager = gameObject.GetComponent<GameManage>();
            NetworkServer.ReplacePlayerForConnection(conn, player);
        
    }

   
}

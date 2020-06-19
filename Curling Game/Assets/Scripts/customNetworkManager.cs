using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class customNetworkManager : NetworkManager
{
    public List<Sprite> yes;
    public List<GameObject> players;
    public GameManage GM;
    public override void OnServerAddPlayer(NetworkConnection conn) {
        Vector3 spawnPos = GameManage.spawnPoints[Random.Range(0,GameManage.spawnPoints.Count -1 )].position;
        playerPrefab = players[0];
       
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        //player.GetComponent<PlayerController>().customName = nameScript.nam;
        //player.GetComponent<PlayerController>().manager = GM;
        //characterSelect nameList = list.GetComponent<characterSelect>();
        //player.GetComponent<SpriteRenderer>().sprite = yes[spriteCounter];
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(NetworkConnection conn) {
        base.OnServerDisconnect(conn);
        
    }

    public override void OnClientDisconnect(NetworkConnection conn) {
        base.OnClientDisconnect(conn);
    }
}

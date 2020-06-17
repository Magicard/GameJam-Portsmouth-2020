using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class customNetworkManager : NetworkManager
{
    public GameManage GM;
    public override void OnServerAddPlayer(NetworkConnection conn) {
        Vector3 spawnPos = GameManage.spawnPoints[Random.Range(0,GameManage.spawnPoints.Count -1 )].position;
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        //player.GetComponent<PlayerController>().manager = GM;
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}

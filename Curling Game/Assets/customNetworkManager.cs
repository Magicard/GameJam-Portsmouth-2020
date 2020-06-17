using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class customNetworkManager : NetworkManager
{
    public GameManage GM;
    public override void OnServerAddPlayer(NetworkConnection conn) {
        GameObject player = Instantiate(playerPrefab, new Vector3(2, 2, 0), Quaternion.identity);
        player.GetComponent<PlayerController>().manager = GM;
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}

using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curlingStone : NetworkBehaviour {
    [SyncVar]
    public GameObject owner;
    [SyncVar]
    public Boolean isShot = false;

    // Start is called before the first frame update

    void Start() {
        if (isServer) {
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if(GetComponent<Rigidbody2D>().velocity.magnitude < 2f) {
            CmdNotShot();
        }
    }

    [Command]
    void CmdSetPos() {
        
    }
    
    void CmdNotShot() {
        isShot = false;
    }

    [ClientRpc]
    void RpcSetPos() {
        
    }
}

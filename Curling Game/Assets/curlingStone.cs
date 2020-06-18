﻿using Mirror;
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
        if (isShot) {
            if (GetComponent<Rigidbody2D>().velocity.magnitude < 2f) {
               CmdNotShot();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (isServer) {
            if (isShot && collision.gameObject.tag == "playerobj") {
                Debug.Log("kill" + collision.gameObject.name.ToString());

                collision.gameObject.GetComponent<PlayerController>().isDead = true;
                collision.gameObject.GetComponent<PlayerController>().hasBall = false;

                collision.gameObject.GetComponent<PlayerController>().swingStone.GetComponent<SpriteRenderer>().enabled = false;
                collision.gameObject.GetComponent<PlayerController>().gameObject.GetComponent<SpriteRenderer>().enabled = false;
                collision.gameObject.GetComponent<PlayerController>().broom.GetComponent<SpriteRenderer>().enabled = false;

                GameManage.players[collision.gameObject.GetComponent<PlayerController>().IDName].RpcDie();
                GameManage.players[owner.GetComponent<PlayerController>().IDName].kills++;

            }
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

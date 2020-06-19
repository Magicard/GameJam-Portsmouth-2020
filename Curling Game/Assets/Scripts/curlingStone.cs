using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curlingStone : NetworkBehaviour {
    public GameObject stonePrefabref;

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
            if(collision.gameObject.tag == "playerobj") {
                if (isShot && collision.gameObject.GetComponent<PlayerController>().isDead == false) {

                    if (collision.gameObject.GetComponent<PlayerController>().hasBall) {
                        GameObject b = Instantiate(stonePrefabref.GetComponent<stoneRefScript>().spawnRef, collision.gameObject.transform.position, Quaternion.identity);
                        b.GetComponent<curlingStone>().isShot = false;
                        b.GetComponent<curlingStone>().owner = null;
                        NetworkServer.Spawn(b);
                    }

                        Debug.Log("kill" + collision.gameObject.name.ToString());
                    GameManage.spawnGibs(collision.gameObject.transform.position);
                    collision.gameObject.GetComponent<PlayerController>().isDead = true;
                    collision.gameObject.GetComponent<PlayerController>().hasBall = false;
                    //collision.gameObject.transform.position = new Vector3(-2, -2, 0); //hmm
                    collision.gameObject.GetComponent<PlayerController>().swingStone.GetComponent<SpriteRenderer>().enabled = false;
                    collision.gameObject.GetComponent<PlayerController>().gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    collision.gameObject.GetComponent<PlayerController>().broom.GetComponent<SpriteRenderer>().enabled = false;

                    

                    GameManage.players[collision.gameObject.GetComponent<PlayerController>().IDName].RpcDie();
                    GameManage.players[owner.GetComponent<PlayerController>().IDName].kills++;
                    //GameManage.players[owner.GetComponent<PlayerController>().IDName].gameObject.transform.position = new Vector3(0,0,0);
                    GameManage.sendboard();

                }

                if (!collision.gameObject.GetComponent<PlayerController>().hasBall && !collision.gameObject.GetComponent<PlayerController>().isDead && collision.gameObject.tag == "playerobj" && !isShot) {
                    collision.gameObject.GetComponent<PlayerController>().hasBall = true;
                    collision.gameObject.GetComponent<PlayerController>().RpcSetStone();
                    collision.gameObject.GetComponent<PlayerController>().RpcSetStoneVisible(true);
                    NetworkServer.Destroy(gameObject);
                }
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

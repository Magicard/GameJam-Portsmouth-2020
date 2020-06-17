using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : NetworkBehaviour {
    public GameObject selfPrefab;
    public GameObject swinger;
    public GameObject swingStone;
    public Camera cam;
    public GameObject ballPrefab;
    public GameManage manager;
    public float weight = 5f;
    public float vspd = 0;
    public float hspd = 0;
    public float vel = 0;
    float maxVel = 7f;
    float sin = 0;
    float cos = 0;

    [SyncVar]
    public Boolean hasBall = false;
    [SyncVar]
    public Boolean isDead = false;


    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        selfPrefab = gameObject;
        swingStone.GetComponent<SpriteRenderer>().enabled = hasBall;

    }

    // Update is called once per frame
    private void Update() {

        

        if (isLocalPlayer) {
            
            Vector3 mouse_pos;
            Transform target = gameObject.transform; //Assign to the object you want to rotate
            Vector3 object_pos;
            float angle;
            
            mouse_pos = Input.mousePosition;
            mouse_pos.z = 5.23f; //The distance between the camera and object
            object_pos = cam.WorldToScreenPoint(target.position);
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            swinger.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            //swinger.transform.position = gameObject.transform.position;
            float d = (float)(angle * Math.PI) / 180;
            float asin = Mathf.Sin(d);
            float acos = Mathf.Cos(d);
            swinger.transform.position = new Vector3(transform.position.x + (acos * 1f), transform.position.y + (asin * 1f ), transform.position.z);
            swingStone.transform.position = new Vector3(transform.position.x + (acos * 1f), transform.position.y + (asin * 1f), transform.position.z);
            swingStone.GetComponent<SpriteRenderer>().enabled = hasBall;
            CmdSetStoneVisible(hasBall);




            //swinger.transform.RotateAround(transform.position, swinger.transform.right, angle);




            float spd = 10f;

            if (Input.GetMouseButton(1)) {

                float sinDiff = 0;
                float cosDiff = 0;

                if (sin !=0 && cos != 0) {
                     sinDiff =Mathf.Abs(sin) - Mathf.Abs(Mathf.Sin(d));
                     cosDiff =Mathf.Abs(cos) - Mathf.Abs(Mathf.Cos(d));
                }
                float diff = sinDiff + cosDiff;

                sin = Mathf.Sin(d);
                cos = Mathf.Cos(d);

                vel += 0.05f;
                vel -= (diff*17);
                vel = (vel > maxVel) ? maxVel : vel;



                



            } else {
                vel -= weight;
                vel = (vel < 0) ? 0 : vel;

                if (vel == 0) {
                    hspd = 0;
                    vspd = 0;
                }
            }
            hspd = (vel * cos);
            vspd = (vel * sin);





            //vspd -= weight;
            //hspd -= weight;

            //hspd = (hspd < 0) ? 0 : hspd;
            //vspd = (vspd < 0) ? 0 : vspd;




            Vector3 currentPos = transform.position;
            Vector3 newPos = new Vector3(currentPos.x + (hspd * Time.deltaTime), currentPos.y + (vspd * Time.deltaTime), currentPos.y+ 0.5f);
            transform.position = newPos;

             

        
            
        } else {
            gameObject.GetComponent<PlayerController>().enabled = false;
            cam.enabled = false;
        }

        if (Input.GetMouseButtonDown(0)) {

            if (hasBall) {
                Vector3 mouse_pos;
                Transform target = gameObject.transform; //Assign to the object you want to rotate
                Vector3 object_pos;
                float angle;

                mouse_pos = Input.mousePosition;
                mouse_pos.z = 5.23f; //The distance between the camera and object
                object_pos = cam.WorldToScreenPoint(target.position);
                mouse_pos.x = mouse_pos.x - object_pos.x;
                mouse_pos.y = mouse_pos.y - object_pos.y;
                angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

                CmdYeet(angle);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "curlingStone" && !isDead) {
            Debug.Log("should pick up stone");

            if (collision.gameObject.GetComponent<curlingStone>().isShot) {

                StartCoroutine(respawn());
                    CmdDie();
                    return;
                
                
               
            }
            
                CmdSetStone();
                //hasBall = true;
                CmdDestroyBall(collision.gameObject);
            
            

        }
        
    }
    [Client]
    IEnumerator respawn() {
        Debug.Log("Timer Started");
        yield return new WaitForSeconds(4f);

        CmdRespawn();
    }

    
    void CmdRespawn() {
        swingStone.GetComponent<SpriteRenderer>().enabled = hasBall;
        Debug.Log("RESPAWN");
        NetworkConnection refe = gameObject.GetComponent<NetworkBehaviour>().connectionToClient;
        
        NetworkServer.Destroy(gameObject);
        GameObject player = Instantiate(selfPrefab, new Vector3(-2, -2, 0), Quaternion.identity);
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerController>().cam.enabled = true;
        player.GetComponent<PlayerController>().isDead = false;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<NetworkTransform>().enabled = true;
        player.GetComponent<CircleCollider2D>().enabled = true;

        //player.GetComponent<PlayerController>().manager = gameObject.GetComponent<GameManage>();
        NetworkServer.ReplacePlayerForConnection(refe, player,true);
    }
    [Command]
    void CmdYeet(float dir) {
        hasBall = false;
        swingStone.GetComponent<SpriteRenderer>().enabled = hasBall;

        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        curlingStone stone = ball.GetComponent<curlingStone>();
        
        ball.transform.rotation = Quaternion.Euler(new Vector3(0, 0, dir));
        ball.transform.position += ball.transform.right * 2f;
        ball.GetComponent<Rigidbody2D>().velocity = ball.transform.right * 20f;


        stone.owner = gameObject;
        stone.isShot = true;


        NetworkServer.Spawn(ball);

        //RpcYeet();
    }
  
    void CmdDie() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        isDead = true;

        RpcDie();

        //GameObject.Destroy(gameObject);
        //manager.respawn(gameObject.GetComponent<NetworkBehaviour>().connectionToClient);
        //

    }
    
    void CmdSetStone() {

        hasBall = true;
        

    }
    
    void CmdDestroyBall(GameObject ball) {
        //GameObject.Destroy(ball);
        NetworkServer.Destroy(ball);

    }
    [Command]
    void CmdSetStoneVisible(bool vis) {
        //bool b = hasBall;
        swingStone.GetComponent<SpriteRenderer>().enabled = vis;
        RpcSetStoneVisible(vis);
    }

    [ClientRpc]
    void RpcDie() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    [ClientRpc]
    void RpcSetStoneVisible(bool visible) {
        if (isLocalPlayer) {
            return;
        }
        swingStone.GetComponent<SpriteRenderer>().enabled = visible;
    }


}

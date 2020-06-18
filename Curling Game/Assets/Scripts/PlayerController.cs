using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {
    public Text killsDisplay;
    public Text wintxt;
    public Text nameDisplay;
    public GameObject broom;
    public Canvas canvasRef;
    public GameObject globalCanvas;
    public GameObject selfPrefab;
    public GameObject swinger;
    public GameObject swingStone;
    public Camera cam;
    public GameObject ballPrefab;
    public GameManage manager;
    public GameObject _gibSpload;
    public leaderboardManager leaderBoard;
    public float weight = 5f;
    public float vspd = 0;
    public float hspd = 0;
    public float vel = 0;
    float maxVel = 7f;
    float sin = 0;
    float cos = 0;
    public string myNetId;
    public string customName = "thisiscustomname";

    [SyncVar]
    public Boolean hasBall = false;
    [SyncVar]
    public Boolean isDead = false;
    [SyncVar]
    public int kills = 0;
    [SyncVar]
    public string IDName;
   
    



    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        nameDisplay.text = name;

        rb = gameObject.GetComponent<Rigidbody2D>();
       
        selfPrefab = gameObject;
        swingStone.GetComponent<SpriteRenderer>().enabled = hasBall;

    }
    public override void OnStartClient() {
        base.OnStartClient();
        string _id = gameObject.GetComponent<NetworkIdentity>().netId.ToString();
        name = "player" + _id;
        myNetId = name;
        IDName = "player" + _id;
    }

    public override void OnStartServer() {
        base.OnStartServer();
        string _id = gameObject.GetComponent<NetworkIdentity>().netId.ToString();
        name = "player" + _id;
        myNetId = name;
        IDName = "player" + _id;
        //CmdSetIDName(IDName);

        GameManage.addPlayer(IDName, this);
    }
    // Update is called once per frame
    private void Update() {


        
        if (isLocalPlayer) {
            
            killsDisplay.text = kills.ToString();
            globalCanvas.transform.position = gameObject.transform.position;
            //CmdGetKills();
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
                //vel -= (diff*17);
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
            if (!isDead) {
                transform.position = newPos;
            }
            

             

        
            
        } else {
            gameObject.GetComponent<PlayerController>().enabled = false;
            cam.enabled = false;
            canvasRef.enabled = false;
            gameObject.GetComponent<facingScript>().enabled = false;
        }

        if (Input.GetMouseButtonDown(0)) {

            if (hasBall && !isDead) {
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

        if (Input.GetKey(KeyCode.Delete)) {
            if (isServer) {
                //CmdDie();
                CmdSpload();
                RpcDie();

               
            }
        }

    }

    
    [Client]
    IEnumerator respawn() {
        Debug.Log("Timer Started");
        yield return new WaitForSeconds(4f);

        CmdRespawn();
    }

    IEnumerator setTextToNothing() {
        yield return new WaitForSeconds(4f);
        wintxt.text = "";
    }

    void addKill() {
        
            CmdAddKill();
        
    }
    [Command]
    void CmdSpload() {
        
        
        GameObject o = Instantiate(_gibSpload, transform.position, Quaternion.identity);
        NetworkServer.Spawn(o);

    }
    [Command]
    public void CmdResetSpawn(Vector3 pos) {
        gameObject.transform.position = pos;
    }
    [Command]
    void CmdRespawn() {
        isDead = false;
        swingStone.GetComponent<SpriteRenderer>().enabled = hasBall;
        transform.position = new Vector3(-2, -2, 0);
        broom.GetComponent<SpriteRenderer>().enabled = true;
        RpcRespawn();
        /*
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
        */
        

    }
    [Command]
    void CmdSetIDName(string n) {
        IDName = n;
    }
    [Command]
    public void CmdSetBoard() {
        foreach (KeyValuePair<string, PlayerController> kv in GameManage.players) {

            string s = "";
            List<string> tx = new List<string>();
            //leaderboardManager lb = kv.Value.leaderBoard.GetComponent<leaderboardManager>();
            for (int i = 0; i < GameManage.players.Count; i++) {
                // Text t = new Text();
                // tx.Add()
                tx.Add(GameManage.players.ElementAt(i).Value.IDName + " " + GameManage.players.ElementAt(i).Value.kills.ToString());
                //leaderBoard.textList[i].text = GameManage.players.ElementAt(i).Value.IDName + " " + GameManage.players.ElementAt(i).Value.kills.ToString();
                s += GameManage.players.ElementAt(i).Value.IDName + " " + GameManage.players.ElementAt(i).Value.kills.ToString();
                if (i == GameManage.players.Count - 1) {

                } else {
                    s += ",";
                }

            }
            Debug.Log(s);
            List<string> lst = s.Split(',').ToList<String>();
            for (int i = 0; i < lst.Count; i++) {
                leaderBoard.textList[i].text = lst[i];
            }
            //kv.Value.RpcSetBoard(s);

            //kv.Value.CmdSetBoard();
            RpcSetBoard(s);
        }
    }
    [Command]
    void CmdYeet(float dir) {
        hasBall = false;
        swingStone.GetComponent<SpriteRenderer>().enabled = hasBall;

        GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
        curlingStone stone = ball.GetComponent<curlingStone>();
        
        ball.transform.rotation = Quaternion.Euler(new Vector3(0, 0, dir));
        ball.transform.position += ball.transform.right * 2f;
        //ball.GetComponent<Rigidbody2D>().velocity = ball.transform.right * 20f;
        ball.GetComponent<Rigidbody2D>().AddForce(ball.transform.right * 2f, ForceMode2D.Impulse);

        

        stone.owner = gameObject;
        stone.isShot = true;


        NetworkServer.Spawn(ball);

        //RpcYeet();
    }
    [Command]
    void CmdDie() {
        isDead = true;

        swingStone.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        broom.GetComponent<SpriteRenderer>().enabled = false;
        isDead = true;

        RpcDie();

        //GameObject.Destroy(gameObject);
        //manager.respawn(gameObject.GetComponent<NetworkBehaviour>().connectionToClient);
        //

    }
    [Command]
    void CmdGetKills() {
        kills = GameManage.players[myNetId].kills;
        RpcGetKills();
    }
    
    void CmdSetStone() {

        hasBall = true;
        RpcSetStone();
        

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

    [Command]
    void CmdAddKill() {
        //Debug.Log(gameObject.name.ToString() + " was killed by " + enemy.name.ToString());
        kills += 1;
    }
    [Command]
    void CmdGetScores() {

    }

    [ClientRpc]
    public void RpcDie() {
        //
        isDead = true;
        hasBall = false;

        swingStone.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        broom.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(respawn());
    }
    [ClientRpc]
    public void RpcSetStoneVisible(bool visible) {
        if (isLocalPlayer) {
            return;
        }
        swingStone.GetComponent<SpriteRenderer>().enabled = visible;
    }
    [ClientRpc]
    void RpcRespawn() {
        isDead = false;
        hasBall = false;
        swingStone.GetComponent<SpriteRenderer>().enabled = hasBall;
        
        GetComponent<PlayerController>().enabled = true;
        GetComponent<PlayerController>().cam.enabled = true;
        GetComponent<PlayerController>().isDead = false;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<NetworkTransform>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        broom.GetComponent<SpriteRenderer>().enabled = true;
    }
    [ClientRpc]
    void RpcGetKills() {
        killsDisplay.text = kills.ToString();
    }
    [ClientRpc]
    public void RpcResetSpawn(Vector3 pos,string win) {
        gameObject.transform.position = pos;
        wintxt.text = win;
        StartCoroutine(setTextToNothing());
    }

   

    [ClientRpc]
    public void RpcSetStone() {
        hasBall = true;
    }

    

    

    

    [ClientRpc]
    public void RpcSetBoard(string str) {
        List<string> lst =  str.Split(',').ToList<String>();
        for (int i = 0; i < lst.Count; i++) {
            leaderBoard.textList[i].text = lst[i];
        }
    }
    [ClientRpc]
    public void RpcGetString(string s) {
        Debug.Log("got " + s);
        List<string> str = s.Split(',').ToList<string>();

        for(int i = 0; i < leaderBoard.textList.Count; i++) {
            leaderBoard.textList[i].text = str[i];
        }
    }



}

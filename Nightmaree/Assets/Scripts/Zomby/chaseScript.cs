using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chaseScript : MonoBehaviour {
    public Transform player;
    public Transform head;
    Animator anim;

    string state = "patrol";
    public GameObject[] waypoints; //my way points
    int currentWP = 0;   //truck the way points 
    public float rotSpeed = 0.2f; //rotation speed 
    public float speed = 1.5f;  //speed
    float accuracyWP = 5.0f; //for the zombi to not pass the way point 
    bool chackingLost = false;

    public AudioSource audioSource;

    public AudioClip soundSleleton;

    private float timer = 10f;
    private Text timerSeconds;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        timerSeconds = GetComponent<Text>();
	}

    //the style of the losing screen
    public void OnGUI()
    {
        if (chackingLost)
        {
        GUIStyle myStyle = new GUIStyle(GUI.skin.GetStyle("Button"));
        myStyle.fontSize = 50;
        GUI.color = Color.red;
        GUI.Label(new Rect((Screen.width / 2) - 150, 70, 300, 45), " YOU LOST ", myStyle);
        }
    }
        // Update is called once per frame
        void Update () {
        Vector3 direction = player.position - this.transform.position; // if it is last then 10 we will take the diraction 
        direction.y = 0; //making the zomby look at us and not going down
        float angle = Vector3.Angle(direction, head.up);
        
        if (state == "patrol" && waypoints.Length > 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            if(Vector3.Distance(waypoints[currentWP].transform.position , transform.position) < accuracyWP) // going to all of the waypoints
            {
                currentWP++;
                if(currentWP >= waypoints.Length) // if we where at all of ower waypoints , we want to start again
                {
                    currentWP = 0;
                }
            }

            //we want ower zombi to rotate to ower zombi
            direction = waypoints[currentWP].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }

        if(chackingLost)
        {
            timer -= Time.deltaTime*3;
           
            if (timer <= 0)
            {
                Application.LoadLevel(0);
            }
        }

        if (Vector3.Distance(player.position, this.transform.position) < 10 && (angle < 35 || state == "pursuing")) // take a look at the distance beetwen the player position and the sceleton position
        {
            state = "pursuing";
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime); //makink him rotate to that rotation 

            //anim.SetBool("isIdle", false); //changing the idle posistion to false if we want to make him move
            if (direction.magnitude > 2) //the lantg of the vector 
            {
                speed = 3.2f;
                this.transform.Translate(0, 0, Time.deltaTime * speed); //we are going to move the sceleton
                anim.SetBool("isWalking", true); //making him walke
                anim.SetBool("isAttacking", false); //naking him attack us
            }

            //Starting the game all over again when we are losing
            else
            {
                audioSource.PlayOneShot(soundSleleton);
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                chackingLost = true;
            }
        }
        else //if we are not near the zombi we will stay in idle
        {
            speed = 1.5f;
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
            state = "patrol";
        }

		
	}
}

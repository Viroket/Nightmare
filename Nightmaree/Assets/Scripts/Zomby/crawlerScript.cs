using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawlerScript : MonoBehaviour {

    public Transform player;
    public float rotSpeed = 0.4f; //rotation speed 
    bool chackingLost = false;
    private float timer = 5f;

    public AudioSource audioSource;

    public AudioClip soundZombi;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(player.position, this.transform.position) < 8)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            audioSource.PlayOneShot(soundZombi);





            this.transform.rotation = Quaternion.Slerp(this.transform.rotation , Quaternion.LookRotation(direction), 0.5f);
           
            if (direction.magnitude > 2)
            {
                this.transform.Translate(0, 0, 0.2f);
            }
            else
            { 
                chackingLost = true;
            }
        }

        


            if (chackingLost)
        {
            timer -= Time.deltaTime * 3;

            if (timer <= 0)
            {
                Application.LoadLevel(0);
            }
        }


    }

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
}

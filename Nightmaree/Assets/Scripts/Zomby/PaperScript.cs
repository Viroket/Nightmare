using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperScript : MonoBehaviour {
    public int paper = 0;
    public int paperToWin = 8;
    public bool chackingPapers = false;

    private float timer = 10f;
    private Text timerSeconds;


    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "paper")
        {
            paper += 1;
            Debug.Log("A paper was picked up . Total papers =" + paper);
            Destroy(col.gameObject);
        }
    }
    public void OnGUI()
    {
        if (paper < paperToWin)
        {
            GUI.Box(new Rect((Screen.width / 2) - 100, 10, 200, 35), ""+ paper +"/" + paperToWin + " papers");
        }
        else
        {
            GUI.Box(new Rect((Screen.width / 2) - 100, 10, 200, 35), "All Papers Collected!");
            chackingPapers = true;

            GUIStyle myStyle = new GUIStyle(GUI.skin.GetStyle("warLabel"));
            myStyle.fontSize = 50;
            GUI.color = Color.red;
            GUI.Label(new Rect((Screen.width / 2) - 150, 70, 300, 45), " YOU WON  ", myStyle);

            timer -= Time.deltaTime * 3;

            if (timer <= 0)
            {
               Application.LoadLevel(0);
            }
        }
        
    }

    
    

}

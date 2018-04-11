using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteriScript : MonoBehaviour {

    public bool showText;
    public int Bat;

    public GameObject Flight;
    public int mainBat;

    public bool safeRemove;


    private void Start()
    {
        showText = false;
    }


    void OnTriggerStay(Collider other)
    {
        showText = true;
        if (!safeRemove)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                mainBat = Flight.GetComponent<FlashLightScript>().batLevel;
                Bat = 15;
                Flight.GetComponent<FlashLightScript>().batLevel = Bat += mainBat;
                safeRemove = true;

                if (safeRemove)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        showText = false;
    }
    


    private void OnGUI()
    {
        if(showText)
        {
            GUI.Box(new Rect((Screen.width / 2) - 100, 60, 200, 35), " Press 'E' to pickup");
        }
    }
}

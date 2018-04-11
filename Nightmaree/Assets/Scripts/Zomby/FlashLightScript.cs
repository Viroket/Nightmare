using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightScript : MonoBehaviour {

    public Light flashLight;
    public AudioSource audioSource;

    public AudioClip soundFlashOn;
    public AudioClip soundFlashOff;
    private bool isActive;

    public float d;
    public int batLevel;
    public float timer;

    public Light Flight;
    public bool isOn;

    void minusBat()
    {
        if (isOn)
        {
            batLevel -= 1;
        }
    }





    // Use this for initialization
    void Start ()
    {
        isActive = true;
        Flight = GetComponent<Light>();
        batLevel = 31;
        minusBat();
        isOn = true;
    }

    public void OnGUI()
    {
        GUI.Box(new Rect(0, Screen.height / 44, Screen.width / 9, Screen.height / 14), batLevel.ToString());
    }

    // Update is called once per frame
    void Update ()
    {
        if (timer >= 0)
        {
            if (isOn)
            {
                timer -= Time.deltaTime;
            }
        }

        if (timer <= 0)
        {
            timer = 5;
            minusBat();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            Flight.enabled = !Flight.enabled;
            if(!isOn)
            {
                isOn = true;
            }
            else
            {
                isOn = false;
            }
        }

        if(batLevel == 0)
        {
            batLevel = 0;
            flashLight.enabled = false;
            isOn = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(isActive == false) //Toggle flashligh on
            {
                //flashLight.enabled = true;
                //isActive = true;

                audioSource.PlayOneShot(soundFlashOn);
            }
            
            else if(isActive == true) //Toggle flashligh off
            {
                //flashLight.enabled = false;
                //isActive = false;

                audioSource.PlayOneShot(soundFlashOff);
            }
        }
    }
}

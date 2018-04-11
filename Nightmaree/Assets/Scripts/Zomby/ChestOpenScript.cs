using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenScript : MonoBehaviour {

    public AudioClip chestSound;
    public Transform chestTreasure;

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(chestSound, transform.position);
            chestTreasure.GetComponent<Animation>().Play();
            Destroy(gameObject);
        }
    }

}

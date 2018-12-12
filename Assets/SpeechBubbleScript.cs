using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Return)){
            GameObject.Find("Player").GetComponent<PlayerMovementScript>().canMove = true;
            gameObject.SetActive(false);
            GameObject.Find("EnterButton").SetActive(false);
        }
    }
}

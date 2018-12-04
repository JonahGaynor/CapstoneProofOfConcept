using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalFloorTiles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<PlayerMovementScript>().canMove = false;

        }
    }
    void OnCollisionStay(Collision collision) {
        if (this.tag == "LeftTile" && collision.gameObject.tag == "Player"){
            if (collision.gameObject.GetComponent<PlayerMovementScript>().stunned){
                collision.gameObject.GetComponent<PlayerMovementScript>().moveLeft = true;
            }
        }
    }
}

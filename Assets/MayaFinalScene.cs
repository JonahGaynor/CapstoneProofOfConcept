using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayaFinalScene : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        this.GetComponent<PlayerMovementScript>().canMove = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0.5f, GameObject.Find("Player").transform.position.y, 0);
	}
}

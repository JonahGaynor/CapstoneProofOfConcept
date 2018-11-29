using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1MayaScript : MonoBehaviour {

    public bool active = false;
    Vector3 targetPos;

	// Use this for initialization
	void Start () {
        targetPos = GameObject.Find ("Agent1").GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	void Update () {
        if (active){
            if (transform.position.y >= targetPos.y){
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
            } else {
                transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, 0);
            }

        }
    }
}

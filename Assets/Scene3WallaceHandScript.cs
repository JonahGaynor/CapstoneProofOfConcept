using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3WallaceHandScript : MonoBehaviour {

    bool moveUp = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (moveUp) {
            transform.position = new Vector3(transform.position.x, ((transform.position.y * 11) / 10), 0);
        }
        else if (transform.position.y < 5f) {
            moveUp = true;
        } else if (GameObject.Find("MayaIDCard").GetComponent<MayaIDCardScript>().active) {
            transform.position = new Vector3(GameObject.Find("MayaIDCard").transform.position.x, GameObject.Find("MayaIDCard").transform.position.y + 4.8f, 0);
        }
    }
}

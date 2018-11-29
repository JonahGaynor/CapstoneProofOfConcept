using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MayaIDCardScript : MonoBehaviour {

    float targetY = 0;
    bool moveDown = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > targetY){
            transform.position = new Vector3 (transform.position.x, ((transform.position.y * 9)/10), 0);
        } else if (moveDown){
            targetY = -10f;
            transform.position = new Vector3(transform.position.x, ((transform.position.y * 9) + targetY) / 10, 0);
        }
        if (transform.position.y < 1 && Input.GetKeyUp(KeyCode.Return)){
            moveDown = true;
        }
        if (this.transform.position.y < -9f){
            this.gameObject.SetActive(false);
        }
    }
}

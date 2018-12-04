using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2ElevatorScript : MonoBehaviour {

    float timer = 0;
    public Sprite[] mySprites;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > 9.0f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[5];  
        } else if (timer > 8.7f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[4];
        } else if (timer > 6.7f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[0];
        } else if (timer > 5.2f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[3];
        } else if (timer > 3.7f){
            this.GetComponent<SpriteRenderer>().sprite = mySprites[2];
        } else if (timer > 2.2f) {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[1];
        } else {
            this.GetComponent<SpriteRenderer>().sprite = mySprites[0];
        }
    }
}

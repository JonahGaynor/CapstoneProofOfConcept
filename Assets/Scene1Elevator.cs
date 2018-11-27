using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Elevator : MonoBehaviour {

    float timer = 0;
    float maxTime;
    public Sprite[] mySprites;
    int myCounter = 0;
    int mode = 0;
    float modeTimer = 0;
    

	// Use this for initialization
	void Start () {
        maxTime = 0.8f;
	}
	
	// Update is called once per frame
	void Update () {
        if (mode == 0){
            maxTime = 0.25f;
        } else if (mode == 1){
            maxTime = 0.20f;
        } else if (mode == 2){
            maxTime = 0.15f;
        } else if (mode == 3){
            maxTime = 0.10f;
        }

        modeTimer++;
        timer += Time.deltaTime;
        if (timer >= maxTime){
            if (myCounter > 2){
                myCounter = 0;
            } else {
            myCounter++;
            }
            timer = 0;
        }
        this.GetComponent<SpriteRenderer>().sprite = mySprites[myCounter];
        if (modeTimer >= 4.6f){
            mode = 0;
        } else if (modeTimer >= 4.3f){
            mode = 1;
        } else if (modeTimer >= 4.0f){
            mode = 2;
        } else if (modeTimer >= 2.5f){
            mode = 3;
        } else if (modeTimer >= 2.0f){
            mode = 2;
        } else if (modeTimer >= 1.2f){
            mode = 1;
        } else {
            mode = 0;
        }
	}
}

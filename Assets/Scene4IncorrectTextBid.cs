using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene4IncorrectTextBid : MonoBehaviour {

    float timer = 0f;
    bool haveStartedCoroutine = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (this.GetComponent<Text>().enabled == true) {
            timer += Time.deltaTime;
            if (!haveStartedCoroutine){
                StartCoroutine(TurnOn());
            }
        } else {
            timer = 0;
        }
        if (timer > 1f) {
            this.GetComponent<Text>().enabled = false;
            this.GetComponent<Text>().text = "";
            timer = 0;
        }
    }

    IEnumerator TurnOn() {
        yield return new WaitForSeconds(0.4f);
        this.GetComponent<Text>().text = "INVALID BID";
    }
}

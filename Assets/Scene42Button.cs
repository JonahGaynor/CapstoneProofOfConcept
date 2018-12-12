using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene42Button : MonoBehaviour {

    int counter = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown() {
        counter++;
        if (counter >= 2) {
            Scene4Pulse.instance.IncreaseStep();
            GameObject.Find("BidButton").GetComponent<Image>().enabled = true;
        }
    }
}

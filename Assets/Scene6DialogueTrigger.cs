using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene6DialogueTrigger : MonoBehaviour {

    bool haveStarted = false;

    Vector3 mayaPos;

	// Use this for initialization
	void Start () {
        mayaPos = GameObject.Find("Player").GetComponent<Transform>().position;
	}

    // Update is called once per frame
    void Update() {
        mayaPos = GameObject.Find("Player").GetComponent<Transform>().position;

        if (mayaPos.y >= -2.6f && !haveStarted)
        {
            Debug.Log("start");
            haveStarted = true;
            this.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<DialogueTrigger>().TriggerDialogue();
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}

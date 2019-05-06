using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Wait());
	}

    //// Update is called once per frame
    //void Update () {

    //}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.76f);
        this.GetComponent<DialogueTrigger>().TriggerDialogue();

    }
}

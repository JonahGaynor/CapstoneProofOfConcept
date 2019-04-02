using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantChoiceBattleButton : MonoBehaviour {

    public Dialogue dialogue;
    public Animator myAnim;

	// Use this for initialization
	void Start () {
        myAnim = GetComponentInParent<Animator>();
        myAnim.SetBool("turnOn", true);
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void OnClick()
    {

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        myAnim.SetBool("turnOn", false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantChoiceBattleButton : MonoBehaviour {

    public Dialogue dialogue;
    public Animator myAnim;
    GameObject player;
    GameObject dialogueManager;
    bool hasStarted = false;

	// Use this for initialization
	void Start () {
        myAnim = GetComponentInParent<Animator>();
        player = GameObject.Find("Player");
        dialogueManager = GameObject.Find("DialogueManager");
        //myAnim.SetBool("turnOn", true);
	}

    //// Update is called once per frame
    //void Update () {

    //}
    void Update()
    {
        if (player.transform.position.y >= 11.5f && dialogueManager.GetComponent<DialogueManager>().done && !hasStarted)
        {
            hasStarted = true;
            myAnim.SetBool("turnOn", true);
        }
    }

    public void OnClick()
    {

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        myAnim.SetBool("turnOn", false);
        StartCoroutine(GameObject.Find("endtile").GetComponent<EndTileScript>().PauseToChangeLevel());
    }

}

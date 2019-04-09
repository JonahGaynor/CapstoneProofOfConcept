using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Petty : MonoBehaviour {

    GameObject dialogue;
   //public Sprite test;
    public int storageCounter = 0;

    // Update is called once per frame
    void Update()
    {
        dialogue = GameObject.Find("DialogueManager");
        if (dialogue.GetComponent<DialogueManager>().done && this.GetComponent<DialogueStorage>().storage.Length >= storageCounter)
        {
            Debug.Log("dialogue completed");
            dialogue.GetComponent<DialogueManager>().done = false;
            StartCoroutine(NewCharacter());
        }
        else if (storageCounter > this.GetComponent<DialogueStorage>().storage.Length && dialogue.GetComponent<DialogueManager>().done)
        {
            Debug.Log("make active");
            GameObject.Find("MayaIDCard").GetComponent<MayaIDCardScript>().active = true;
            this.enabled = false;
        }
    }

    IEnumerator NewCharacter() {
        Debug.Log("switch character");
        yield return new WaitForSeconds(0.3f);
        //dialogue.GetComponent<DialogueManager>().speakerSprite = this.GetComponent<DialogueTrigger>().dialogue.speakerSprite;
        dialogue.GetComponent<DialogueManager>().myAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<DialogueManager>().done = false;
        this.GetComponent<DialogueTrigger>().TriggerDialogue();
        //yield return new WaitForSeconds(1.3f);
        //if (storageCounter >= this.GetComponent<DialogueStorage>().storage.Length)
        //{
        if (this.GetComponent<DialogueStorage>().storage.Length > storageCounter)
        {
            this.GetComponent<DialogueTrigger>().dialogue = this.GetComponent<DialogueStorage>().storage[storageCounter];
        }
        else if (this.GetComponent<DialogueStorage>().storage.Length < storageCounter)
        {
            Debug.Log("make active");
            GameObject.Find("MayaIDCard").GetComponent<MayaIDCardScript>().active = true;
            this.enabled = false;
        }
        dialogue.GetComponent<DialogueManager>().done = false;

        storageCounter++;

        //if (storageCounter == 8)
        //{
        //    yield return new WaitForSeconds(1.0f);
        //    this.enabled = false;
        //}
        //this.GetComponent<DialogueTrigger>().dialogue.speakerSprite = test;
        //this.enabled = false;
    }
}

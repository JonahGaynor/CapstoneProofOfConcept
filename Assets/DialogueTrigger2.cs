using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger2 : MonoBehaviour
{

    public Dialogue dialogue;
    public Dialogue dialogue2;
    int counter = 0;

    public void TriggerDialogue()
    {
        if (counter == 0)
        {
            GameObject.Find("DialogueManager2").GetComponent<DialogueManager>().StartDialogue(dialogue);
        }
        else if (counter == 1)
        {

        }
    }
}

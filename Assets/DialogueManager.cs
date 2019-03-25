using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Image speakerSprite;
    public Text dialogueText;

    Queue<string> sentences;

    public Animator myAnim;

    public bool done = false;

	// Use this for initialization
	void Awake () {
        sentences = new Queue<string>();
	}
	
    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting dialogue with " + dialogue.name);

        GameObject.Find("Player").GetComponent<PlayerMovementScript>().canMove = false;

        myAnim.SetBool("isOpen", true);

        speakerSprite.sprite = dialogue.speakerSprite;

        //nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            //Debug.Log(sentence);
        }

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            Debug.Log("no more sentences");
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(NextChar(sentence));
    }

    IEnumerator NextChar(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        GameObject.Find("Player").GetComponent<PlayerMovementScript>().canMove = true;
        done = true;
        myAnim.SetBool("isOpen", false);

    }
}

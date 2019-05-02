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
    bool finishedSentence = true;
    string holdSentence;

    public AudioClip typewriter;
    AudioSource myAudio;

    // Use this for initialization
    void Awake () {
        sentences = new Queue<string>();
        myAudio = GetComponent<AudioSource>();
    }
    
    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting dialogue with " + dialogue.name);

        GameObject.Find("Player").GetComponent<PlayerMovementScript>().canMove = false;
        done = false;
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

        if (!finishedSentence)
        {
            StopAllCoroutines();
            dialogueText.text = holdSentence;
            myAudio.Stop();
            finishedSentence = true;
        }
        else if (sentences.Count == 0)
        {
            Debug.Log("no more sentences");
            EndDialogue();
            return;
        }
        else
        {
            string sentence = sentences.Dequeue();
            myAudio.Stop();
            StopAllCoroutines();

            myAudio.PlayOneShot(typewriter);
            StartCoroutine(NextChar(sentence));
        }

    }

    IEnumerator NextChar(string sentence)
    {
        finishedSentence = false;
        dialogueText.text = "";
        holdSentence = sentence;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //if (sentence[sentence.Length] == letter)
            //{
            //    myAudio.Stop();
            //}
            yield return null;
        }
        myAudio.Stop();
        finishedSentence = true;
    }

    public void EndDialogue()
    {
        myAudio.Stop();
        GameObject.Find("Player").GetComponent<PlayerMovementScript>().canMove = true;
        done = true;
        myAnim.SetBool("isOpen", false);

    }
}

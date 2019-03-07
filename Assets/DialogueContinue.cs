using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueContinue : MonoBehaviour {

    public Sprite downSprite;
    public Sprite upSprite;
    GameObject TextBox;

    void Start()
    {
        TextBox = GameObject.FindGameObjectWithTag("TextBox");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return)){
            TextBox.GetComponent<Image>().sprite = downSprite;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("trigger");
            TextBox.GetComponent<Image>().sprite = upSprite;
            NextSentence();
        }
	}

    public void NextSentence() {
        FindObjectOfType<DialogueManager>().NextSentence();
    }
}

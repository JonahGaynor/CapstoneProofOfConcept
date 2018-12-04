using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene5TextScript : MonoBehaviour
{

    int enterCounter = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            enterCounter++;
            if (enterCounter == 1)
            {
                this.GetComponent<Text>().text = "Of course when you play against other people they won't show you their dice.";
            }
            else if (enterCounter == 2)
            {
                this.GetComponent<Text>().text = "Since we can't have you knowing where the Central Computer is located, we're going to give you some anaesthesia.";
            }
            else if (enterCounter == 3)
            {
                this.GetComponent<Text>().text = "When you wake up you'll be inside of the Central Computer. Good luck. Agent Petty, administer the anaesthesia.";
            }
            else if (enterCounter == 4)
            {
                this.GetComponent<Text>().text = "...";
            }
            else if (enterCounter == 5)
            {
                this.GetComponent<Text>().text = "So we ran out of anaesthesia, huh?";
            }
            else if (enterCounter == 6)
            {
                SceneManager.LoadScene("ProofofConcept_Scene6");
            }
        }
    }
}
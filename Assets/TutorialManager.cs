using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public int enterCounter = 0;

    public Dialogue secondDialogue;

    public GameObject textBox;

    public Sprite enemyBlankDice;

    GameObject[] enemyDice;

    /*
    1. These are your dice
    2. These are my dice.
    4. *Hide dice* When we play, you won't see my dice and I won't see yours.
    5. You can do 2 things in this game: Bid and Call.
    6. You bid based on how many of a certain dice face you believe to be among our 10 combined dice.
    7. Every bid must be higher than the opponent's. You can increase either the amount of the bid or the dice face.
    8. If you increase the amount of the bid, you can bid any dice face. You may not decrease the amount of dice.
    9. If I were to bid two 5s, what could you bid? Try bidding using the bidding area.
    10. You call when you believe that the total dice do not have at least the amount of the relevant dice face.
    11. If you call a bid of four 2s, you would be correct if there are three or fewer 4s. You would be incorrect if there are four or more 2s. (bid this)
    12. If you correctly call, the opponent loses one of their dice. If you incorrectly call, you lose one of your dice.
    13. We then roll our dice and go again until one of us loses all of our dice.
    */

    private void Start()
    {
        Cursor.visible = true;
        textBox = GameObject.FindGameObjectWithTag("TextBox");
        textBox.GetComponent<Animator>().SetBool("isOn", true);
        enemyDice = GameObject.FindGameObjectsWithTag("EnemyDice");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            enterCounter++;
            if (enterCounter == 1)
            {
                TheseAreMyDice();
            }
            else if (enterCounter == 2)
            {
                HideDice();
            }
            else if (enterCounter == 4)
            {
                Between10Dice();
            }
            else if (enterCounter == 5)
            {
                NotClear();
            }
            else if (enterCounter == 7)
            {
                BiddingArea();
            }
            else if (enterCounter == 9)
            {
                LoseDice();
            }
            else if (enterCounter == 12)
            {
                End();
            }
        }
	}

    void TheseAreMyDice()
    {
        Scene4Pulse.instance.IncreaseStep();
    }

    void HideDice()
    {
        foreach (GameObject dice in enemyDice)
        {
            dice.GetComponent<SpriteRenderer>().sprite = enemyBlankDice;
        }
        Scene4Pulse.instance.IncreaseStep();
    }

    void Between10Dice()
    {
        GameObject.Find("Border2").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Border2").GetComponent<Scene4SecondPulse>().Reset();
        Scene4Pulse.instance.IncreaseStep();
    }

    void NotClear()
    {
        GameObject.Find("Border2").GetComponent<SpriteRenderer>().enabled = false;
        Scene4Pulse.instance.IncreaseStep();
    }

    public void Clear()
    {
        Debug.Log("clear");
        Scene4Pulse.instance.IncreaseStep();
        FindObjectOfType<DialogueManager>().StartDialogue(secondDialogue);
        GameObject[] biddingDice = GameObject.FindGameObjectsWithTag("BiddingDice");
        foreach (GameObject dice in biddingDice)
        {
            Destroy(dice);
        }
        GameObject.Find("CallButton").GetComponent<Image>().enabled = true;
    }

    void BiddingArea()
    {
        Scene4Pulse.instance.IncreaseStep();
        GameObject.Find("BidButton").GetComponent<Image>().enabled = true;
    }

    public void LoseDice()
    {
        GameObject.Find("EnemyDice4").SetActive(false);
    }

    void End()
    {
        SceneManager.LoadScene("ProofofConcept_Scene5");
    }
}

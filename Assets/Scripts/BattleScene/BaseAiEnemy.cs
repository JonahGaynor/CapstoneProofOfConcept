using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAiEnemy : MonoBehaviour {

    int counter = 0;
    int currentBidNumber;
    int currentBidAmount;

    int currentBidAmountDifference;
    int currentBidNumberDifference;
    int turnNumber;
    public int bluffingNumb = 0;
    public int bluffingNumb2 = 0;
    bool isBluffing = true;

    int rand1 = 0;
    int rand2 = 0;
    bool randSwitcher = true;

    public GameObject[] myDice;
    public GameObject[] myBidDice;
    public GameObject[] mayasDice;
    public Transform boxPos;

    public int enemyNumber;

    int myOnes = 0;
    int myTwos = 0;
    int myThrees = 0;
    int myFours = 0;
    int myFives = 0;
    int mySixes = 0;

    int MayaOnes = 0;
    int MayaTwos = 0;
    int MayaThrees = 0;
    int MayaFours = 0;
    int MayaFives = 0;
    int MayaSixes = 0;

    public List<int> MayaBids;

    // Use this for initialization
    void Start()
    {
        boxPos = GameObject.Find("3DPrinter").GetComponent<Transform>();
        turnNumber = 0;
    }

    //AI Enemy 1
    //Behavior:
    //First bid: one 1
    //Calls: NEVER
    //Bids: +1 number
    public void Turn() {

        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        StartCoroutine(Pause());

        //First bid: one 1
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
        }

        //Normal bidding. +1 number but +1 amount and 1 if it's 6. Always increase by 1 value.
        else {
            if (currentBidNumber == 6)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, currentBidNumber + 1);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[currentBidNumber], boxPos.position, Quaternion.identity);
                }
            }
        }
    }

    //AI Enemy 2
    //Behavior:
    //First bid: one 1
    //Calls: If you go +2 amount and if it's not the first bid or if the bid amount is 4
    //Bids: +1 number

    public void TurnAi2()
    {
        turnNumber++;
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        StartCoroutine(Pause());

        //First bid: one 1
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
        }

        //Call if: Maya bids >=+2 on the amount or if Maya bids 4 of a number
        else if (currentBidAmountDifference >= 2 && turnNumber > 2 || currentBidAmount >= 4)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Normal bidding. +1 number but +1 amount and 1 if it's 6. Always increase by 1 value.
        else
        {
            if (currentBidNumber == 6)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, currentBidNumber + 1);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[currentBidNumber], boxPos.position, Quaternion.identity);
                }
            }
        }
        currentBidAmountDifference = 0;
    }

    //AI Enemy 3
    //Behavior:
    //First bid: one 6
    //Calls: If you go +2 amount and if it's not the first bid or if the bid amount is 4
    //Bids: +1 number if it doesn't have the number, +1 amount if it does

    public void TurnAi3()
    {
        turnNumber++;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        StartCoroutine(Pause());

        //First bid: one 6
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
        }

        //Call if: Maya bids >=+2 on the amount or if Maya bids 4 of a number
        else if (currentBidAmountDifference >= 2 && turnNumber > 1 || currentBidAmount >= 4)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Bid +1 on Maya's bid.
        else
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
            for (int i = 0; i < currentBidAmount + 1; i++)
            {
                Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
            }
        }
    }

    //Julia
    //Behavior:
    //First bid: (50%) one her highest number (50%) one 1
    //Turn counter = 1: (80%) call if starting bid is >=3
    //Calls: (40%) If you increase number and amount OR (100%) if you bid 4 of anything
    //Bids: (70%) next highest number (30%) +1 amount

    public void TurnJulia()
    {
        turnNumber++;
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }
        StartCoroutine(Pause());

        //First bid: (50%) one her highest number (50%) one 1
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            if (Random.value < 0.5f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
                Instantiate(myDice[0], boxPos.position, Quaternion.identity);
            }
            else
            {
                if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
                    Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 5);
                    Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 4);
                    Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 3);
                    Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 2);
                    Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }

        }

        //Turn counter = 1: (80%) call if starting bid is >=3
        else if (turnNumber == 1 && currentBidAmount >= 3 && Random.value <= 0.8f)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Calls: (40%) If you increase number
        else if (currentBidAmountDifference > 0 && currentBidNumberDifference > 0 && Random.value <= 0.4f)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Calls: if you bid 4 of anything
        else if (currentBidAmount >= 4)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Bids: (70%) next highest number
        else
        {
            if (Random.value < 0.7f)
            {
                if (currentBidNumber == 1)
                {
                    if (myTwos > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myThrees > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFours > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFives > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (mySixes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (currentBidNumber == 2)
                {
                    if (myThrees > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFours > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFives > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (mySixes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myOnes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (currentBidNumber == 3)
                {
                    if (myFours > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFives > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (mySixes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myOnes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myTwos > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (currentBidNumber == 4)
                {
                    if (myFives > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (mySixes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myOnes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myTwos > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myThrees > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (currentBidNumber == 5)
                {
                    if (mySixes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myOnes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myTwos > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myThrees > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFours > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (currentBidNumber == 6)
                {
                    if (myOnes > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myTwos > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myThrees > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFours > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFives > 0)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
            }

            //(30%) +1 amount
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                }
            }
        }
        myOnes = 0;
        myTwos = 0;
        myThrees = 0;
        myFours = 0;
        myFives = 0;
        mySixes = 0;
    }

    //AI Enemy 4
    //Behavior:
    //First bid: starts at 1-2 of rand#
    //Calls: If you go +2 amount and if it's not the first bid or if the bid amount and number is increased if not the first or second bid
    //Bids: alternates between 2 rand numbers

    public void TurnAi4()
    {
        if (rand1 == 0)
        {
            rand1 = Random.Range(1, 7);
        }
        if (rand2 == 0)
        {
            rand2 = Random.Range(1, 7);
        }
        turnNumber++;
        myBidDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        mayasDice = GameObject.FindGameObjectsWithTag("Dice");
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        StartCoroutine(Pause());

        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            int rand = Random.Range(1, 7);
            if (Random.value < 0.5f || myBidDice.Length + mayasDice.Length < 4)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, rand);
                for (int i = 0; i < 1; i++)
                {
                    Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, rand);
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                }
            }
        }

        else if (currentBidAmount >= 4)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Calls: If you increase number
        else if (currentBidAmountDifference > 0 && currentBidNumberDifference > 0 && turnNumber > 2)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Calls: if you bid 4 of anything
        else if (currentBidAmountDifference >= 2 && turnNumber > 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Has 2 numbers, bids switch between the two of them.
        else
        {
            if (randSwitcher)
            {
                if (rand1 > currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand1);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand1 - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand1 - 1], boxPos.position, Quaternion.identity);
                    }
                }
                randSwitcher = false;
            }
            else
            {
                if (rand2 > currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand2 - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand2 - 1], boxPos.position, Quaternion.identity);
                    }
                }
                randSwitcher = true;
            }
        }
    }

    //AI Enemy 5
    //Behavior:
    //First bid: one 6
    //Calls: If you go +2 amount and if it's not the first bid
    //Calls: If you bid on a new number after 2 bids
    //Bids: next number it has

    public void TurnAi5()
    {
        turnNumber++;
        MayaBids.Add(currentBidNumber);
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }
        StartCoroutine(Pause());

        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
        }

        //Calls: if you bid 4 of anything
        else if (currentBidAmountDifference >= 2 && turnNumber > 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Calls: if you bid on a new number after 2 bids
        else if (turnNumber > 2 && !MayaBids.Contains(currentBidNumber))
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        else
        {
            if (currentBidNumber == 1)
            {
                if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 2)
            {
                if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 3)
            {
                if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 4)
            {
                if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 5)
            {
                if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 6)
            {
                if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }

        myOnes = 0;
        myTwos = 0;
        myThrees = 0;
        myFours = 0;
        myFives = 0;
        mySixes = 0;
    }

    //AI Enemy 6
    //Behavior:
    //First bid: one rand
    //Calls: When you bid on its bluffing numb
    //Bids: picks next available numb it doesn't have, then sticks to it

    public void TurnAi6()
    {
        turnNumber++;
        if (bluffingNumb == 0)
        {
            bluffingNumb = Random.Range(1, 7);
        }
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        StartCoroutine(Pause());

        //First bid: one rand
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            int rand = Random.Range(1, 7);
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, rand);
            Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
        }

        //Calls: When you bid on its bluffing numb and not the first turn
        else if (currentBidNumber == bluffingNumb && turnNumber > 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Calls: when you bid 4+ on a numb
        else if (currentBidAmount >= 4)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }

        //Bids: on randomly selected bluffing numb and sticks to it
        else
        {
            if (bluffingNumb <= currentBidNumber)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                }
            }
        }
    }

    public void TurnJacob()
    {
        turnNumber++;
        if (rand1 == 0)
        {
            rand1 = Random.Range(1, 7);
        }
        if (rand2 == 0)
        {
            rand2 = Random.Range(1, 7);
        }
        myBidDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        mayasDice = GameObject.FindGameObjectsWithTag("Dice");
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().battleDice)
        {
            if (d.GetComponent<BaseDiceScript>().myFace == 1)
            {
                MayaOnes++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 2)
            {
                MayaTwos++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 3)
            {
                MayaThrees++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 4)
            {
                MayaFours++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 5)
            {
                MayaFives++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 6)
            {
                MayaSixes++;
            }
        }

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }
        StartCoroutine(Pause());

        //starts
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            float randFirst = Random.value;
            if (randFirst <= 0.33f || myBidDice.Length + mayasDice.Length < 4)
            {
                if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
                    Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 5);
                    Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 4);
                    Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 3);
                    Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 2);
                    Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }
            else if (randFirst <= 0.83f)
            {
                if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 6);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 5);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 4);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 3);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 2);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 1);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                int rand = Random.Range(1, 7);
                if (Random.value < 0.5f || myBidDice.Length + mayasDice.Length < 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, rand);
                    Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, rand);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }


        }

        if (currentBidNumber == 1)
        {
            if (myOnes + MayaOnes < currentBidAmount)
            {
                if (Random.value < 0.4f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else if (currentBidAmountDifference != 0 && currentBidNumberDifference > 0)
                {
                    if (Random.value < 0.5f)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    JacobFinalBid();
                }
            }
            else if (currentBidAmountDifference > 0 && currentBidNumberDifference > 0)
            {
                if (Random.value < 0.5f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                JacobFinalBid();
            }

        }
        else if (currentBidNumber == 2)
        {
            if (myTwos + MayaTwos < currentBidAmount)
            {
                if (Random.value < 0.4f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else if (currentBidAmountDifference != 0 && currentBidNumberDifference > 0)
                {
                    if (Random.value < 0.5f)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    JacobFinalBid();
                }
            }
            else if (currentBidAmountDifference > 0 && currentBidNumberDifference > 0)
            {
                if (Random.value < 0.5f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                JacobFinalBid();
            }

        }
        else if (currentBidNumber == 3)
        {
            if (myThrees + MayaThrees < currentBidAmount)
            {
                if (Random.value < 0.4f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else if (currentBidAmountDifference != 0 && currentBidNumberDifference > 0)
                {
                    if (Random.value < 0.5f)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    JacobFinalBid();
                }
            }
            else if (currentBidAmountDifference > 0 && currentBidNumberDifference > 0)
            {
                if (Random.value < 0.5f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                JacobFinalBid();
            }

        }
        else if (currentBidNumber == 4)
        {
            if (myFours + MayaFours < currentBidAmount)
            {
                if (Random.value < 0.4f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else if (currentBidAmountDifference != 0 && currentBidNumberDifference > 0)
                {
                    if (Random.value < 0.5f)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    JacobFinalBid();
                }
            }
            else if (currentBidAmountDifference > 0 && currentBidNumberDifference > 0)
            {
                if (Random.value < 0.5f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                JacobFinalBid();
            }

        }
        else if (currentBidNumber == 5)
        {
            if (myFives + MayaFives < currentBidAmount)
            {
                if (Random.value < 0.4f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else if (currentBidAmountDifference != 0 && currentBidNumberDifference > 0)
                {
                    if (Random.value < 0.5f)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    JacobFinalBid();
                }
            }
            else if (currentBidAmountDifference > 0 && currentBidNumberDifference > 0)
            {
                if (Random.value < 0.5f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                JacobFinalBid();
            }

        }
        else if (currentBidNumber == 6)
        {
            if (mySixes + MayaSixes < currentBidAmount)
            {
                if (Random.value < 0.4f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else if (currentBidAmountDifference != 0 && currentBidNumberDifference > 0)
                {
                    if (Random.value < 0.5f)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else
                {
                    JacobFinalBid();
                }
            }
            else if (currentBidAmountDifference > 0 && currentBidNumberDifference > 0)
            {
                if (Random.value < 0.5f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                JacobFinalBid();
            }

        }

        myOnes = 0;
        myTwos = 0;
        myThrees = 0;
        myFours = 0;
        myFives = 0;
        mySixes = 0;
        MayaOnes = 0;
        MayaTwos = 0;
        MayaThrees = 0;
        MayaFours = 0;
        MayaFives = 0;
        MayaSixes = 0;
    }

    void JacobFinalBid()
    {
        float rand = Random.value;
        //+1 amount rand
        if (rand < 0.2f)
        {
            int randNumb = Random.Range(1, 7);
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, randNumb);
            for (int i = 0; i < currentBidAmount + 1; i++)
            {
                Instantiate(myDice[randNumb - 1], boxPos.position, Quaternion.identity);
            }
        }
        //bids absolute highest
        else if (rand < 0.4f)
        {
            int temp1 = MayaOnes + myOnes;
            int temp2 = MayaTwos + myTwos;
            int temp3 = MayaThrees + myThrees;
            int temp4 = MayaFours + myFours;
            int temp5 = MayaFives + myFives;
            int temp6 = MayaSixes + mySixes;

            //ok sunday jonah here's the issue here: so basically we need to add in a clause that says "only if that bid is legal"

            if (temp6 >= temp5 && temp6 >= temp4 && temp6 >= temp3 && temp6 >= temp2 && temp6 >= temp1)
            {
                if (temp6 > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp6, 6);
                    for (int i = 0; i < temp6; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (currentBidNumber == 6)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 6);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (temp5 >= temp6 && temp5 >= temp4 && temp5 >= temp3 && temp5 >= temp2 && temp5 >= temp1)
            {
                if (temp5 > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp5, 5);
                    for (int i = 0; i < temp5; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (currentBidNumber == 5 || currentBidNumber == 6)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (temp4 >= temp6 && temp4 >= temp5 && temp4 >= temp3 && temp4 >= temp2 && temp4 >= temp1)
            {
                if (temp4 > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp4, 4);
                    for (int i = 0; i < temp4; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (currentBidNumber >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (temp3 >= temp6 && temp3 >= temp5 && temp3 >= temp4 && temp3 >= temp2 && temp3 >= temp1)
            {
                if (temp3 > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp3, 3);
                    for (int i = 0; i < temp3; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (currentBidNumber >= 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (temp2 >= temp6 && temp2 >= temp5 && temp2 >= temp4 && temp2 >= temp3 && temp2 >= temp1)
            {
                if (temp2 > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp2, 2);
                    for (int i = 0; i < temp2; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (currentBidNumber >= 2)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                if (temp1 > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp1, 1);
                    for (int i = 0; i < temp1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        //alternates between two pre-picked numbs
        else if (rand < 0.6f)
        {
            if (randSwitcher)
            {
                if (rand1 <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand1 - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand1);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand1 - 1], boxPos.position, Quaternion.identity);
                    }
                }
                randSwitcher = false;
            }
            else
            {
                if (rand2 <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand2 - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand2 - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        //next highest it has
        else
        {
            if (currentBidNumber == 1)
            {
                if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 2)
            {
                if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 3)
            {
                if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 4)
            {
                if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 5)
            {
                if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 6)
            {
                if (myOnes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
    }

    public void TurnAi7()
    {
        turnNumber++;
        if (bluffingNumb == 0)
        {
            bluffingNumb = Random.Range(1, 7);
        }
        myBidDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        mayasDice = GameObject.FindGameObjectsWithTag("Dice");
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }

        StartCoroutine(Pause());

        //First bid: one rand
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            int rand = Random.Range(1, 7);
            if (myBidDice.Length + mayasDice.Length < 4)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, rand);
                Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, rand);
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                }
            }
        }
        else if (currentBidNumber == bluffingNumb && currentBidAmount >= 3)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmount >= 4 && currentBidNumber == 1 && myOnes <= 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmount >= 4 && currentBidNumber == 2 && myTwos <= 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmount >= 4 && currentBidNumber == 3 && myThrees <= 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmount >= 4 && currentBidNumber == 4 && myFours <= 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmount >= 4 && currentBidNumber == 5 && myFives <= 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmount >= 4 && currentBidNumber == 6 && mySixes <= 1)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (isBluffing)
        {
            if (bluffingNumb <= currentBidNumber)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                }
            }
            isBluffing = false;
        }
        else
        {
            if (currentBidNumber == 1)
            {
                if (myTwos >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 2)
            {
                if (myThrees >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 3)
            {
                if (myFours >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 4)
            {
                if (myFives >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mySixes >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 5)
            {
                if (mySixes >= currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myOnes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                if (myOnes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
            }
            isBluffing = true;
        }

        myOnes = 0;
        myTwos = 0;
        myThrees = 0;
        myFours = 0;
        myFives = 0;
        mySixes = 0;
    }

    public void TurnAi8()
    {
        turnNumber++;
        if (bluffingNumb == 0)
        {
            bluffingNumb = Random.Range(1, 7);
        }
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        int increaseAmount = (currentBidAmountDifference * 6) + currentBidNumberDifference;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }

        StartCoroutine(Pause());

        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            if (mySixes > 0)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
                Instantiate(myDice[5], boxPos.position, Quaternion.identity);
            }
            else if (myFives > 0)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 5);
                Instantiate(myDice[4], boxPos.position, Quaternion.identity);
            }
            else if (myFours > 0)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 4);
                Instantiate(myDice[3], boxPos.position, Quaternion.identity);
            }
            else if (myThrees > 0)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 3);
                Instantiate(myDice[2], boxPos.position, Quaternion.identity);
            }
            else if (myTwos > 0)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 2);
                Instantiate(myDice[1], boxPos.position, Quaternion.identity);
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
                Instantiate(myDice[0], boxPos.position, Quaternion.identity);
            }
        }
        else if (currentBidNumber == bluffingNumb && turnNumber >= 3)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmountDifference >= 2 && turnNumber >= 2)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidNumber == bluffingNumb && currentBidAmount >= 4)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (increaseAmount <= 3)
        {
            if (bluffingNumb <= currentBidNumber)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                }
            }
        }
        else
        {
            if (mySixes > currentBidAmount)
            {
                if (currentBidNumber == 6)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 6);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myFives > currentBidAmount)
            {
                if (currentBidNumber >= 5)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myFours > currentBidAmount)
            {
                if (currentBidNumber >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myThrees > currentBidAmount)
            {
                if (currentBidNumber >= 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myTwos > currentBidAmount)
            {
                if (currentBidNumber >= 2)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myOnes > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }

        myOnes = 0;
        myTwos = 0;
        myThrees = 0;
        myFours = 0;
        myFives = 0;
        mySixes = 0;
    }

    public void TurnAi9()
    {
        turnNumber++;
        if (bluffingNumb == 0)
        {
            bluffingNumb = Random.Range(1, 7);
        }
        if (bluffingNumb2 == 0)
        {
            bluffingNumb2 = Random.Range(1, 7);
        }
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        int increaseAmount = (currentBidAmountDifference * 6) + currentBidNumberDifference;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }

        StartCoroutine(Pause());

        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            int rand = Random.Range(1, 7);
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, rand);
            Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
        }
        else if (increaseAmount == 1 && turnNumber >= 3)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (bluffingNumb == currentBidNumber)
        {
            if (bluffingNumb == 6)
            {
                if (currentBidAmount >= mySixes + 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    if (increaseAmount <= 3)
                    {
                        if (bluffingNumb <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                    else
                    {
                        if (bluffingNumb2 <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                }

            }
            else if (bluffingNumb == 5)
            {
                if (currentBidAmount >= myFives + 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    if (increaseAmount <= 3)
                    {
                        if (bluffingNumb <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                    else
                    {
                        if (bluffingNumb2 <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                }
            }
            else if (bluffingNumb == 4)
            {
                if (currentBidAmount >= myFours + 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    if (increaseAmount <= 3)
                    {
                        if (bluffingNumb <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                    else
                    {
                        if (bluffingNumb2 <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                }
            }
            else if (bluffingNumb == 3)
            {
                if (currentBidAmount >= myThrees + 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    if (increaseAmount <= 3)
                    {
                        if (bluffingNumb <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                    else
                    {
                        if (bluffingNumb2 <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                }
            }
            else if (bluffingNumb == 2)
            {
                if (currentBidAmount >= myTwos + 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    if (increaseAmount <= 3)
                    {
                        if (bluffingNumb <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                    else
                    {
                        if (bluffingNumb2 <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                }
            }
            else
            {
                if (currentBidAmount >= myOnes + 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
                }
                else
                {
                    if (increaseAmount <= 3)
                    {
                        if (bluffingNumb <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                    else
                    {
                        if (bluffingNumb2 <= currentBidNumber)
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount + 1; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                        else
                        {
                            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb2);
                            for (int i = 0; i < currentBidAmount; i++)
                            {
                                Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (increaseAmount <= 3)
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                if (bluffingNumb2 <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb2 - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
    }

    public void TurnAmelia()
    {
        turnNumber++;
        myBidDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        mayasDice = GameObject.FindGameObjectsWithTag("Dice");
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        int increaseAmount = (currentBidAmountDifference * 6) + currentBidNumberDifference;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().battleDice)
        {
            if (d.GetComponent<BaseDiceScript>().myFace == 1)
            {
                MayaOnes++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 2)
            {
                MayaTwos++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 3)
            {
                MayaThrees++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 4)
            {
                MayaFours++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 5)
            {
                MayaFives++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 6)
            {
                MayaSixes++;
            }
        }

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }
        if (bluffingNumb == 0)
        {
            if (mySixes == 0)
            {
                bluffingNumb = 6;
            }
            else if (myFives == 0)
            {
                bluffingNumb = 5;
            }
            else if (myFours == 0)
            {
                bluffingNumb = 4;
            }
            else if (myThrees == 0)
            {
                bluffingNumb = 3;
            }
            else if (myTwos == 0)
            {
                bluffingNumb = 2;
            }
            else
            {
                bluffingNumb = 1;
            }
        }

        StartCoroutine(Pause());

        //starts
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            float rand = Random.value;

            if (mySixes >= myFives && mySixes >= myFours && mySixes >= myThrees && mySixes >= myTwos && mySixes >= myOnes)
            {
                if (rand <= 0.5f && myBidDice.Length + mayasDice.Length >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 6);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(mySixes, 6);
                    for (int i = 0; i < mySixes; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myFives >= mySixes && myFives >= myFours && myFives >= myThrees && myFives >= myTwos && myFives >= myOnes)
            {
                if (rand <= 0.5f && myBidDice.Length + mayasDice.Length >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 5);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myFives, 5);
                    for (int i = 0; i < myFives; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myFours >= mySixes && myFours >= myFives && myFours >= myThrees && myFours >= myTwos && myFours >= myOnes)
            {
                if (rand <= 0.5f && myBidDice.Length + mayasDice.Length >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 4);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myFours, 4);
                    for (int i = 0; i < myFours; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myThrees >= mySixes && myThrees >= myFives && myThrees >= myFours && myThrees >= myTwos && myThrees >= myOnes)
            {
                if (rand <= 0.5f && myBidDice.Length + mayasDice.Length >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 3);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myThrees, 3);
                    for (int i = 0; i < myThrees; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (myTwos >= mySixes && myTwos >= myFives && myTwos >= myFours && myTwos >= myThrees && myTwos >= myOnes)
            {
                if (rand <= 0.5f && myBidDice.Length + mayasDice.Length >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 2);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myTwos, 2);
                    for (int i = 0; i < myTwos; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                if (rand <= 0.5f && myBidDice.Length + mayasDice.Length >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 1);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myOnes, 1);
                    for (int i = 0; i < myOnes; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (Random.value <= 0.5f && currentBidAmount >= 3 && currentBidNumber == bluffingNumb)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (increaseAmount <= 3 && currentBidAmount >= 3)
        {
            if (Random.value <= 0.6f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }

                }
            }
        }
        else if (Random.value <= 0.5f && currentBidAmount >= 3 && currentBidNumber == bluffingNumb && turnNumber >= 3)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidNumber == 1)
        {
            if (Random.value <= 0.4f && MayaOnes + myOnes > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (Random.value <= 0.8f)
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }

                }
            }
            else
            {
                int rand = Random.Range(1, 7);
                if (rand <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidNumber == 2)
        {
            if (Random.value <= 0.4f && MayaTwos + myTwos > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (Random.value <= 0.8f)
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }

                }
            }
            else
            {
                int rand = Random.Range(1, 7);
                if (rand <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidNumber == 3)
        {
            if (Random.value <= 0.4f && MayaThrees + myThrees > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (Random.value <= 0.8f)
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }

                }
            }
            else
            {
                int rand = Random.Range(1, 7);
                if (rand <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidNumber == 4)
        {
            if (Random.value <= 0.4f && MayaFours + myFours > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (Random.value <= 0.8f)
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }

                }
            }
            else
            {
                int rand = Random.Range(1, 7);
                if (rand <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidNumber == 5)
        {
            if (Random.value <= 0.4f && MayaFives + myFives > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (Random.value <= 0.8f)
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }

                }
            }
            else
            {
                int rand = Random.Range(1, 7);
                if (rand <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            if (Random.value <= 0.4f && MayaSixes + mySixes > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (Random.value <= 0.8f)
            {
                if (bluffingNumb <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                    }

                }
            }
            else
            {
                int rand = Random.Range(1, 7);
                if (rand <= currentBidNumber)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, rand);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, rand);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }

        myOnes = 0;
        myTwos = 0;
        myThrees = 0;
        myFours = 0;
        myFives = 0;
        mySixes = 0;
        MayaOnes = 0;
        MayaTwos = 0;
        MayaThrees = 0;
        MayaFours = 0;
        MayaFives = 0;
        MayaSixes = 0;

        if (currentBidNumber == bluffingNumb)
        {
            bluffingNumb = Random.Range(1, 7);
        }
    }

    public void TurnAi10()
    {
        turnNumber++;
        myBidDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        mayasDice = GameObject.FindGameObjectsWithTag("Dice");
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }

        StartCoroutine(Pause());

        //First bid: one rand
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            if (myBidDice.Length + mayasDice.Length >= 4)
            {
                if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 6);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 5);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 4);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 3);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 2);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 1);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                if (mySixes > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
                    Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                }
                else if (myFives > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 5);
                    Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                }
                else if (myFours > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 4);
                    Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                }
                else if (myThrees > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 3);
                    Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                }
                else if (myTwos > 0)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 2);
                    Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }
        }
        else if (currentBidAmount >= 4)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidNumberDifference >= 2 && currentBidAmountDifference >= 2 && turnNumber >= 2)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else
        {
            if (currentBidNumber == 6)
            {
                if (mySixes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 6);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 5)
            {
                if (myFives > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 4)
            {
                if (myFours > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 3)
            {
                if (myThrees > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (currentBidNumber == 2)
            {
                if (myTwos > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else
            {
                if (myOnes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
    }

    public void TurnAi11()
    {
        turnNumber++;
        myBidDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        mayasDice = GameObject.FindGameObjectsWithTag("Dice");
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        MayaBids.Add(currentBidNumber);

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }

        StartCoroutine(Pause());

        //First bid: one rand
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            int rand = Random.Range(1, 7);
            if (myBidDice.Length + mayasDice.Length >= 4)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, rand);
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, rand);
                Instantiate(myDice[rand - 1], boxPos.position, Quaternion.identity);
            }
        }
        else if (currentBidAmount >= 5)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (turnNumber > 3 && !MayaBids.Contains(currentBidNumber))
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidNumber == 6)
        {
            if (mySixes + GameObject.Find("BattleManager").GetComponent<BattleManager>().myDice < currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (mySixes > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 6);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }
        }
        else if (currentBidNumber == 5)
        {
            if (myFives + GameObject.Find("BattleManager").GetComponent<BattleManager>().myDice < currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myFives > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                }
            }
        }
        else if (currentBidNumber == 4)
        {
            if (myFours + GameObject.Find("BattleManager").GetComponent<BattleManager>().myDice < currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myFours > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                }
            }
        }
        else if (currentBidNumber == 3)
        {
            if (myThrees + GameObject.Find("BattleManager").GetComponent<BattleManager>().myDice < currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myThrees > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                }
            }
        }
        else if (currentBidNumber == 2)
        {
            if (myTwos + GameObject.Find("BattleManager").GetComponent<BattleManager>().myDice < currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myTwos > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                }
            }
        }
        else
        {
            if (myOnes + GameObject.Find("BattleManager").GetComponent<BattleManager>().myDice < currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myOnes > currentBidAmount)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                for (int i = 0; i < currentBidAmount + 1; i++)
                {
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                for (int i = 0; i < currentBidAmount; i++)
                {
                    Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                }
            }
            //calls if impossible given its hand
            //+1 amount if it has; +1 # if it doesn't (copy/paste from last ai enemy)
        }
    }

    public void TurnAi12()
    {
        turnNumber++;
        myBidDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        mayasDice = GameObject.FindGameObjectsWithTag("Dice");
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        bool mostOnes = false;
        bool mostTwos = false;
        bool mostThrees = false;
        bool mostFours = false;
        bool mostFives = false;
        bool mostSixes = false;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }

        StartCoroutine(Pause());

        //First bid: one rand
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            if (myBidDice.Length + mayasDice.Length >= 4)
            {
                if (mySixes >= myFives && mySixes >= myFours && mySixes >= myThrees && mySixes >= myTwos && mySixes >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 6);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                    mostSixes = true;
                }
                else if (myFives >= mySixes && myFives >= myFours && myFives >= myThrees && myFives >= myTwos && myFives >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 5);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                    mostFives = true;
                }
                else if (myFours >= mySixes && myFours >= myFives && myFours >= myThrees && myFours >= myTwos && myFours >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 4);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                    mostFours = true;
                }
                else if (myThrees >= mySixes && myThrees >= myFives && myThrees >= myFours && myThrees >= myTwos && myThrees >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 3);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                    mostThrees = true;
                }
                else if (myTwos >= mySixes && myTwos >= myFives && myTwos >= myFours && myTwos >= myThrees && myTwos >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 2);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                    mostTwos = true;
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 1);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                    mostOnes = true;
                }
            }
            else
            {
                if (mySixes >= myFives && mySixes >= myFours && mySixes >= myThrees && mySixes >= myTwos && mySixes >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
                    Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    mostSixes = true;
                }
                else if (myFives >= mySixes && myFives >= myFours && myFives >= myThrees && myFives >= myTwos && myFives >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 5);
                    Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    mostFives = true;
                }
                else if (myFours >= mySixes && myFours >= myFives && myFours >= myThrees && myFours >= myTwos && myFours >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 4);
                    Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    mostFours = true;
                }
                else if (myThrees >= mySixes && myThrees >= myFives && myThrees >= myFours && myThrees >= myTwos && myThrees >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 3);
                    Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    mostThrees = true;
                }
                else if (myTwos >= mySixes && myTwos >= myFives && myTwos >= myFours && myTwos >= myThrees && myTwos >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 2);
                    Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    mostTwos = true;
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 1);
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    mostOnes = true;
                }
            }
        }
        else if (currentBidAmount >= 4)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidNumber == 6)
        {
            if (currentBidAmount > mySixes + 2)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                if (mySixes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 6);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostSixes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 6);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFives)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFours)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostThrees)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostTwos)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidNumber == 5)
        {
            if (currentBidAmount > myFives + 2)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                if (myFives > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostSixes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFives)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFours)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostThrees)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostTwos)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidNumber == 4)
        {
            if (currentBidAmount > myFours + 2)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                if (myFours > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostSixes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFives)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFours)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostThrees)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostTwos)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidNumber == 3)
        {
            if (currentBidAmount > myThrees + 2)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                if (myThrees > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostSixes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFives)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFours)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostThrees)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostTwos)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidNumber == 2)
        {
            if (currentBidAmount > myTwos + 2)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                if (myTwos > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostSixes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFives)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFours)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostThrees)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostTwos)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            if (currentBidAmount > myOnes + 2)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                if (myOnes > currentBidAmount)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostSixes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFives)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostFours)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostThrees)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (mostTwos)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
    }

    public void TurnSimon()
    {
        turnNumber++;
        myBidDice = GameObject.FindGameObjectsWithTag("EnemyDice");
        mayasDice = GameObject.FindGameObjectsWithTag("Dice");
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        int increaseAmount = (currentBidAmountDifference * 6) + currentBidNumberDifference;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().battleDice)
        {
            if (d.GetComponent<BaseDiceScript>().myFace == 1)
            {
                MayaOnes++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 2)
            {
                MayaTwos++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 3)
            {
                MayaThrees++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 4)
            {
                MayaFours++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 5)
            {
                MayaFives++;
            }
            else if (d.GetComponent<BaseDiceScript>().myFace == 6)
            {
                MayaSixes++;
            }
        }

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                myOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                myTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                myThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                myFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                myFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
            {
                mySixes++;
            }
        }
        if (bluffingNumb == 0)
        {
            if (mySixes == 0)
            {
                bluffingNumb = 6;
            }
            else if (myFives == 0)
            {
                bluffingNumb = 5;
            }
            else if (myFours == 0)
            {
                bluffingNumb = 4;
            }
            else if (myThrees == 0)
            {
                bluffingNumb = 3;
            }
            else if (myTwos == 0)
            {
                bluffingNumb = 2;
            }
            else
            {
                bluffingNumb = 1;
            }
        }

        StartCoroutine(Pause());

        //starts
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            float rand = Random.value;
            if (rand <= 0.25f && myBidDice.Length + mayasDice.Length >= 4)
            {
                if (mySixes >= myFives && mySixes >= myFours && mySixes >= myThrees && mySixes >= myTwos && mySixes >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 6);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives >= mySixes && myFives >= myFours && myFives >= myThrees && myFives >= myTwos && myFives >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 5);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours >= mySixes && myFours >= myFives && myFours >= myThrees && myFours >= myTwos && myFours >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 4);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees >= mySixes && myThrees >= myFives && myThrees >= myFours && myThrees >= myTwos && myThrees >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 3);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos >= mySixes && myTwos >= myFives && myTwos >= myFours && myTwos >= myThrees && myTwos >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 2);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 1);
                    for (int i = 0; i < 2; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
            else if (rand <= 0.5f && myBidDice.Length + mayasDice.Length >= 4)
            {
                int rand2 = Random.Range(1, 7);
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, rand2);
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(myDice[rand2 - 1], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                if (mySixes >= myFives && mySixes >= myFours && mySixes >= myThrees && mySixes >= myTwos && mySixes >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(mySixes, 6);
                    for (int i = 0; i < mySixes; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFives >= mySixes && myFives >= myFours && myFives >= myThrees && myFives >= myTwos && myFives >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myFives, 5);
                    for (int i = 0; i < myFives; i++)
                    {
                        Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myFours >= mySixes && myFours >= myFives && myFours >= myThrees && myFours >= myTwos && myFours >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myFours, 4);
                    for (int i = 0; i < myFours; i++)
                    {
                        Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myThrees >= mySixes && myThrees >= myFives && myThrees >= myFours && myThrees >= myTwos && myThrees >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myThrees, 3);
                    for (int i = 0; i < myThrees; i++)
                    {
                        Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                    }
                }
                else if (myTwos >= mySixes && myTwos >= myFives && myTwos >= myFours && myTwos >= myThrees && myTwos >= myOnes)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myTwos, 2);
                    for (int i = 0; i < myTwos; i++)
                    {
                        Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(myOnes, 1);
                    for (int i = 0; i < myOnes; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
            }
        }
        else if (currentBidAmountDifference >= 3)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmountDifference >= 2 && turnNumber >= 2)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidNumber == 6)
        {
            if (mySixes + MayaSixes < currentBidAmount && increaseAmount <= 3 && Random.value <= 0.7f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (mySixes + MayaSixes < currentBidAmount && Random.value <= 0.55f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                float randomVal = Random.value;
                if (randomVal <= 0.5f)
                {
                    if (myOnes >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myTwos >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myThrees >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFours >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFives >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 6);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (randomVal <= 0.75f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                    for (int i = 0; i < currentBidAmount + 1; i++)
                    {
                        Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    if (bluffingNumb <= currentBidNumber)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
        else if (currentBidNumber == 5)
        {
            if (myFives + MayaFives < currentBidAmount && increaseAmount <= 3 && Random.value <= 0.7f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myFives + MayaFives < currentBidAmount && Random.value <= 0.55f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                float randomVal = Random.value;
                if (randomVal <= 0.5f)
                {
                    if (mySixes >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myOnes >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myTwos >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myThrees >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFours >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (randomVal <= 0.75f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    if (bluffingNumb <= currentBidNumber)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
        else if (currentBidNumber == 4)
        {
            if (myFours + MayaFours < currentBidAmount && increaseAmount <= 3 && Random.value <= 0.7f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myFours + MayaFours < currentBidAmount && Random.value <= 0.55f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                float randomVal = Random.value;
                if (randomVal <= 0.5f)
                {
                    if (myFives >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (mySixes >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myOnes >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myTwos >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myThrees >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (randomVal <= 0.75f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    if (bluffingNumb <= currentBidNumber)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
        else if (currentBidNumber == 3)
        {
            if (myThrees + MayaThrees < currentBidAmount && increaseAmount <= 3 && Random.value <= 0.7f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myThrees + MayaThrees < currentBidAmount && Random.value <= 0.55f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                float randomVal = Random.value;
                if (randomVal <= 0.5f)
                {
                    if (myFours >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFives >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (mySixes >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myOnes >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myTwos >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (randomVal <= 0.75f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    if (bluffingNumb <= currentBidNumber)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
        else if (currentBidNumber == 2)
        {
            if (myTwos + MayaTwos < currentBidAmount && increaseAmount <= 3 && Random.value <= 0.7f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myTwos + MayaTwos < currentBidAmount && Random.value <= 0.55f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                float randomVal = Random.value;
                if (randomVal <= 0.5f)
                {
                    if (myThrees >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFours >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFives >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (mySixes >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myOnes >= currentBidAmount + 1)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (randomVal <= 0.75f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    if (bluffingNumb <= currentBidNumber)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
            }
        }
        else
        {
            if (myOnes + MayaOnes < currentBidAmount && increaseAmount <= 3 && Random.value <= 0.7f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else if (myOnes + MayaOnes < currentBidAmount && Random.value <= 0.55f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                float randomVal = Random.value;
                if (randomVal <= 0.5f)
                {
                    if (myTwos >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myThrees >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFours >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (myFives >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                        }
                    }
                    else if (mySixes >= currentBidAmount)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                        }
                    }
                }
                else if (randomVal <= 0.75f)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                    for (int i = 0; i < currentBidAmount; i++)
                    {
                        Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                    }
                }
                else
                {
                    if (bluffingNumb <= currentBidNumber)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, bluffingNumb);
                        for (int i = 0; i < currentBidAmount + 1; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, bluffingNumb);
                        for (int i = 0; i < currentBidAmount; i++)
                        {
                            Instantiate(myDice[bluffingNumb - 1], boxPos.position, Quaternion.identity);
                        }
                    }
                }
            }
        }

        myOnes = 0;
        myTwos = 0;
        myThrees = 0;
        myFours = 0;
        myFives = 0;
        mySixes = 0;
        MayaOnes = 0;
        MayaTwos = 0;
        MayaThrees = 0;
        MayaFours = 0;
        MayaFives = 0;
        MayaSixes = 0;
        bluffingNumb = 0;
    }
    IEnumerator Pause(){
        GameObject.Find("teacup").GetComponent<TeaCupScript>().destroyOthers = false;
        yield return new WaitForSeconds (1f);
        GameObject.Find("CurrentBidSlot").GetComponent<CurrentBidSlotScript>().shouldDestroy = true;
        yield return new WaitForSeconds (1f);
        GameObject.Find ("teacup").GetComponent<TeaCupScript>().destroyOthers = true;
        //GameObject.Find ("BattleManager").GetComponent<BattleManager>().myTurn = true;
        yield return new WaitForSeconds (1f);
        GameObject.Find ("teacup").GetComponent<TeaCupScript>().destroyOthers = false;
    }

    IEnumerator PauseToSpawn (){
        Debug.Log ("Hit coroutine to spawn");
        yield return new WaitForSeconds (0.4f);
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        for (int i = 0; i < currentBidAmount; i++) {
            float randX = Random.Range(-0.8f, 0.8f);
            float randY = Random.Range(-0.8f, 0.8f);
            Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
            //Instantiate(myDice[currentBidNumber - 1], new Vector2(boxPos.position.x + randX, boxPos.position.y + randY), Quaternion.identity);
        }
    }
}

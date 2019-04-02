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
    int bluffingNumb = 0;

    int rand1 = 0;
    int rand2 = 0;
    bool randSwitcher = true;

    public GameObject[] myDice;
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

    List<int> MayaBids;

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
    public void Turn (){

        currentBidAmount = GameObject.Find ("BattleManager").GetComponent<BattleManager>().currentBidAmount;
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
        else if (currentBidAmountDifference >= 2 && turnNumber > 1 || currentBidAmount >= 4)
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
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        StartCoroutine(Pause());

        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            int rand = Random.Range(1, 7);
            if (Random.value < 0.5f)
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
        bluffingNumb = Random.Range(1, 7);
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
            if (bluffingNumb >= currentBidNumber)
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
        currentBidAmountDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount - currentBidAmount;
        currentBidNumberDifference = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber - currentBidNumber;
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().battleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == 1)
            {
                MayaOnes++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 2)
            {
                MayaTwos++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 3)
            {
                MayaThrees++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 4)
            {
                MayaFours++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 5)
            {
                MayaFives++;
            }
            else if (d.GetComponent<EnemyDiceScript>().myFace == 6)
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
            if (randFirst <= 0.33f)
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
                if (Random.value < 0.5f)
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
            if (temp6 >= temp5 && temp6 >= temp4 && temp6 >= temp3 && temp6 >= temp2 && temp6 >= temp1)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp6, 6);
                for (int i = 0; i < temp6; i++)
                {
                    Instantiate(myDice[5], boxPos.position, Quaternion.identity);
                }
            }
            else if (temp5 >= temp6 && temp5 >= temp4 && temp5 >= temp3 && temp5 >= temp2 && temp5 >= temp1)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp5, 5);
                for (int i = 0; i < temp5; i++)
                {
                    Instantiate(myDice[4], boxPos.position, Quaternion.identity);
                }
            }
            else if (temp4 >= temp6 && temp4 >= temp5 && temp4 >= temp3 && temp4 >= temp2 && temp4 >= temp1)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp4, 4);
                for (int i = 0; i < temp4; i++)
                {
                    Instantiate(myDice[3], boxPos.position, Quaternion.identity);
                }
            }
            else if (temp3 >= temp6 && temp3 >= temp5 && temp3 >= temp4 && temp3 >= temp2 && temp3 >= temp1)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp3, 3);
                for (int i = 0; i < temp3; i++)
                {
                    Instantiate(myDice[2], boxPos.position, Quaternion.identity);
                }
            }
            else if (temp2 >= temp6 && temp2 >= temp5 && temp2 >= temp4 && temp2 >= temp3 && temp2 >= temp1)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp2, 2);
                for (int i = 0; i < temp2; i++)
                {
                    Instantiate(myDice[1], boxPos.position, Quaternion.identity);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(temp1, 1);
                for (int i = 0; i < temp1; i++)
                {
                    Instantiate(myDice[0], boxPos.position, Quaternion.identity);
                }
            }
        }
        //alternates between two pre-picked numbs
        else if (rand < 0.6f)
        {
            if (randSwitcher)
            {
                if (rand1 >= currentBidNumber)
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
                if (rand2 >= currentBidNumber)
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

    IEnumerator Pause(){
        yield return new WaitForSeconds (2f);
        GameObject.Find ("teacup").GetComponent<TeaCupScript>().destroyOthers = true;
        GameObject.Find ("BattleManager").GetComponent<BattleManager>().myTurn = true;
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

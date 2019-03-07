using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAiEnemy : MonoBehaviour {

    int counter = 0;
    int currentBidNumber;
    int currentBidAmount;

    public GameObject[] myDice;
    public Transform boxPos;

    public int enemyNumber;

    int myOnes = 0;
    int myTwos = 0;
    int myThrees = 0;
    int myFours = 0;
    int myFives = 0;
    int mySixes = 0;


	// Use this for initialization
	void Start () {
        //boxPos = GameObject.Find ("teacup").GetComponent<Transform>();
        boxPos = GameObject.Find("3DPrinter").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    public void Turn (){
        currentBidAmount = GameObject.Find ("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            int face = Random.Range(1, 7);
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, face);
            for (int i = 0; i < 1; i++)
            {
                Debug.Log("made another thing");
                Instantiate(myDice[face - 1], boxPos.position, Quaternion.identity);
            }
        } else if (currentBidAmount > 3){
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().EnemyCall();
        } else if (GameObject.Find ("BattleManager").GetComponent<BattleManager>().counter > 0){
            StartCoroutine(Pause());
            GameObject.Find ("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
            currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
            currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
            for (int i = 0; i < currentBidAmount; i++) {
                Debug.Log("made another thing");
                Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
            }
        } else {
            StartCoroutine(Pause());
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, currentBidNumber + 1);
            StartCoroutine (PauseToSpawn());
        }

    }

    public void TurnAi2()
    {
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == currentBidNumber)
            {
                counter++;
            }
        }
        if (currentBidAmount == 0 && currentBidNumber == 0)
        {
            StartCoroutine(Pause());
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
            //for (int i = 0; i < currentBidAmount; i++)
            //{
            //    Debug.Log("made another thing");
            //    Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
            //}
            Instantiate(myDice[0], boxPos.position, Quaternion.identity);
        }
        else if (counter == 0 && GameObject.Find("BattleManager").GetComponent<BattleManager>().turnCount > 2)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidNumber <= 3)
        {
            StartCoroutine(Pause());
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
            StartCoroutine(PauseToSpawn());
        }
        else if (currentBidNumber <= 3)
        {
            StartCoroutine(Pause());
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
            StartCoroutine(PauseToSpawn());
        }
        else
        {
            StartCoroutine(Pause());
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
            StartCoroutine(PauseToSpawn());
        }
        //else if (GameObject.Find("BattleManager").GetComponent<BattleManager>().counter > 0)
        //{
        //    StartCoroutine(Pause());
        //    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
        //    currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        //    currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;
        //    for (int i = 0; i < currentBidAmount; i++)
        //    {
        //        Debug.Log("made another thing");
        //        Instantiate(myDice[currentBidNumber - 1], boxPos.position, Quaternion.identity);
        //    }
        //}
        //else
        //{
        //    StartCoroutine(Pause());
        //    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, currentBidNumber + 1);
        //    StartCoroutine(PauseToSpawn());
        //}
    }
    public void TurnJulia()
    {
        currentBidAmount = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidAmount;
        currentBidNumber = GameObject.Find("BattleManager").GetComponent<BattleManager>().currentBidNumber;

        foreach (GameObject d in GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyBattleDice)
        {
            if (d.GetComponent<EnemyDiceScript>().myFace == currentBidNumber)
            {
                counter++;
            }
        }
        //if the bid is 0:
        //      - bids a 6 (25%)
        //      - bids her highest numb (50%)
        //      - bids two of her highest numb (25%)
        //if you bid a bid that given her hand is impossible
        //      - calls (100%)
        //if you bid two more on a number than what she has
        //      - calls (50%)
        //      - bids +1 (50%)
        //else
        //      - bids on her most dense number (100%)
        if (currentBidAmount == 0)
        {
            float rand = Random.value;
            if (rand < 0.25f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
            }
            else
            {
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
                int temp = Mathf.Max(myOnes, myTwos, myThrees, myFours, myFives, mySixes);
                if (rand < 0.75)
                {
                    if (temp == mySixes)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 6);
                    }
                    else if (temp == myFives)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 5);
                    }
                    else if (temp == myFours)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 4);
                    }
                    else if (temp == myThrees)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 3);
                    }
                    else if (temp == myTwos)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 2);
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(1, 1);
                    }
                }
                else
                {
                    if (temp == mySixes)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 6);
                    }
                    else if (temp == myFives)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 5);
                    }
                    else if (temp == myFours)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 4);
                    }
                    else if (temp == myThrees)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 3);
                    }
                    else if (temp == myTwos)
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 2);
                    }
                    else
                    {
                        GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(2, 1);
                    }
                }
            }

        }
        else if ((counter + GameObject.Find("BattleManager").GetComponent<BattleManager>().battleDice.Length) < currentBidAmount)
        {
            GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
        }
        else if (currentBidAmount > counter + 1)
        {
            if (Random.value < 0.5f)
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyCall();
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, currentBidNumber);
            }
        }
        else
        {
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
            int temp = Mathf.Max(myOnes, myTwos, myThrees, myFours, myFives, mySixes);
            if (temp == mySixes)
            {
                if (currentBidNumber == 6)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 6);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 6);
                }
            }
            else if (temp == myFives)
            {
                if (currentBidNumber >= 5)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 5);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 5);
                }
            }
            else if (temp == myFours)
            {
                if (currentBidNumber >= 4)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 4);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 4);
                }
            }
            else if (temp == myThrees)
            {
                if (currentBidNumber >= 3)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 3);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 3);
                }
            }
            else if (temp == myTwos)
            {
                if (currentBidNumber >= 2)
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 2);
                }
                else
                {
                    GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount, 2);
                }
            }
            else
            {
                GameObject.Find("BattleManager").GetComponent<BattleManager>().EnemyBid(currentBidAmount + 1, 1);
            }
        }
        myOnes = 0;
        myTwos = 0;
        myThrees = 0;
        myFours = 0;
        myFives = 0;
        mySixes = 0;
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

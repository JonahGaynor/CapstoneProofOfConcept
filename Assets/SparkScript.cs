using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SparkScript : MonoBehaviour {

    int diceToSpark = 5;

    bool moveDown = false;
    bool moveLeft = false;

    float targetY;
    float targetX;

    public Vector2[] startingPositions;

    public Sprite[] mySprites;
    int mySpriteNumber = 0;
    float timer = 0;
    float maxTime = 0.2f;

    public static int enemiesDefeated = 0;
   // bool haveMovedDown = false;

	// Use this for initialization
	void Start () {
       // StartCoroutine (SparkMe());
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<SpriteRenderer>().sprite = mySprites[mySpriteNumber];
        if (diceToSpark == 5)
        {
            targetY = -0.6f;
            targetX = -7.8f;
        } else if (diceToSpark == 4)
        {
            targetY = -0.2f;
            targetX = -7.1f;
        } else if (diceToSpark == 3)
        {
            targetY = 0.2f;
            targetX = -6.7f;
        } else if (diceToSpark == 2)
        {
            targetY = 0.6f;
            targetX = -7.6f;
        } else if (diceToSpark == 1)
        {
            targetY = 0.8f;
            targetX = -8.1f;
        }
        if (moveDown)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
        }
        if (moveLeft)
        {
            transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
        }
        if (moveDown || moveLeft)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                timer = 0;
                if (mySpriteNumber == 2)
                {
                    mySpriteNumber = 0;
                }
                else
                {
                    mySpriteNumber++;
                }
            }
        }
    }

    bool haveMovedDown() {
        if (transform.position.y < targetY)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    bool haveMovedLeft() {
        if (transform.position.x < targetX) {
            return true;
        }
        else
        {
            return false;
        }

    }

    public IEnumerator SparkMe() {
        //Debug.Log("Sparking now");
        this.GetComponent<SpriteRenderer>().enabled = true;
        moveDown = true;
        yield return new WaitUntil(haveMovedDown);
        moveDown = false;
        moveLeft = true;
        yield return new WaitUntil(haveMovedLeft);
        moveLeft = false;
        GameObject.Find("EnemyDice" + (diceToSpark - 1) + "").SetActive(false);
        GameObject.Find("enemyGreenWire" + diceToSpark + "").GetComponent<SpriteRenderer>().sortingOrder = 0;
        diceToSpark--;
        if (diceToSpark < 1)
        {
            enemiesDefeated++;
            yield return new WaitForSeconds(1f);
            if (enemiesDefeated == 1)
            {
                SceneManager.LoadScene("Production_Scene7");
            }
            else if (enemiesDefeated == 2)
            {
                SceneManager.LoadScene("Production_Scene8");
            }
            else if (enemiesDefeated == 3)
            {
                SceneManager.LoadScene("Production_Scene10");
            }
        }
        else
        {
            transform.position = startingPositions[diceToSpark - 1];
        }
        this.GetComponent<SpriteRenderer>().enabled = false;
        //StartCoroutine(SparkMe());
    }
}

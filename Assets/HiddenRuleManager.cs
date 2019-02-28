using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenRuleManager : MonoBehaviour {

    public static HiddenRuleManager _instance;

    public List<int> myPath;
    public int myNumb;
    public int lastMove;

    static int _playerDir;

    public Sprite[] teleportSprites;

    GameObject player;

    void Awake()
    {
        _instance = this;
        player = GameObject.Find("Player");
    }

    public static int playerDir
    {
        set
        {
            _playerDir = value;
            _instance.Check(value);
        }
    }

    public void Check(int step)
    {
        if (myPath.Count >= myNumb)
        {
            GameObject.Find("ScoreboardNumber").GetComponent<Image>().enabled = true;
            GameObject.Find("ScoreboardBase").GetComponent<Image>().enabled = true;
            GameObject.Find("ScoreboardNumberBase").GetComponent<Image>().enabled = true;
            lastMove = step;
            if (step == myPath[myNumb])
            {
                if (myNumb == myPath.Count - 1)
                {
                    Victory();
                }
                else
                {
                    myNumb++;
                    GameObject.Find("ScoreboardNumber").GetComponent<ScoreboardNumberScript>().NewNumber(myPath[myNumb]);
                }
            }
            else
            {
                Reset();
            }
        }
    }

    public void Victory()
    {
        GameObject.Find("Door").GetComponent<DoorSprites>().Open(0);
        GameObject.Find("ScoreboardNumber").SetActive(false);
        GameObject.Find("ScoreboardNumberBase").SetActive(false);
        GameObject.Find("ScoreboardBase").SetActive(false);
        GameObject.Find("ScoreboardReset").SetActive(false);
    }

    void Reset()
    {
        //TODO: shoot player
        myNumb = 0;
        GameObject.Find("ScoreboardReset").GetComponent<Image>().enabled = true;
        ShootPlayer();
    }

    void ShootPlayer()
    {
        StartCoroutine(ResetPause());
    }

    IEnumerator ResetPause()
    {
        player.GetComponent<PlayerMovementScript>().canMove = false;
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[0];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[1];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[2];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[3];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[4];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        player.transform.position = new Vector2(-1, -0.5f);
        player.GetComponent<PlayerMovementScript>().targetPos = player.transform.position;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[3];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[2];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[1];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[0];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[5];
        player.GetComponent<PlayerMovementScript>().canMove = true;
        GameObject.Find("ScoreboardReset").GetComponent<Image>().enabled = false;
        GameObject.Find("ScoreboardNumber").GetComponent<Image>().enabled = false;
        GameObject.Find("ScoreboardNumberBase").GetComponent<Image>().enabled = false;
        GameObject.Find("ScoreboardBase").GetComponent<Image>().enabled = false;
    }
}

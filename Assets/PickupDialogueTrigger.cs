using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDialogueTrigger : MonoBehaviour {

    public Vector2 myPos;
    bool isOn = false;
    bool haveStarted = false;
    float timer = 0f;
    bool startTimer = false;

    // Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            startTimer = true;
            timer = 0;
        }
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                startTimer = false;
            }
            if (Mathf.Abs(GameObject.Find("Player").transform.position.x - myPos.x) <= 1.1f)
            {
                isOn = true;
            }
            else if (Mathf.Abs(GameObject.Find("Player").transform.position.y - myPos.y) <= 1.1f)
            {
                isOn = true;
            }
            else
            {
                isOn = false;
            }
        }

        if (isOn)
        {
            if ((Vector2)GameObject.Find("Player").transform.position == myPos && !haveStarted)
            {
                //this.GetComponent<DialogueContinue>().enabled = true;
                haveStarted = true;
                this.GetComponent<SpriteRenderer>().enabled = false;
                this.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
	}
}

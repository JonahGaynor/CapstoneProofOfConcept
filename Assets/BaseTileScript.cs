using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTileScript : MonoBehaviour {

    Vector2 myPos;
    public bool isSteppedOn = false;

	// Use this for initialization
	void Start () {
        myPos = transform.position;
        this.GetComponent<SpriteRenderer>().sortingOrder -= (int)myPos.y;
	}

    public void Check()
    {
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);
        if (diffX < 0.1f)
        {
           // Debug.Log(diffY + "" + myPos);
        }
        if (diffX < 0.1f && diffY < 0.6f && transform.position.y < playerPos.y)
        {
            Debug.Log(myPos);
            isSteppedOn = true;
            transform.position = new Vector2(myPos.x, myPos.y - 0.075f);
        }
        //else if (diffX < 0.1f && diffY < 0.5f && isUp)
        //{

        //        isSteppedOn = true;
        //        transform.position = new Vector2(myPos.x, myPos.y - 0.1f);

        //}
        else
        {
            transform.position = myPos;
        }
    }
}

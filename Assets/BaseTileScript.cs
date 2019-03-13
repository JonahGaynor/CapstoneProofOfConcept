using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTileScript : MonoBehaviour {

    Vector2 myPos;
    Vector2 newPos;
    public bool isSteppedOn = false;
    bool lerp = false;

    private void Awake()
    {
        myPos = transform.position;
    }
    // Use this for initialization
    void Start () {
        newPos = new Vector2(myPos.x, myPos.y - 0.075f);
        this.GetComponent<SpriteRenderer>().sortingOrder -= (int)myPos.y;
	}

    private void Update()
    {
        if (transform.position.y < myPos.y && !lerp)
        {
            transform.position = Vector2.Lerp(transform.position, myPos, 0.5f);
            isSteppedOn = false;
        } if (lerp)
        {
            //Debug.Log(myPos);
            isSteppedOn = true;
            //transform.position = new Vector2(myPos.x, myPos.y - 0.075f);
            transform.position = Vector2.Lerp(transform.position, newPos, 0.5f);
        }
        if (isSteppedOn)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);

        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;

        }
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
            lerp = true;
        }
        else
        {
            lerp = false;
        }
        //else if (diffX < 0.1f && diffY < 0.5f && isUp)
        //{

        //        isSteppedOn = true;
        //        transform.position = new Vector2(myPos.x, myPos.y - 0.1f);

        //}

    }
}

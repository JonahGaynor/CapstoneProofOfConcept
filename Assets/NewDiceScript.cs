using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDiceScript : MonoBehaviour {

    public Sprite newDice;
    public bool toSpin = true;
    public GameObject myChild;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (toSpin)
        {
            Spin();
        }
	}

    void Spin()
    {
        Vector3 rotationVector = new Vector3(0, 0, Time.deltaTime * 30);
        //Quaternion rotation = Quaternion.Euler(rotationVector);
        //transform.rotation = Quaternion.Euler(rotationVector);
        transform.Rotate(rotationVector);
        //myChild.GetComponent<SpriteRenderer>().sprite = newDice;
        //myChild.GetComponent<Transform>().position = Vector2.Lerp(myChild.GetComponent<Transform>().position, Vector2.zero, 0.01f);
        //if (myChild.GetComponent<Transform>().position.y < -0.1f)
        //{
        //    myChild.GetComponent<Transform>().localScale = Vector3.Lerp(myChild.GetComponent<Transform>().localScale, new Vector3(8, 8, 8), 0.01f);
        //}
        //else if (myChild.GetComponent<Transform>().localScale.x < 12)
        //{
        //    myChild.GetComponent<Transform>().localScale = Vector3.Lerp(myChild.GetComponent<Transform>().localScale, new Vector3(13, 13, 13), 0.1f);

        //}
        //else if (myChild.GetComponent<Transform>().localScale.x > 12)
        //{
        //    myChild.GetComponent<Transform>().localScale = Vector3.Lerp(myChild.GetComponent<Transform>().localScale, new Vector3(10, 10, 10), 0.08f);
        //}
        //else
        //{
        //    myChild.GetComponent<Transform>().rotation = Quaternion.identity;
        //}

    }
}

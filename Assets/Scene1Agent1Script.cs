using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Agent1Script : MonoBehaviour {

	public bool active = false;
    bool haveStartedCoroutine = false;
    bool moveOne = false;
    bool moveTwo = false;
    bool moveThree = false;
    public Vector3 targetOnePos;
    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        targetOnePos = GameObject.Find ("Player").GetComponent<Transform>().position;
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (active && !haveStartedCoroutine){
            haveStartedCoroutine = true;
            StartCoroutine(MyCoroutine());
        }
        if (moveOne){
            transform.position = new Vector3 (transform.position.x - 0.05f, transform.position.y, 0);
            if (transform.position.x <= targetOnePos.x){
                moveOne = false;
            }
        }
        if (moveTwo){
            transform.position = new Vector3 (transform.position.x, transform.position.y + 0.05f, 0);
            if (transform.position.y >= targetOnePos.y - 1){
                moveTwo = false;
            }
        }
        if (moveThree)
        {
            if (transform.position.y > startingPos.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, 0);
            }
            if (transform.position.x >= startingPos.x && transform.position.y <= startingPos.y)
            {
                moveThree = false;
            }
        }
    }

    IEnumerator MyCoroutine(){
        yield return new WaitForSeconds (0.1f);
        moveOne = true;
        yield return new WaitForSeconds (5f);
        moveTwo = true;
        yield return new WaitForSeconds (6f);
        moveThree = true;
    }
}

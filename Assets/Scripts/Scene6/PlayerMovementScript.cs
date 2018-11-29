using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    Animator myAnimator;
   // Rigidbody2D rb;
    bool moveUp = false;
    bool moveLeft = false;
    bool moveRight = false;
    bool moveDown = false;

    Vector2 upPos;
    Vector2 leftPos;
    Vector2 rightPos;
    Vector2 downPos;
    Vector2 targetPos;

	// Use this for initialization
	//void Start () {
     //   myAnimator = this.GetComponent<Animator>();
       // rb = this.GetComponent<Rigidbody2D>();
	//}
	
	// Update is called once per frame
	void Update () {
        if (!moveUp && !moveLeft && !moveRight && !moveDown){
            if (Input.GetKeyUp(KeyCode.UpArrow)){
                moveUp = true;
                //myAnimator.SetTrigger("MoveUp");
                upPos = new Vector2 (transform.position.x, transform.position.y + 0.1f);
                targetPos = new Vector2 (transform.position.x, transform.position.y + 1f);
             //   rb.MovePosition(upPos);
            } else if (Input.GetKeyUp(KeyCode.LeftArrow)){
                moveLeft = true;
                leftPos = new Vector2 (transform.position.x - 0.1f, transform.position.y);
                targetPos = new Vector2 (transform.position.x - 1, transform.position.y);
            } else if (Input.GetKeyUp(KeyCode.RightArrow)){
                moveRight = true;
                rightPos = new Vector2 (transform.position.x + 0.1f, transform.position.y);
                targetPos = new Vector2 (transform.position.x + 1, transform.position.y);
            } else if (Input.GetKeyUp(KeyCode.DownArrow)){
                moveDown = true;
                downPos = new Vector2 (transform.position.x, transform.position.y - 0.1f);
                targetPos = new Vector2 (transform.position.x, transform.position.y - 1);
            }
        }


        if (Physics2D.OverlapPoint(targetPos) == null){
            if (moveUp){
                transform.position = upPos;
                upPos = new Vector2(transform.position.x, transform.position.y + 0.1f);
                if (transform.position.y >= targetPos.y){
                    moveUp = false;
                    targetPos = Vector2.zero;
                }
            } else if (moveLeft){
                transform.position = leftPos;
                leftPos = new Vector2(transform.position.x - 0.1f, transform.position.y);
                if (transform.position.x <= targetPos.x){
                    moveLeft = false;
                    targetPos = Vector2.zero;
                }
            } else if (moveRight){
                transform.position = rightPos;
                rightPos = new Vector2(transform.position.x + 0.1f, transform.position.y);
                if (transform.position.x >= targetPos.x){
                    moveRight = false;
                    targetPos = Vector2.zero;
                }
            } else if (moveDown){
                transform.position = downPos;
                downPos = new Vector2(transform.position.x, transform.position.y - 0.1f);
                if (transform.position.y <= targetPos.y){
                    moveDown = false;
                    targetPos = Vector2.zero;
                }
            }
        } else {
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
        }
    }
}

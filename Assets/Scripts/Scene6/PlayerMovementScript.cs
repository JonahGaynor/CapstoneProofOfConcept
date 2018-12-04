using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementScript : MonoBehaviour {

    //TODO: make input buffer

    Animator myAnimator;
   // Rigidbody2D rb;
    public bool moveUp = false;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool moveDown = false;
    public bool canMove = true;

    public bool stunned = false;

    public Tilemap walls;

    Vector2 upPos;
    Vector2 leftPos;
    Vector2 rightPos;
    Vector2 downPos;
    Vector2 targetPos;
    Vector2 checkWallPos;

    float xOffset;
    float yOffset;

	// Use this for initialization
	void Start () {
     //   myAnimator = this.GetComponent<Animator>();
       // rb = this.GetComponent<Rigidbody2D>();
        xOffset = GameObject.Find ("Grid").GetComponent<Transform>().position.x;
        yOffset = GameObject.Find ("Grid").GetComponent<Transform>().position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (!moveUp && !moveLeft && !moveRight && !moveDown && canMove){
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

        checkWallPos = new Vector2 (targetPos.x - xOffset, targetPos.y - yOffset);

        if (!walls.HasTile(Vector3Int.RoundToInt(targetPos))){
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

    void OnCollisionEnter2D(Collision2D collision) {
        if (stunned){
            stunned = false;
        }
        if (collision.gameObject.tag == "LeftTile" || collision.gameObject.tag == "RightTile" || collision.gameObject.tag == "UpTile" || collision.gameObject.tag == "DownTile"){
            stunned = true;
        } 
        //else if (collider.gameObject.tag == "Wall"){
        //    stunned = false;
        //}
    }
}

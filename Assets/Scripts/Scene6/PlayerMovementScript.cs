﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour {

    Scene activeScene;

    //TODO: make input buffer

    Animator myAnimator;
   // Rigidbody2D rb;
    public bool moveUp = false;
    public bool moveLeft = false;
    public bool moveRight = false;
    public bool moveDown = false;
    public bool canMove = true;

    public bool stunned = false;

    public AudioClip shootTileWhoosh;
    public AudioClip[] footsteps;
    AudioSource myAudio;

    public int myColor = 0;

    public Tilemap walls;

    public Sprite[] upSprites;
    public Sprite[] leftSprites;
    public Sprite[] rightSprites;
    public Sprite[] downSprites;
    public int spriteCounter = 0;
    float spriteCounterTimer = 0;

    Vector2 upPos;
    Vector2 leftPos;
    Vector2 rightPos;
    Vector2 downPos;
    public Vector2 targetPos;
    Vector2 checkWallPos;
    Vector2 fixedTargetPos;

    public List<Vector2> steppedOn;

    GameObject[] normalTiles;
    GameObject[] myDoors;
    GameObject[] upTiles;
    GameObject[] leftTiles;
    GameObject[] rightTiles;
    GameObject[] downTiles;
    GameObject[] blueBoxes;
    GameObject[] redBoxes;
    GameObject[] yellowBoxes;
    GameObject[] greenBoxes;

    public Sprite blueSprite;
    public Sprite redSprite;

    GameObject lastTile;

    bool moveToNext = false;
    public bool drawingWires = false;

    float xOffset;
    float yOffset;

    bool firstWallaceText = false;

    // Use this for initialization
    void Start () {
        targetPos = new Vector2(transform.position.x, transform.position.y);
        normalTiles = GameObject.FindGameObjectsWithTag("BaseTile");
        upTiles = GameObject.FindGameObjectsWithTag("TileUp");
        leftTiles = GameObject.FindGameObjectsWithTag("TileLeft");
        rightTiles = GameObject.FindGameObjectsWithTag("TileRight");
        downTiles = GameObject.FindGameObjectsWithTag("TileDown");
        lastTile = GameObject.FindGameObjectWithTag("TileFinal");
        blueBoxes = GameObject.FindGameObjectsWithTag("BlueBox");
        redBoxes = GameObject.FindGameObjectsWithTag("RedBox");
        yellowBoxes = GameObject.FindGameObjectsWithTag("YellowBox");
        greenBoxes = GameObject.FindGameObjectsWithTag("GreenBox");
        myAudio = GetComponent<AudioSource>();
     //   myAnimator = this.GetComponent<Animator>();
       // rb = this.GetComponent<Rigidbody2D>();
  //      xOffset = GameObject.Find ("Grid").GetComponent<Transform>().position.x;
    //    yOffset = GameObject.Find ("Grid").GetComponent<Transform>().position.y;
    }

    public void MoveUp()
    {
        moveUp = true;
        //myAnimator.SetTrigger("MoveUp");

        upPos = new Vector2(transform.position.x, transform.position.y + 0.1f);
        targetPos += Vector2.up;
        if (drawingWires && !Physics2D.OverlapPoint(new Vector2 (transform.position.x, transform.position.y + 0.5f)))
        {
            if (!steppedOn.Contains(new Vector2(targetPos.x, targetPos.y - 0.5f)))
            {
                steppedOn.Add(new Vector2(targetPos.x, targetPos.y - 0.5f));
                //Debug.Log("add");
            }
            else
            {
                //Debug.Log("remove");
                steppedOn.Remove(steppedOn[steppedOn.Count - 1]);
            }
        }
        if (!myAudio.isPlaying)
        {
            int rand = Random.Range(0, 5);
            myAudio.PlayOneShot(footsteps[rand], 0.3f);
        }
        //   rb.MovePosition(upPos);
    }
    public void MoveLeft()
    {

        //foreach (GameObject b in blueBoxes)
        //{
        //    float dist = Vector2.Distance((Vector2)b.transform.position, fixedTargetPos);
        //    if (dist < 0.1f)
        //    {
        //        b.GetComponent<WireBoxScript>().isOn = true;
        //    }
        //}
        moveLeft = true;
        leftPos = new Vector2(transform.position.x - 0.1f, transform.position.y);
        targetPos += Vector2.left;
        if (drawingWires && !Physics2D.OverlapPoint(new Vector2(transform.position.x - 1, transform.position.y - 0.5f)))
        {
            if (!steppedOn.Contains(new Vector2(targetPos.x, targetPos.y - 0.5f)))
            {
                steppedOn.Add(new Vector2(targetPos.x, targetPos.y - 0.5f));
                //Debug.Log("add");
            }
            else
            {
                //Debug.Log("remove");
                steppedOn.Remove(steppedOn[steppedOn.Count - 1]);
            }
        }
        if (!myAudio.isPlaying)
        {
            int rand = Random.Range(0, 5);
            myAudio.PlayOneShot(footsteps[rand], 0.3f);
        }

    }
    public void MoveRight()
    {

        //foreach (GameObject b in blueBoxes)
        //{
        //    float dist = Vector2.Distance((Vector2)b.transform.position, fixedTargetPos);
        //    if (dist < 0.1f)
        //    {
        //        b.GetComponent<WireBoxScript>().isOn = true;
        //    }
        //}
        moveRight = true;
        rightPos = new Vector2(transform.position.x + 0.1f, transform.position.y);
        targetPos += Vector2.right;

        if (drawingWires && !Physics2D.OverlapPoint(new Vector2(transform.position.x + 1, transform.position.y - 0.5f)))
        {
            if (!steppedOn.Contains(new Vector2(targetPos.x, targetPos.y - 0.5f)))
            {
                steppedOn.Add(new Vector2(targetPos.x, targetPos.y - 0.5f));
                //Debug.Log("add");
            }
            else
            {
                //Debug.Log("remove");
                steppedOn.Remove(steppedOn[steppedOn.Count - 1]);
            }
        }
        if (!myAudio.isPlaying)
        {
            int rand = Random.Range(0, 5);
            myAudio.PlayOneShot(footsteps[rand], 0.3f);
        }

    }
    public void MoveDown()
    {

        //foreach (GameObject b in blueBoxes)
        //{
        //    float dist = Vector2.Distance((Vector2)b.transform.position, fixedTargetPos);
        //    if (dist < 0.1f)
        //    {
        //        b.GetComponent<WireBoxScript>().isOn = true;
        //    }
        //}
        moveDown = true;
        downPos = new Vector2(transform.position.x, transform.position.y - 0.1f);
        targetPos += Vector2.down;
        if (drawingWires && !Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y - 1.5f)))
        {
            if (!steppedOn.Contains(new Vector2(targetPos.x, targetPos.y - 0.5f)))
            {
                steppedOn.Add(new Vector2(targetPos.x, targetPos.y - 0.5f));
                //Debug.Log("add");
            }
            else
            {
                //Debug.Log("remove");
                steppedOn.Remove(steppedOn[steppedOn.Count - 1]);
            }
        }
        if (!myAudio.isPlaying)
        {
            int rand = Random.Range(0, 5);
            myAudio.PlayOneShot(footsteps[rand], 0.3f);
        }

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.R))
        {
            activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Physics2D.OverlapPoint(fixedTargetPos))
        {
            //Debug.Log(fixedTargetPos);
        }
        if (transform.position.y > lastTile.transform.position.y && !moveToNext)
        {
            moveToNext = true;
            StartCoroutine(GetComponentInChildren<PlayerOutAnimation>().GetOut());
        }

        fixedTargetPos = new Vector2(transform.position.x, transform.position.y - 0.5f);
        //if (transform.position.y >= -3 && !firstWallaceText) {
        //     canMove = false;
        //     firstWallaceText = true;
        //            GameObject.Find("WallaceSpeech1").GetComponent<SpriteRenderer>().enabled = true;
        //          GameObject.Find("EnterButton").GetComponent<SpriteRenderer>().enabled = true;
        //GameObject.Find("tempSpeechBubble").GetComponent<SpriteRenderer>().enabled = true;
        //GameObject.Find("TextFace").GetComponent<SpriteRenderer>().enabled = true;
        //GameObject.Find("FirstWallaceText").GetComponent<Text>().enabled = true;
        // }

        if (!moveUp && !moveLeft && !moveRight && !moveDown && canMove) {
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) {
                myAudio.Stop();
                MoveUp();
            } else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
                myAudio.Stop();
                MoveLeft();
            } else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
                myAudio.Stop();
                MoveRight();
            } else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
                myAudio.Stop();
                MoveDown();
            }
        }


        //checkWallPos = new Vector2(targetPos.x, targetPos.y);
        if (Physics2D.OverlapPoint(targetPos + (Vector2.down/2)))
        {
            //Debug.Log("WORK PLEASE");
            targetPos = new Vector2(transform.position.x, transform.position.y);
        }
        //if (moveUp && Physics2D.OverlapPoint(targetPos))
        //{
        //    Debug.Log("we out here");
        //   // myDoors = GameObject.FindGameObjectsWithTag("Door");
        //    //foreach (GameObject d in myDoors)
        //    //{

        //    //    if ((Vector2)d.transform.position == (Vector2)transform.position + Vector2.up)
        //    //    {
        //    //        d.GetComponent<DoorSprites>().myNumber = 1;
        //    //        d.GetComponent<BoxCollider2D>().enabled = false;
        //    //        //d.SetActive(false);
        //    //        // d.tag = "";
        //    //    }
        //    //    else
        //    //    {
        //    //        Debug.Log(d.transform.position);
        //    //        Debug.Log(targetPos);
        //    //    }
        //    //}
        //}
        if (!Physics2D.OverlapPoint(targetPos + (Vector2.down/2)) && (moveUp || moveLeft || moveRight || moveDown)) {
            //Debug.Log(targetPos);
             spriteCounterTimer += Time.deltaTime;
            if (spriteCounterTimer >= 0.08f)
            {
                spriteCounterTimer = 0;
                if (spriteCounter == 3)
                {
                    spriteCounter = 0;
                }
                else
                {
                    spriteCounter++;
                }
            }

            myDoors = GameObject.FindGameObjectsWithTag("Door");
            //foreach (GameObject d in myDoors) {
            //    if (!d.GetComponent<BoxCollider2D>().enabled) {
            //        //Debug.Log(d.transform.position);
            //        Destroy(d);
            //    }
            //}
        // if (!walls.HasTile(Vector3Int.RoundToInt(targetPos))){
            if (moveUp){
                this.GetComponent<SpriteRenderer>().sprite = upSprites[spriteCounter];

//                Debug.Log(targetPos);
                transform.position = upPos;
                upPos = new Vector2(transform.position.x, transform.position.y + 0.1f);


                if (transform.position.y >= targetPos.y){
                    foreach (GameObject tile in normalTiles)
                    {
                        if (steppedOn.Contains(tile.transform.position))
                        {
                            if (myColor == 1)
                            {
                                tile.GetComponent<SpriteRenderer>().sprite = blueSprite;
                            }
                            else
                            {
                                tile.GetComponent<SpriteRenderer>().sprite = redSprite;
                            }
                        }
                    }
                    foreach (GameObject tile in normalTiles)
                    {
                        tile.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject u in upTiles)
                    {
                        u.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject r in rightTiles)
                    {
                        r.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject d in downTiles)
                    {
                        d.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject l in leftTiles)
                    {
                        l.GetComponent<BaseTileScript>().Check();
                    }
                    transform.position = new Vector3(targetPos.x, targetPos.y, 0);
                    moveUp = false;
                    moveLeft = false;
                    moveRight = false;
                    moveDown = false;

                    if (HiddenRuleManager._instance != null && transform.position.y > -0.5f)
                    {
                        HiddenRuleManager.playerDir = 1;
                    }

                    //targetPos = new Vector2(transform.position.x, transform.position.y);
                    this.GetComponent<SpriteRenderer>().sprite = upSprites[0];
                    foreach (GameObject b in blueBoxes)
                    {
                        float dist = Vector2.Distance((Vector2)b.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            if (drawingWires && myColor == 1)
                            {
                                b.GetComponent<WireBoxScript>().isOn = true;
                                b.GetComponent<WireBoxScript>().Check();
                                drawingWires = false;
                                myColor = 0;
                                steppedOn.Clear();
                            }
                            else if (myColor == 0)
                            {
                                b.GetComponent<WireBoxScript>().isOn = true;
                                drawingWires = true;
                                myColor = 1;
                                if (!Physics2D.OverlapPoint(fixedTargetPos))
                                {
                                    steppedOn.Add(new Vector2(transform.position.x, transform.position.y - 0.5f));
                                }
                            }

                        }
                    }
                    foreach (GameObject r in redBoxes)
                    {
                        float dist = Vector2.Distance((Vector2)r.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            if (drawingWires && myColor == 2)
                            {
                                r.GetComponent<WireBoxScript>().isOn = true;
                                r.GetComponent<WireBoxScript>().Check();
                                drawingWires = false;
                                myColor = 0;
                                steppedOn.Clear();
                            }
                            else if (myColor == 0)
                            {
                                r.GetComponent<WireBoxScript>().isOn = true;
                                drawingWires = true;
                                myColor = 2;
                                if (!Physics2D.OverlapPoint(fixedTargetPos))
                                {
                                    steppedOn.Add(new Vector2(transform.position.x, transform.position.y - 0.5f));
                                }
                            }
                        }
                    }
                    foreach (GameObject u in upTiles)
                    {
                        float dist = Vector2.Distance((Vector2)u.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveUp();
                        }
                        //if ((Vector2)u.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveUp = true;
                        //    targetPos = new Vector2(transform.position.x, transform.position.y + 1);
                        //}               
                    }
                    foreach (GameObject l in leftTiles)
                    {
                        float dist = Vector2.Distance((Vector2)l.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveLeft();
                        }
                        //if ((Vector2)l.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveLeft = true;
                        //    targetPos = new Vector2(transform.position.x - 1, transform.position.y);
                        //}
                    }
                    foreach (GameObject r in rightTiles)
                    {
                        float dist = Vector2.Distance((Vector2)r.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveRight();
                        }
                        //if ((Vector2)r.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveRight = true;
                        //    targetPos = new Vector2(transform.position.x + 1, transform.position.y);
                        //}
                    }
                    foreach (GameObject d in downTiles)
                    {
                        float dist = Vector2.Distance((Vector2)d.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveDown();
                        }
                        //if ((Vector2)d.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveDown = true;
                        //    targetPos = new Vector2(transform.position.x, transform.position.y - 1);
                        //}
                    }

                    if (!moveUp && !moveLeft && !moveRight && !moveDown)
                    {
                        //targetPos = new Vector2(transform.position.x, transform.position.y);
                    }
                }
            } else if (moveLeft){
                this.GetComponent<SpriteRenderer>().sprite = leftSprites[spriteCounter];

                transform.position = leftPos;
                leftPos = new Vector2(transform.position.x - 0.1f, transform.position.y);
                foreach (GameObject tile in normalTiles)
                {
                    if (steppedOn.Contains(tile.transform.position))
                    {
                        if (myColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = blueSprite;
                        }
                        else
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = redSprite;
                        }
                    }
                }
                if (transform.position.x <= targetPos.x){
                    foreach (GameObject tile in normalTiles)
                    {
                        tile.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject u in upTiles)
                    {
                        u.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject r in rightTiles)
                    {
                        r.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject d in downTiles)
                    {
                        d.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject l in leftTiles)
                    {
                        l.GetComponent<BaseTileScript>().Check();
                    }
                    transform.position = new Vector3(targetPos.x, targetPos.y, 0);
                    moveUp = false;
                    moveLeft = false;
                    moveRight = false;
                    moveDown = false;
                    //targetPos = new Vector2(transform.position.x, transform.position.y);

                    if (HiddenRuleManager._instance != null && transform.position.y > -0.5f)
                    {
                        HiddenRuleManager.playerDir = 0;
                    }

                    this.GetComponent<SpriteRenderer>().sprite = leftSprites[0];
                    foreach (GameObject b in blueBoxes)
                    {
                        float dist = Vector2.Distance((Vector2)b.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            if (drawingWires && myColor == 1)
                            {
                                b.GetComponent<WireBoxScript>().isOn = true;
                                b.GetComponent<WireBoxScript>().Check();
                                drawingWires = false;
                                myColor = 0;
                                steppedOn.Clear();
                            }
                            else if (myColor == 0)
                            {
                                b.GetComponent<WireBoxScript>().isOn = true;
                                drawingWires = true;
                                myColor = 1;
                                if (!Physics2D.OverlapPoint(fixedTargetPos))
                                {
                                    steppedOn.Add(new Vector2(transform.position.x, transform.position.y - 0.5f));
                                }
                            }
                        
                        }
                    }
                    foreach (GameObject r in redBoxes)
                    {
                        float dist = Vector2.Distance((Vector2)r.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            if (drawingWires && myColor == 2)
                            {
                                r.GetComponent<WireBoxScript>().isOn = true;
                                r.GetComponent<WireBoxScript>().Check();
                                drawingWires = false;
                                myColor = 0;
                                steppedOn.Clear();
                            }
                            else if (myColor == 0)
                            {
                                r.GetComponent<WireBoxScript>().isOn = true;
                                drawingWires = true;
                                myColor = 2;
                                if (!Physics2D.OverlapPoint(fixedTargetPos))
                                {
                                    steppedOn.Add(new Vector2(transform.position.x, transform.position.y - 0.5f));
                                }
                            }
                        }
                    }
                    foreach (GameObject u in upTiles)
                    {
                        float dist = Vector2.Distance((Vector2)u.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveUp();
                        }
                        //if ((Vector2)u.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveUp = true;
                        //    targetPos = new Vector2(transform.position.x, transform.position.y + 1);
                        //}
                    }
                    foreach (GameObject l in leftTiles)
                    {

                        float dist = Vector2.Distance((Vector2)l.transform.position, fixedTargetPos);
                        if (dist < 0.1f) {
                            // Debug.Log("LEFT PLEASE");
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveLeft();
                        }

                        //if ((Vector2)l.transform.position == fixedTargetPos)
                        //{
                        //    Debug.Log("LEFT PLEASE");
                        //    canMove = false;
                        //    moveLeft = true;
                        //    targetPos = new Vector2(transform.position.x - 1, transform.position.y);
                        //}
                        else {
                            //Debug.Log((Vector2)l.transform.position);
                            //Debug.Log(fixedTargetPos);
                        }
                    }
                    foreach (GameObject r in rightTiles)
                    {
                        float dist = Vector2.Distance((Vector2)r.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveRight();
                        }
                        //if ((Vector2)r.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveRight = true;
                        //    targetPos = new Vector2(transform.position.x + 1, transform.position.y);
                        //}
                    }
                    foreach (GameObject d in downTiles)
                    {
                        float dist = Vector2.Distance((Vector2)d.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveDown();
                        }
                        //if ((Vector2)d.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveDown = true;
                        //    targetPos = new Vector2(transform.position.x, transform.position.y - 1);
                        //}
                    }
                    //Debug.Log(moveUp + "up");
                    //Debug.Log(moveLeft + "left");
                    //Debug.Log(moveRight + "right");
                    //Debug.Log(moveDown + "down");
                }
            } else if (moveRight){
                this.GetComponent<SpriteRenderer>().sprite = rightSprites[spriteCounter];

                transform.position = rightPos;
                rightPos = new Vector2(transform.position.x + 0.1f, transform.position.y);

                foreach (GameObject tile in normalTiles)
                {
                    if (steppedOn.Contains(tile.transform.position))
                    {
                        if (myColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = blueSprite;
                        }
                        else
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = redSprite;
                        }
                    }
                }
                if (transform.position.x >= targetPos.x){
                    foreach (GameObject tile in normalTiles)
                    {
                        tile.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject u in upTiles)
                    {
                        u.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject r in rightTiles)
                    {
                        r.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject d in downTiles)
                    {
                        d.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject l in leftTiles)
                    {
                        l.GetComponent<BaseTileScript>().Check();
                    }
                    transform.position = new Vector3(targetPos.x, targetPos.y, 0);
                    moveUp = false;
                    moveLeft = false;
                    moveRight = false;
                    moveDown = false;

                    if (HiddenRuleManager._instance != null && transform.position.y > -0.5f)
                    {
                        HiddenRuleManager.playerDir = 2;
                    }
                    //lastMoved = 2;
                    //targetPos = new Vector2(transform.position.x, transform.position.y);

                    this.GetComponent<SpriteRenderer>().sprite = rightSprites[0];
                    foreach (GameObject b in blueBoxes)
                    {
                        float dist = Vector2.Distance((Vector2)b.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            if (drawingWires && myColor == 1)
                            {
                                b.GetComponent<WireBoxScript>().isOn = true;
                                b.GetComponent<WireBoxScript>().Check();
                                drawingWires = false;
                                myColor = 0;
                                steppedOn.Clear();
                            }
                            else if (myColor == 0)
                            {
                                b.GetComponent<WireBoxScript>().isOn = true;
                                drawingWires = true;
                                myColor = 1;
                                if (!Physics2D.OverlapPoint(fixedTargetPos))
                                {
                                    steppedOn.Add(new Vector2(transform.position.x, transform.position.y - 0.5f));
                                }
                            }
                        }
                    }
                    foreach (GameObject r in redBoxes)
                    {
                        float dist = Vector2.Distance((Vector2)r.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            if (drawingWires && myColor == 2)
                            {
                                r.GetComponent<WireBoxScript>().isOn = true;
                                r.GetComponent<WireBoxScript>().Check();
                                drawingWires = false;
                                myColor = 0;
                                steppedOn.Clear();
                            }
                            else if (myColor == 0)
                            {
                                r.GetComponent<WireBoxScript>().isOn = true;
                                drawingWires = true;
                                myColor = 2;
                                if (!Physics2D.OverlapPoint(fixedTargetPos))
                                {
                                    steppedOn.Add(new Vector2(transform.position.x, transform.position.y - 0.5f));
                                }
                            }
                        }
                    }
                    foreach (GameObject u in upTiles)
                    {
                        float dist = Vector2.Distance((Vector2)u.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveUp();
                        }
                        //if ((Vector2)u.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveUp = true;
                        //    targetPos = new Vector2(transform.position.x, transform.position.y + 1);
                        //}
                    }
                    foreach (GameObject l in leftTiles)
                    {
                        float dist = Vector2.Distance((Vector2)l.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveLeft();
                        }
                        //if ((Vector2)l.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveLeft = true;
                        //    targetPos = new Vector2(transform.position.x - 1, transform.position.y);
                        //}
                    }
                    foreach (GameObject r in rightTiles)
                    {
                        float dist = Vector2.Distance((Vector2)r.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveRight();
                        }
                        //if ((Vector2)r.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveRight = true;
                        //    targetPos = new Vector2(transform.position.x + 1, transform.position.y);
                        //}
                    }
                    foreach (GameObject d in downTiles)
                    {
                        float dist = Vector2.Distance((Vector2)d.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveDown();
                        }
                        //if ((Vector2)d.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveDown = true;
                        //    targetPos = new Vector2(transform.position.x, transform.position.y - 1);
                        //}
                    }
                }
            } else if (moveDown){
                this.GetComponent<SpriteRenderer>().sprite = downSprites[spriteCounter];
                //Debug.Log(targetPos);
                transform.position = downPos;
                downPos = new Vector2(transform.position.x, transform.position.y - 0.1f);
                foreach (GameObject tile in normalTiles)
                {
                    if (steppedOn.Contains(tile.transform.position))
                    {
                        if (myColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = blueSprite;
                        }
                        else
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = redSprite;
                        }
                    }
                }
                if (transform.position.y <= targetPos.y){
                    foreach (GameObject tile in normalTiles)
                    {
                        tile.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject u in upTiles)
                    {
                        u.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject r in rightTiles)
                    {
                        r.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject d in downTiles)
                    {
                        d.GetComponent<BaseTileScript>().Check();
                    }
                    foreach (GameObject l in leftTiles)
                    {
                        l.GetComponent<BaseTileScript>().Check();
                    }
                    transform.position = new Vector3(targetPos.x, targetPos.y, 0);
                    moveUp = false;
                    moveLeft = false;
                    moveRight = false;
                    moveDown = false;
                    //targetPos = new Vector2(transform.position.x, transform.position.y);

                    if (HiddenRuleManager._instance != null && transform.position.y > -0.5f)
                    {
                        HiddenRuleManager.playerDir = 3;
                    }

                    this.GetComponent<SpriteRenderer>().sprite = downSprites[0];
                    foreach (GameObject b in blueBoxes)
                    {
                        float dist = Vector2.Distance((Vector2)b.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            if (drawingWires && myColor == 1)
                            {
                                b.GetComponent<WireBoxScript>().isOn = true;
                                b.GetComponent<WireBoxScript>().Check();
                                drawingWires = false;
                                myColor = 0;
                                steppedOn.Clear();
                            }
                            else if (myColor == 0)
                            {
                                b.GetComponent<WireBoxScript>().isOn = true;
                                drawingWires = true;
                                myColor = 1;
                                if (!Physics2D.OverlapPoint(fixedTargetPos))
                                {
                                    steppedOn.Add(new Vector2(transform.position.x, transform.position.y - 0.5f));
                                }
                            }
                        }
                    }
                    foreach (GameObject r in redBoxes)
                    {
                        float dist = Vector2.Distance((Vector2)r.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            if (drawingWires && myColor == 2)
                            {
                                r.GetComponent<WireBoxScript>().isOn = true;
                                r.GetComponent<WireBoxScript>().Check();
                                drawingWires = false;
                                myColor = 0;
                                steppedOn.Clear();
                            }
                            else if (myColor == 0)
                            {
                                r.GetComponent<WireBoxScript>().isOn = true;
                                drawingWires = true;
                                myColor = 2;
                                if (!Physics2D.OverlapPoint(fixedTargetPos))
                                {
                                    steppedOn.Add(new Vector2(transform.position.x, transform.position.y - 0.5f));
                                }
                            }
                        }
                    }
                    foreach (GameObject u in upTiles)
                    {
                        float dist = Vector2.Distance((Vector2)u.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveUp();
                        }
                        //if ((Vector2)u.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveUp = true;
                        //    targetPos = new Vector2(transform.position.x, transform.position.y + 1);
                        //}
                    }
                    foreach (GameObject l in leftTiles)
                    {
                        float dist = Vector2.Distance((Vector2)l.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveLeft();
                        }
                        //if ((Vector2)l.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveLeft = true;
                        //    targetPos = new Vector2(transform.position.x - 1, transform.position.y);
                        //}
                    }
                    foreach (GameObject r in rightTiles)
                    {
                        float dist = Vector2.Distance((Vector2)r.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveRight();
                        }
                        //if ((Vector2)r.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveRight = true;
                        //    targetPos = new Vector2(transform.position.x + 1, transform.position.y);
                        //}
                    }
                    foreach (GameObject d in downTiles)
                    {
                        float dist = Vector2.Distance((Vector2)d.transform.position, fixedTargetPos);
                        if (dist < 0.1f)
                        {
                            myAudio.PlayOneShot(shootTileWhoosh);
                            MoveDown();
                        }
                        //if ((Vector2)d.transform.position == fixedTargetPos)
                        //{
                        //    canMove = false;
                        //    moveDown = true;
                        //    targetPos = new Vector2(transform.position.x, transform.position.y - 1);
                        //}
                    }

                }
            }
            //if (!moveUp && !moveLeft && !moveRight && !moveDown)
            //{
            //    canMove = true;
            //}
        } else if (moveUp || moveLeft || moveRight || moveDown){
            //Debug.Log(targetPos);
            //Debug.Log(Physics2D.OverlapPoint(targetPos));

            // Debug.Log(myColliders[0]);
            moveUp = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
           // targetPos = new Vector2(transform.position.x, transform.position.y);
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

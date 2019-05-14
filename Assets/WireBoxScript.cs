using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBoxScript : MonoBehaviour
{

    public int myColor;
    public bool isOn = false;
    public bool isActive = true;

    public Sprite offSprite;
    public Sprite onSprite;

    public AudioClip completed;
    AudioSource myAudio;

    public static int completedPuzzles = 0;

    TurnTile _turntile = new TurnTile();

    List<Vector2> steppedOn;

    public GameObject[] possibleLinks;

    public GameObject[] needToLock;
    public Sprite lockSprite;
    public Sprite normalLockSprite;


    public GameObject[] allTiles;
    public GameObject myTile;

    public Sprite blueSprite;
    public Sprite redSprite;
    public Sprite normalSprite;

    int linkCounter;

    public GameObject myPair;
    GameObject player;

    public GameObject spawnLine;

    void Start()
    {
        myAudio = GetComponentInParent<AudioSource>();
        allTiles = GameObject.FindGameObjectsWithTag("BaseTile");
        this.GetComponent<SpriteRenderer>().sortingOrder -= (int)transform.position.y;
        completedPuzzles = 0;
        Cursor.visible = true;
    }

    void Update()
    {
        //TODO: there's gotta be a better way to do this guaranteed
        if (isOn)
        {
            this.GetComponent<SpriteRenderer>().sprite = onSprite;
            foreach (GameObject toLock in needToLock)
            {
                toLock.GetComponent<BoxCollider2D>().enabled = true;
                toLock.GetComponent<SpriteRenderer>().sprite = lockSprite;
                this.GetComponent<BoxCollider2D>().enabled = true;
            }
            if (myColor == 0)
            {
                myTile.GetComponent<SpriteRenderer>().sprite = blueSprite;
            }
            else
            {
                myTile.GetComponent<SpriteRenderer>().sprite = redSprite;
            }
        }
    }


    public void Check()
    {

        player = GameObject.Find("Player");
        if (isActive)
        {
            //this.GetComponent<BoxCollider2D>().enabled = false;
            float dist = Vector2.Distance((Vector2)player.transform.position, (Vector2)this.transform.position);
            foreach (GameObject link in possibleLinks)
            {
                if (link.GetComponent<WireBoxScript>().isOn)
                {
                    linkCounter++;
                    completedPuzzles++;
                    this.GetComponent<SpriteRenderer>().sprite = offSprite;
                    GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
                    foreach (GameObject d in doors)
                    {
                        d.GetComponent<DoorSprites>().Open(completedPuzzles);
                    }
                    player.GetComponent<PlayerMovementScript>().drawingWires = false;
                    steppedOn = player.GetComponent<PlayerMovementScript>().steppedOn;
                    // myPair.GetComponent<WireBoxScript>().isOn = false;
                    isActive = false;
                    myAudio.Stop();
                    myAudio.PlayOneShot(completed);
                    foreach (GameObject tile in allTiles)
                    {
                        tile.GetComponent<SpriteRenderer>().sprite = normalSprite;
                    }
                    for (int i = 1; i < steppedOn.Count - 1; i++)
                    {
                        GameObject newLine = Instantiate(spawnLine);
                        TurnTile.instance.SetTileDirections(steppedOn[i - 1], steppedOn[i], steppedOn[i + 1], myColor);
                        TurnTile.instance.SetTileOrientation(newLine);
                        newLine.transform.position = steppedOn[i];
                    }
                    link.GetComponent<SpriteRenderer>().sprite = onSprite;
                    this.GetComponent<SpriteRenderer>().sprite = onSprite;
                    link.GetComponent<BoxCollider2D>().enabled = true;
                    this.GetComponent<BoxCollider2D>().enabled = true;
                    link.GetComponent<WireBoxScript>().enabled = false;
                    this.GetComponent<WireBoxScript>().enabled = false;
                }
            }
            linkCounter = 0;
            Debug.Log("let's get this bread");
            //float dist = Vector2.Distance((Vector2)player.transform.position, (Vector2)this.transform.position);
            //if (isOn && myPair.GetComponent<WireBoxScript>().isOn)
            //{

            //}
            player.GetComponent<PlayerMovementScript>().steppedOn.Clear();
            //if (steppedOn.Count != 0)
            //{
            foreach (GameObject toLock in needToLock)
            {
                if (toLock.GetComponent<WireBoxScript>().enabled)
                {
                    toLock.GetComponent<BoxCollider2D>().enabled = false;
                }
                toLock.GetComponent<SpriteRenderer>().sprite = normalLockSprite;
            }
            steppedOn.Clear();
            //}
        }

    }
}

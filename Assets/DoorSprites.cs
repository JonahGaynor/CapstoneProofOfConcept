using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSprites : MonoBehaviour {

    public int myNumber;
    public Sprite[] mySprites;

    public int requiredNumber;

    public bool isClosed = true;

    public SpriteRenderer sr;

    public AudioClip openDoor;
    AudioSource myAudio;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        myAudio = this.GetComponent<AudioSource>();
    }

    public void Open(int completedPuzzles)
    {
        Debug.Log("open sesame");
        if (completedPuzzles >= requiredNumber)
        {
            StartCoroutine(OpenSFX());
        }
        else
        {
            myNumber = 0;
            sr.sprite = mySprites[myNumber];
            isClosed = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    IEnumerator OpenSFX()
    {
        yield return new WaitForSeconds(0.5f);
        myAudio.PlayOneShot(openDoor);
        myNumber = 1;
        sr.sprite = mySprites[myNumber];
        isClosed = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        myAudio.enabled = false;
    }
}

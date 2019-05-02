using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutAnimation : MonoBehaviour {

    public Sprite[] outSprites;
    public Sprite normalForward;

    public AudioClip inAudio;
    public AudioClip outAudio;
    AudioSource myAudio;

    GameObject audioPlayer;

    GameObject[] tiles;

	// Use this for initialization
	void Start () {
        //StartCoroutine(GetIn());
        audioPlayer = GameObject.Find("AudioManager");
	}
	
    public IEnumerator GetIn()
    {
        this.GetComponentInParent<PlayerMovementScript>().canMove = false;
        myAudio = GetComponentInParent<AudioSource>();
        this.GetComponentInParent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        this.GetComponentInParent<SpriteRenderer>().enabled = true;
        myAudio.PlayOneShot(inAudio);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[4];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[3];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[2];
        yield return new WaitForSeconds(0.1f);
        tiles = GameObject.FindGameObjectsWithTag("BaseTile");
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<BaseTileScript>().Check();
        }
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[1];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[0];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().sprite = normalForward;
        this.GetComponentInParent<PlayerMovementScript>().canMove = true;
    }

    public IEnumerator GetOut()
    {
        this.GetComponentInParent<PlayerMovementScript>().canMove = false;
        yield return new WaitForSeconds(0.1f);
        audioPlayer.GetComponent<AudioSource>().volume -= 0.02f;
        myAudio.PlayOneShot(outAudio);
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[0];
        yield return new WaitForSeconds(0.1f);
        audioPlayer.GetComponent<AudioSource>().volume -= 0.02f;
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[1];
        yield return new WaitForSeconds(0.1f);
        audioPlayer.GetComponent<AudioSource>().volume -= 0.02f;
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[2];
        yield return new WaitForSeconds(0.1f);
        audioPlayer.GetComponent<AudioSource>().volume -= 0.02f;
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[3];
        yield return new WaitForSeconds(0.1f);
        audioPlayer.GetComponent<AudioSource>().volume -= 0.02f;
        this.GetComponentInParent<SpriteRenderer>().sprite = outSprites[4];
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInParent<SpriteRenderer>().enabled = false;
    }
}

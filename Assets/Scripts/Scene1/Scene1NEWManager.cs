using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1NEWManager : MonoBehaviour {

    public Sprite[] mySprites;

    AudioSource myAudio;
    public AudioClip gasp;
    public AudioClip titleBoom;
    public AudioClip doorCreek;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = mySprites[0];
        myAudio = this.GetComponent<AudioSource>();
        StartCoroutine(TheBigOne());
	}


    IEnumerator TheBigOne (){
        yield return new WaitForSeconds (1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[1];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[2];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[3];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[4];
        myAudio.PlayOneShot(doorCreek);
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[5];
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[6];
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[7];
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[8];
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[9];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[10];
        myAudio.Stop();
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[11];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[12];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[13];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[14];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[15];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[16];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[17];
        GameObject.Find("AudioManager").GetComponent<AudioSource>().Stop();
        myAudio.PlayOneShot(gasp);
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[18];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[19];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[20];
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[21];
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("TitleCard").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("TitleCard").GetComponent<AudioSource>().PlayOneShot(titleBoom);
        yield return new WaitForSeconds(2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[22];
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("ProofofConcept_Scene2");
    }
}

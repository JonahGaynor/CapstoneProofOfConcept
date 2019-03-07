using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1FlashManager : MonoBehaviour {


    public Sprite[] mySprites;

    AudioSource myAudio;
    public AudioClip gasp;
    public AudioClip titleBoom;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = mySprites[18];
        myAudio = this.GetComponent<AudioSource>();
        StartCoroutine(TheBigOne());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }


    IEnumerator TheBigOne()
    {
        yield return new WaitForSeconds(.3f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[0];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[18];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[1];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[18];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[2];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[18];
        yield return new WaitForSeconds(9.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[7];
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[8];
        yield return new WaitForSeconds(.2f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[9];
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().sprite = mySprites[10];
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

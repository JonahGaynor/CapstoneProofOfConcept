using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreExplode : MonoBehaviour {

    public Sprite[] mySprites;
    public Sprite[] whiteOutSprites;
    public Sprite ogSprite;
    SpriteRenderer sr;

    public AudioClip bang;
    public AudioClip whiteOut;
    AudioSource myAudio;

	// Use this for initialization
	void Start () {
        sr = this.GetComponent<SpriteRenderer>();
        myAudio = this.GetComponent<AudioSource>();
	}

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(Explode());
        }
    }

    public IEnumerator Explode()
    {
        sr.sprite = ogSprite;
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sortingOrder = 45;
        //this.GetComponent<SpriteRenderer>().sortingOrder = 109;
        //GameObject.Find("WhiteOut").GetComponent<SpriteRenderer>().sprite = whiteOutSprites[0];
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(1.2f);
        for (int i = 0; i < mySprites.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            sr.sprite = mySprites[i];
        }
        yield return new WaitForSeconds(0.1f);
        sr.sprite = ogSprite;
        for (int i = 0; i < mySprites.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            sr.sprite = mySprites[i];
        }
        yield return new WaitForSeconds(0.1f);
        sr.sprite = ogSprite;
        for (int i = 0; i < mySprites.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            sr.sprite = mySprites[i];
        }
        yield return new WaitForSeconds(0.1f);
        sr.sprite = ogSprite;
        for (int i = 0; i < mySprites.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            sr.sprite = mySprites[i];
        }
        myAudio.PlayOneShot(bang, 0.3f);
        for (int i = 0; i < whiteOutSprites.Length; i++)
        {
            yield return new WaitForSeconds(0.03f);
            GameObject.Find("WhiteOut").GetComponent<SpriteRenderer>().sprite = whiteOutSprites[i];
        }
        myAudio.PlayOneShot(whiteOut);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Production_Credits");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreExplode : MonoBehaviour {

    public Sprite[] mySprites;
    public Sprite[] whiteOutSprites;
    public Sprite ogSprite;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = this.GetComponent<SpriteRenderer>();
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
        for (int i = 0; i < whiteOutSprites.Length; i++)
        {
            yield return new WaitForSeconds(0.03f);
            GameObject.Find("WhiteOut").GetComponent<SpriteRenderer>().sprite = whiteOutSprites[i];
        }
    }
}

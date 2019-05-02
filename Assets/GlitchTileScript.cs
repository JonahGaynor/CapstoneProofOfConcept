using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchTileScript : MonoBehaviour {

    public Sprite[] glitchSprites;
    SpriteRenderer sr;
    public GameObject newTile;

	// Use this for initialization
	void Start () {
        sr = this.GetComponent<SpriteRenderer>();
        //StartCoroutine(Glitch());
	}

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            StopAllCoroutines();
            StartCoroutine(Glitch());
        }
    }


    IEnumerator Glitch()
    {
        sr.sprite = glitchSprites[0];
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.1f);
        sr.sprite = glitchSprites[1];
        yield return new WaitForSeconds(0.5f);
        sr.sprite = glitchSprites[0];
        yield return new WaitForSeconds(0.3f);
        sr.sprite = glitchSprites[2];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = glitchSprites[1];
        yield return new WaitForSeconds(0.05f);
        sr.sprite = glitchSprites[0];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = glitchSprites[2];
        yield return new WaitForSeconds(0.05f);
        sr.sprite = glitchSprites[1];
        yield return new WaitForSeconds(0.1f);
        sr.sprite = glitchSprites[3];
        yield return new WaitForSeconds(0.05f);
        sr.sprite = glitchSprites[0];
        yield return new WaitForSeconds(0.1f);
        Instantiate(newTile, transform.position, Quaternion.identity);
        sr.sortingOrder = -100;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour {

    public Vector2[] resetPositions;
    GameObject player;
    public Sprite upSprite;
    public Sprite[] teleportSprites;
    int counter;
    GameObject[] wires;
    GameObject[] blueBoxes;
    public Sprite blueSprite;
    GameObject[] redBoxes;
    public Sprite redSprite;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void OnClick()
    {
        if (resetPositions[resetPositions.Length - 1].y < player.transform.position.y)
        {
            wires = GameObject.FindGameObjectsWithTag("Wire");
            blueBoxes = GameObject.FindGameObjectsWithTag("BlueBox");
            redBoxes = GameObject.FindGameObjectsWithTag("RedBox");
            //player.transform.position = resetPositions[resetPositions.Length - 1];
            //player.GetComponent<SpriteRenderer>().sprite = upSprite;
            //player.GetComponent<PlayerMovementScript>().targetPos = resetPositions[resetPositions.Length - 1];
            counter = 1;
            StartCoroutine(Teleport());
        } else if (resetPositions[resetPositions.Length - 2].y < player.transform.position.y)
        {
            wires = GameObject.FindGameObjectsWithTag("Wire");
            blueBoxes = GameObject.FindGameObjectsWithTag("BlueBox");
            redBoxes = GameObject.FindGameObjectsWithTag("RedBox");
            //player.transform.position = resetPositions[resetPositions.Length - 2];
            //player.GetComponent<SpriteRenderer>().sprite = upSprite;
            //player.GetComponent<PlayerMovementScript>().targetPos = resetPositions[resetPositions.Length - 2];
            counter = 2;
            StartCoroutine(Teleport());
        }
        else if (resetPositions[resetPositions.Length - 3].y < player.transform.position.y)
        {
            wires = GameObject.FindGameObjectsWithTag("Wire");
            blueBoxes = GameObject.FindGameObjectsWithTag("BlueBox");
            redBoxes = GameObject.FindGameObjectsWithTag("RedBox");
            //player.transform.position = resetPositions[resetPositions.Length - 3];
            //player.GetComponent<SpriteRenderer>().sprite = upSprite;
            //player.GetComponent<PlayerMovementScript>().targetPos = resetPositions[resetPositions.Length - 3];
            counter = 3;
            StartCoroutine(Teleport());
        }

        //Scene thisScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(thisScene.name);
    }

    IEnumerator Teleport()
    {
        player.GetComponent<PlayerMovementScript>().canMove = false;
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[0];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[1];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[2];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[3];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[4];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        player.transform.position = resetPositions[resetPositions.Length - counter];
        player.GetComponent<PlayerMovementScript>().targetPos = player.transform.position;
        foreach (GameObject wire in wires)
        {
            if (wire.transform.position.y > resetPositions[resetPositions.Length - counter].y)
            {
                Destroy(wire);
            }
        }
        foreach (GameObject box in blueBoxes)
        {
            box.GetComponent<WireBoxScript>().enabled = true;
            box.GetComponent<WireBoxScript>().isOn = false;
            box.GetComponent<WireBoxScript>().isActive = true;
            box.GetComponent<SpriteRenderer>().sprite = blueSprite;
        }
        foreach (GameObject box in redBoxes)
        {
            box.GetComponent<WireBoxScript>().enabled = true;
            box.GetComponent<WireBoxScript>().isOn = false;
            box.GetComponent<WireBoxScript>().isActive = true;
            box.GetComponent<SpriteRenderer>().sprite = redSprite;
        }
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[3];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[2];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[1];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[0];
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<SpriteRenderer>().sprite = teleportSprites[5];
        player.GetComponent<PlayerMovementScript>().canMove = true;

    }
}

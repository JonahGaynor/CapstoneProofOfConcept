using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantChoicePlayer : MonoBehaviour {

    public bool animate = false;
    public Vector2 targetPos;
    PlayerMovementScript inUse;

	// Use this for initialization
	void Start () {
        inUse = this.GetComponent<PlayerMovementScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (animate)
        {
            if (!inUse.moveUp && !inUse.moveRight && !inUse.moveDown && !inUse.moveLeft)
            {
                if (transform.position.x < targetPos.x)
                {
                    inUse.MoveRight();
                }
                else if (transform.position.x > targetPos.x)
                {
                    inUse.MoveLeft();
                }
                else if (transform.position.y < targetPos.y)
                {
                    inUse.MoveUp();
                }
                else if (transform.position.y > targetPos.y)
                {
                    inUse.MoveDown();
                }
                else
                {
                    animate = false;
                    this.GetComponent<SpriteRenderer>().sprite = inUse.leftSprites[0];
                }
            }

        }
	}

}

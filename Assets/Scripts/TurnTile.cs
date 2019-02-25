using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTile : MonoBehaviour {
    public Direction fromDir;
    public Direction toDir;

    public Sprite[] mySprites;
    
    int thisColor;

    public static TurnTile instance
    {
        get { return _instance; }
    }
    
    private static TurnTile _instance;

    //TODO: MAKE START AND END POINTS PART OF THE LIST
    private void Start()
    {
        _instance = this;

    }
    public void SetTileDirections(Vector2 from, Vector2 origin, Vector2 to, int color)
    {
        Debug.Log("set tile directions");
        if (from.x < origin.x)
        {
            fromDir = Direction.West;
        }

        if (from.x > origin.x)
        {
            fromDir = Direction.East;
            }
        if (from.y < origin.y)
        {
            fromDir = Direction.South;
        }
        if (from.y > origin.y)
        {
            fromDir = Direction.North;
        }
        if (to.x < origin.x)
        {
            toDir = Direction.West;
        }

        if (to.x > origin.x)
        {
            toDir = Direction.East;
        }
        if (to.y < origin.y)
        {
            toDir = Direction.South;
        }
        if (to.y > origin.y)
        {
            toDir = Direction.North;
        }

        thisColor = color;
    }

    public void SetTileOrientation(GameObject tile)
    {
        Debug.Log("set tile orientation");
        SpriteRenderer r = tile.GetComponent<SpriteRenderer>();
        switch (fromDir)
        {
            case Direction.North:
                switch (toDir)
                {
                    case Direction.North:
                        break;
                    case Direction.South:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[0];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[8];
                        }
                        break;
                    case Direction.East:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[2];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[10];
                        }
                        break;
                    case Direction.West:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[1];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[9];
                        }
                        break;
                }
                break;
            case Direction.South:
                switch (toDir)
                {
                    case Direction.North:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[0];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[8];
                        }
                        break;
                    case Direction.South:
                        break;
                    case Direction.East:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[4];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[12];
                        }
                        break;
                    case Direction.West:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[3];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[11];
                        }
                        break;
                }
                break;
            case Direction.East:
                switch (toDir)
                {
                    case Direction.North:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[2];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[10];
                        }
                        break;
                    case Direction.South:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[4];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[12];
                        }
                        break;
                    case Direction.East:
                        break;
                    case Direction.West:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[5];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[13];
                        }
                        break;
                }
                break;
            case Direction.West:
                switch (toDir)
                {
                    case Direction.North:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[1];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[9];
                        }
                        break;
                    case Direction.South:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[3];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[11];
                        }
                        break;
                    case Direction.East:
                        if (thisColor == 0)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[5];
                        }
                        else if (thisColor == 1)
                        {
                            tile.GetComponent<SpriteRenderer>().sprite = mySprites[13];
                        }
                        break;
                    case Direction.West:
                        break;
                }
                break;
        }
    }

    public enum Direction
    {
        North,
        South,
        East,
        West
    }
    public Direction myDir;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDiceInUse : MonoBehaviour {

    public bool selected = false;

    public GameObject[] otherDice;

    public bool isOn;

    public GenDiceScript diceProfile;

    Vector3 tempPos;

    public int slot;

    //// Use this for initialization
    //void Start () {
        
    //}
    
    //// Update is called once per frame
    //void Update () {
        
    //}

    public void OnClick()
    {
        if (isOn)
        {
            if (!selected)
            {
                tempPos = this.transform.position;
                selected = true;
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                foreach (GameObject dice in otherDice)
                {
                    if (dice.GetComponent<UIDiceNotInUse>().selected)
                    {
                        this.transform.position = new Vector2(dice.transform.position.x, dice.transform.position.y);
                        dice.transform.position = new Vector2(tempPos.x, tempPos.y);
                        this.GetComponent<UIDiceNotInUse>().isOn = true;
                        dice.GetComponent<UIDiceNotInUse>().isOn = false;
                        dice.GetComponent<UIDiceInUse>().isOn = true;
                        this.GetComponent<UIDiceInUse>().isOn = false;
                        dice.transform.localScale = new Vector3(1f, 1f, 1f);
                        this.transform.localScale = new Vector3(1f, 1f, 1f);
                        dice.GetComponent<UIDiceInUse>().slot = slot;
                        if (slot == 1)
                        {
                            GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile1 = dice.GetComponent<UIDiceInUse>().diceProfile;
                        }
                        else if (slot == 2)
                        {
                            GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile2 = dice.GetComponent<UIDiceInUse>().diceProfile;
                        }
                        else if (slot == 3)
                        {
                            GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile3 = dice.GetComponent<UIDiceInUse>().diceProfile;
                        }
                        else if (slot == 4)
                        {
                            GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile4 = dice.GetComponent<UIDiceInUse>().diceProfile;
                        }
                        else if (slot == 5)
                        {
                            GameObject.Find("StatsTracker").GetComponent<PlayerStatsTracker>().diceProfile5 = dice.GetComponent<UIDiceInUse>().diceProfile;
                        }
                    }

                }
            }
            else
            {
                selected = false;
                transform.localScale = new Vector3(1f, 1f, 1f);

            }
        }
    }
}

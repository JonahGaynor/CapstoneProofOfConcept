using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsTracker : MonoBehaviour {

    public int myDice;
    public int enemiesDefeated;
    //public Vector2[] firstBids;
    //public Vector2[] calledBids;

    //public Sprite[] enemySprites;
    //public Sprite[] enemyPrinters;
    public BattleScenePackage Ai1;
    public BattleScenePackage Ai2;
    public BattleScenePackage Ai3;
    public BattleScenePackage Julia;
    public BattleScenePackage Jacob;
    public BattleScenePackage Amelia;
    public BattleScenePackage Simon;

    public int enemyHit;

    public GenDiceScript diceProfile1;
    public GenDiceScript diceProfile2;
    public GenDiceScript diceProfile3;
    public GenDiceScript diceProfile4;
    public GenDiceScript diceProfile5;

    public Vector2 myPos;

    Scene lastScene;
    public string sceneName;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}

    public void EnterBattle() {
        Debug.Log("enemy hit number is " + enemyHit);
        lastScene = SceneManager.GetActiveScene();
        GameObject.Find("DiceMaker").GetComponent<SceneTransitionScript>().carryOverDice = myDice;
    }

    public void ExitBattle(int dice) {
        if (dice > 0) {
            //SceneManager.LoadScene(lastScene.name);
            //GameObject.Find("Player").transform.position = myPos;
        }
        else {
            StartCoroutine(GiveDice());
            //myDice = 5;
        }
    }

    IEnumerator GiveDice()
    {
        myDice = GameObject.Find("BattleManager").GetComponent<BattleManager>().myDice;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(lastScene.name);
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("DiceMaker").GetComponent<SceneTransitionScript>().carryOverDice = myDice;
    }
}

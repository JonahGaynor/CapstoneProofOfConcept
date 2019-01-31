using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsTracker : MonoBehaviour {

    public int myDice;
    public int enemiesDefeated;
    public Vector2[] firstBids;
    public Vector2[] calledBids;

    public GenDiceScript diceProfile1;
    public GenDiceScript diceProfile2;
    public GenDiceScript diceProfile3;
    public GenDiceScript diceProfile4;
    public GenDiceScript diceProfile5;

    public Vector2 myPos;

    Scene lastScene;
    string sceneName;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}

    public void EnterBattle() {
        lastScene = SceneManager.GetActiveScene();

    }

    public void ExitBattle(int dice) {
        if (dice > 0) {
            SceneManager.LoadScene(lastScene.name);
            GameObject.Find("Player").transform.position = myPos;
        }
        else {
            SceneManager.LoadScene(lastScene.name);
            myDice = 5;
        }
    }
}

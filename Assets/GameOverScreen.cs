using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
    
	// Update is called once per frame
	void Update () {
        scoreText.text = "You scored " + ScoreHandler.Instance.Score + " points";
	}

    public void GotoStartScreen() {
        SceneManager.LoadScene("Start");
    }
}

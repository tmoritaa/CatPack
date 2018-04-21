using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {
    private static ScoreHandler instance;
    public static ScoreHandler Instance
    {
        get {
            return instance;
        }
    }

    public int Score
    {
        get; private set;
    }

    [SerializeField]
    private Text scoreText;

	// Use this for initialization
	void Awake () {
        instance = this;
        Score = 0;
        updateScoreText();
	}

    public void AddScore(int addVal) {
        Score += addVal;

        updateScoreText();
    }

    private void updateScoreText() {
        scoreText.text = "Score: " + Score.ToString();
    }
}

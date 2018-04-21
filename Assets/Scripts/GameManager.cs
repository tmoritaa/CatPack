using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    public static GameManager Instance
    {
        get {
            return instance;
        }
    }

    public bool IsGameOver
    {
        get; private set;
    }

    [SerializeField]
    private GameObject gameoverScreen;

    void Awake() {
        instance = this;
        IsGameOver = false;
    }

    // Use this for initialization
    void Start () {
        gameoverScreen.SetActive(false);
	}
	
    public void GameOver() {
        IsGameOver = true;
        gameoverScreen.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;


    [SerializeField] private TMP_Text scoreText;

    private int score = 0;

    private void Awake()
    {
        INSTANCE = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddScore(int _score)
    {
        score += _score;
        scoreText.text = "Score: " + score.ToString();
    }
}

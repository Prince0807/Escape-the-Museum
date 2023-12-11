using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;


    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text hintToogleText;
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private Image bloodImage;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hintToogleText.gameObject.SetActive(!hintToogleText.IsActive());
            hintText.gameObject.SetActive(!hintText.IsActive());
        }
    }

    public void SetBloodImageAlpha(float alpha)
    {
        alpha = (100 - alpha)/100;
        Debug.Log(alpha);
        Color color = bloodImage.color;
        color.a = alpha;
        bloodImage.color = color;
    }
}

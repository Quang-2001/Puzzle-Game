using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    Score score;
    void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations!\n You got a score of" + score.CalculateScore() + "%"; 
    }
}

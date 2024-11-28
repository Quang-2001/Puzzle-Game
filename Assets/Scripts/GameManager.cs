using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScene endScene;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScene = FindObjectOfType<EndScene>();
    }
    void Start()
    {
        

        quiz.gameObject.SetActive(true);
        endScene.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (quiz.isComlete)
        {
            quiz.gameObject.SetActive(false);
            endScene.gameObject.SetActive(true);
            endScene.ShowFinalScore();
        }
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

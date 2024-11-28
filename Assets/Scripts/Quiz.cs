using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionText;  
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO curentquestion;

    [Header("Answer")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    Score score;


    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComlete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        score = FindObjectOfType<Score>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComlete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index) //  chon cau tra loi 
    {
        
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score " + score.CalculateScore() + "%";
        
        
        
    } 
    
    void DisplayAnswer(int index)
    {
        Image ButtonImage;
        if (index == curentquestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            ButtonImage = answerButtons[index].GetComponent<Image>();
            ButtonImage.sprite = correctAnswerSprite;
            score.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = curentquestion.GetCorrectAnswerIndex();
            string correctAnswer = curentquestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry the correct answer was\n" + correctAnswer;
            ButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            ButtonImage.sprite = correctAnswerSprite;

        }
    }
    
    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            score.IncrementQuestionsSeen();
        }
        
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        curentquestion = questions[index];
        if (questions.Contains(curentquestion))
        {
            questions.Remove(curentquestion);
        }
        
        
    }
    void DisplayQuestion() // hien thi cau hoi
    {
        questionText.text = curentquestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = curentquestion.GetAnswer(i);
        }
    }
    void SetButtonState(bool state)
    {
        for(int i = 0; i< answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    
    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image ButtonImage = answerButtons[i].GetComponent<Image>();
            ButtonImage.sprite = defaultAnswerSprite;
        }
    }

}

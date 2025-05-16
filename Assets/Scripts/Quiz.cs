using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TMP için eklememiz gerekli
using UnityEngine.UI;  //Image için eklememiz gerekir

public class Quiz : MonoBehaviour

{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;  //sorumuz ile UI bağlamak için
    [SerializeField] List<QuestionSO> questions=new List<QuestionSO>();
     QuestionSO currentQuestion;   //diğer classdaki metotlarımızı kullanabilmek için

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    public bool hasAnsweredEarly=true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    
    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Awake()
    {
        timer=FindObjectOfType<Timer>(); 
        scoreKeeper=FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue=questions.Count;
        progressBar.value=0;
    }
    void Update()
    {
        timerImage.fillAmount=timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            if(progressBar.value==progressBar.maxValue)
        {
            isComplete=true;
            return;
        }
            hasAnsweredEarly=false;
            GetNewQuestion();
            timer.loadNextQuestion=false;
        }
        else if(!hasAnsweredEarly&&!timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }
    Image buttonImage;
    public void OnAnswerSelected(int index)  // hiç anlamadımmmmmm ?
    {
        hasAnsweredEarly=true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text="Score : "+scoreKeeper.CalculateScore() + "%";
    }
    void DisplayAnswer(int index)
    {
        if(index==currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text="Correct!";
            buttonImage=answerButtons[index].GetComponent<Image>();
            buttonImage.sprite=correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex=currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer=currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text="Sorry the correct answer was \n"+correctAnswer;
            buttonImage=answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite=correctAnswerSprite;
        }
    }
    void GetNewQuestion()
    {
        if(questions.Count>0)
        {
        SetButtonState(true);
        SetDefaultButtonSprites();
        GetRandomQuestion();
        DisplayQuestions();
        progressBar.value++;
        scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index=Random.Range(0,questions.Count);
        currentQuestion=questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    
    }

    void DisplayQuestions()
    {
        questionText.text=currentQuestion.GetQuestion(); //diğer classdaki get question ile aldığımız sorumuzu UI daki texte aktar

        for(int i=0;i<answerButtons.Length;i++)
        {
            TextMeshProUGUI buttontext = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>(); // ?
            buttontext.text=currentQuestion.GetAnswer(i);
        }

    }
    void SetButtonState(bool state)
    {
        for(int i=0;i<answerButtons.Length;i++)
        {
            Button button=answerButtons[i].GetComponent<Button>();
            button.interactable=state;
        }
    }
    void SetDefaultButtonSprites()
    {
        for(int i=0;i<answerButtons.Length;i++)
        {
            Image buttonImage =answerButtons[i].GetComponent<Image>();
            buttonImage.sprite=defaultAnswerSprite;
        }
    }
}

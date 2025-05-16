using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers=0;
    int questionsSeen=0;

public void IncrementQuestionsSeen()
{
    questionsSeen++;
}
public int GetQuestionsSeen()
{
    return questionsSeen;
}
 public int GetCorrectAnswer()
 {
      return correctAnswers;
 }
 public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }
    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers/(float)questionsSeen*100);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Quiz question",fileName ="New question")]  //yeni asset menüsü yaratıp adını ve dosya adını verdik

public class QuestionSO : ScriptableObject //monobehavioru değiştirdik
{
    [TextArea(2,6)]  //inspectorda stringimizin tek satır görünmemesi için bu attribute ekledik
    [SerializeField] string question="Enter new question text here";
    [SerializeField] string[] answers=new string[4];  //answers adında 4 elementli bir array oluştur 
    [SerializeField]int correctAnswerIndex;

    public string GetQuestion() //getter olarak kullanıyoruz question değişkenine diğer classlardan ulaşabilmek için
    {
        return question;  //metot question değişkenini döndürsün 
    }
    public int GetCorrectAnswerIndex() //doğru cevabın indeksini döndür
    {
        return correctAnswerIndex;
    }
    public string GetAnswer(int index) //doğru şıkkın indeksini parametre olarak girip doğru cevabı belirtiyoruz
    {
        return answers[index];
    }
}

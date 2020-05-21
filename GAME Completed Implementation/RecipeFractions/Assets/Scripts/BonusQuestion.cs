using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mehroz;
using UnityEngine.UI;

public class BonusQuestion {

    //class vars
    private string question;
    private string answerA;
    private string answerB;
    private string answerC;
    private string answerD;

    //QuestionVars;
    private string BQuestion;
    private int questionSlices;
    private string correctAnswer;
    private List<string> wrongAnswers;
    private List<int> alreadyReturnedWrongAns;
    private int resetWrongAnsList;

    //QuestionTextComponent
    private Text BonusQuestiontxt;
    private Text BtnA;
    private Text BtnB;
    private Text BtnC;
    private Text BtnD;

    public string Question
    {
        get
        {
            return question;
        }

        set
        {
            question = value;
        }
    }

    public string AnswerA
    {
        get
        {
            return answerA;
        }

        set
        {
            answerA = value;
        }
    }

    public string AnswerB
    {
        get
        {
            return answerB;
        }

        set
        {
            answerB = value;
        }
    }

    public string AnswerC
    {
        get
        {
            return answerC;
        }

        set
        {
            answerC = value;
        }
    }

    public string AnswerD
    {
        get
        {
            return answerD;
        }

        set
        {
            answerD = value;
        }
    }

    public BonusQuestion()
    {
    }

    public BonusQuestion(string question, string answerA, string answerB, string answerC, string answerD)
    {
        this.Question = question;
        this.AnswerA = answerA;
        this.AnswerB = answerB;
        this.AnswerC = answerC;
        this.AnswerD = answerD;
    }

    public string GenerateQuestion()
    {
        questionSlices = Random.Range(7,16);
        resetWrongAnsList = 0;

        BQuestion = "If 6 slices make up 1 pizza. Which of the fractions below represent " + questionSlices + " slices?";
        return BQuestion;
    }

    public string GenerateAnswer()
    {
        switch (questionSlices)
        {
            case 7:
                correctAnswer = "1 1/6";
                return correctAnswer;
            case 8:
                correctAnswer = "1 2/6";
                return correctAnswer;
            case 9:
                correctAnswer = "1 3/6";
                return correctAnswer;
            case 10:
                correctAnswer = "1 4/6";
                return correctAnswer;
            case 11:
                correctAnswer = "1 5/6";
                return correctAnswer;
            case 12:
                correctAnswer = "2";
                return correctAnswer;
            case 13:
                correctAnswer = "2 1/6";
                return correctAnswer;
            case 14:
                correctAnswer = "2 2/6";
                return correctAnswer;
            case 15:
                correctAnswer = "2 3/6";
                return correctAnswer;
            case 16:
                correctAnswer = "2 4/6";
                return correctAnswer;
            default:
                return null;
        }
    }

    public string GenerateUniqueWrongAnswer()
    {
        wrongAnswers = new List<string>()
        {
            "1 1/6",
            "1 2/6",
            "1 3/6",
            "1 4/6",
            "1 5/6",
            "2",
            "2 1/6",
            "2 2/6",
            "2 3/6",
            "2 4/6"
        };
        alreadyReturnedWrongAns = new List<int>();
        resetWrongAnsList++;

        int r = Random.Range(1, 10);

        if (wrongAnswers[r] == correctAnswer)
        {
            r = Random.Range(1, 10);
        }

        restart:
        foreach (int i in alreadyReturnedWrongAns)
        {
            if (i == r)
            {
                r = Random.Range(1,10);
                goto restart;
            }
        }

        alreadyReturnedWrongAns.Add(r);

        if (resetWrongAnsList == 3)
            alreadyReturnedWrongAns.Clear();

        return wrongAnswers[r];
    }

    public void PopulatePanel()
    {
        BonusQuestiontxt = GameObject.Find("BonusQuestiontxt").GetComponent<Text>();
        BonusQuestiontxt.text = question;

        BtnA = GameObject.Find("BtnA").transform.GetChild(0).gameObject.GetComponent<Text>();
        BtnB = GameObject.Find("BtnB").transform.GetChild(0).gameObject.GetComponent<Text>();
        BtnC = GameObject.Find("BtnC").transform.GetChild(0).gameObject.GetComponent<Text>();
        BtnD = GameObject.Find("BtnD").transform.GetChild(0).gameObject.GetComponent<Text>();

        BtnA.text = answerA;
        BtnB.text = answerB;
        BtnC.text = answerC;
        BtnD.text = answerD;
    }
}

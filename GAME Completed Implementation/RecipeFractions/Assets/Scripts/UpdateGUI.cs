using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGUI : MonoBehaviour {

    //FoodMenu
    public static GameObject menu;
    
    //Find all questions canvases
    public static GameObject l1q1;
    public static GameObject bonusQuestion;
    public static GameObject L2Q1;
    public static GameObject L2Q2;
    public static GameObject L2Q3;
    public static List<GameObject> L2;
    public static GameObject EndGameCanvas;

    // Use this for initialization
    void Start ()
    {
        //Menu
        menu = GameObject.Find("MenuCanvas");
        menu.SetActive(false);

        //Questions
        l1q1 = GameObject.Find("L1Q1");
        l1q1.SetActive(false);
        bonusQuestion = GameObject.Find("BonusQuestion");
        bonusQuestion.SetActive(false);
        L2Q1 = GameObject.Find("L2Q1");
        L2Q1.SetActive(false);
        L2Q2 = GameObject.Find("L2Q2");
        L2Q2.SetActive(false);
        L2Q3 = GameObject.Find("L2Q3");
        L2Q3.SetActive(false);

        L2 = new List<GameObject>
        {
            L2Q1,
            L2Q2,
            L2Q3
        };

        EndGameCanvas = GameObject.Find("EndGameCanvas");
        EndGameCanvas.SetActive(false);
    }
}

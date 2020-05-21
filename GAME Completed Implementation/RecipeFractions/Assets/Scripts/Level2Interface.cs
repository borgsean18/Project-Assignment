using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Interface : MonoBehaviour {

    public static bool clicked;
    public static string answer;
    private string go;

    // Use this for initialization
    void Start ()
    {
        clicked = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnClicked()
    {
        clicked = true;
    }

    public void OnClickedCorrect()
    {
        clicked = true;
        Score.Scores += 5;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private static int score;
    private static bool updates;

    //Score GUI
    private Text scoreText;

    public static int Scores
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            updates = true;
        }
    }

    // Use this for initialization
    void Start () {
        score = 0;
        updates = false;

        //Score GUI
        scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (updates)
        {
            scoreText.text = "Score: " + score;
            updates = false;
        }
	}
}

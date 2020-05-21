using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mehroz;

public class Chef : MonoBehaviour {

    //chef bools
    public static bool chefHasSelected;
    private static bool updateQuestion;
    public static bool isCorrect;

    //the order
    private static Order o;

    //Questions GameObjects
    private Text l1q1Text;

    // Use this for initialization
    void Start () {

        o = null;

        //chef bools
        chefHasSelected = false;
        updateQuestion = false;
        isCorrect = false;

        //Questions GameObjects
        l1q1Text = GameObject.Find("L1Q1Text").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameOptions.level == 1)
        {
            if (updateQuestion)
            {
                l1q1Text.text = "The customer wants " + o.Slices.ToString() + " of a " + o.PizzaType + " pizza. Press the correct button to cook the right amount of slices!";

                updateQuestion = false;
            }
        }
		
	}


    //------------ Level 1 methods ----------
    #region Level1
    public static void GetTheOrder(Order order)
    {
        o = order;
        updateQuestion = true;
    }

    public void OnOptionClicked(float selectedShape)
    {
        chefHasSelected = true;

        CheckAnswer(selectedShape);
        StartCoroutine(RevertBoolToFalse());
    }

    private void CheckAnswer(float f)
    {
        Fraction frac = new Fraction(f);
        if (frac == o.Slices)
        {
            Score.Scores += 5;
            o.Customer.GetComponent<IndividualCustomer>().HasBeenServed = true;
            isCorrect = true;
            //make the customer eat, leave, and reset
        }
        else
        {
            o.Customer.GetComponent<IndividualCustomer>().Leaving = true;
            isCorrect = false;
        }
    }

    private IEnumerator RevertBoolToFalse()
    {
        yield return new WaitForSeconds(0.5f);
        chefHasSelected = false;
    }
    #endregion Level1
    //------------ Level 1 methods ----------


    //------------ Level 2 methods ----------
    #region Level2



    #endregion Level2
    //------------ Level 2 methods ----------
}

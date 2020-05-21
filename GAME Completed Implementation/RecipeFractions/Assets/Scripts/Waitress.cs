using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Mehroz;

public class Waitress : MonoBehaviour
{
    //list of fractions
    private List<float> fractionsList;

    //The customers order - being defined in OnClicked()
    public static Order o;
    private GameObject customerGO;

    //waitress bools
    private int customerIncrementer;
    private bool isBusy;
    public static bool isAtCustomer;
    private bool custHasOrdered;

    //chairs vars
    //private GameObject[] chairs;

    //waitressPosition
    private GameObject waitressPosition;

    //bonus Question
    BonusQuestion B;
    string correctBAns;

    // Use this for initialization
    void Start()
    {
        //list of fractions
        fractionsList = new List<float>
        {
            0.25f,
            0.5f,
            0.75f,
            1f
        };

        //waitress bools
        customerIncrementer = 0;
        isBusy = false;
        isAtCustomer = false;
        custHasOrdered = false;

        //chairs vars
        //chairs = GameObject.FindGameObjectsWithTag("chair");

        //waitress Position
        waitressPosition = GameObject.Find("waitress_position");
    }

    // Update is called once per frame
    void Update()
    {
        GoToCustomer();

        GiveOrderToChef();
    }

    private void GoToCustomer()
    {
        if (Customer.customers.Count > 0)
        {
            if (!isBusy && !isAtCustomer)
            {
                if (Customer.customers[customerIncrementer].GetComponent<IndividualCustomer>().CallWaitress) //if customer called the waitress
                {
                    transform.position = Vector3.MoveTowards(transform.position, Customer.customers[customerIncrementer].transform.position, 0.02f);
                }
                else
                    customerIncrementer++;

                if (customerIncrementer == Customer.customers.Count)
                    customerIncrementer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "customer")
        {
            if (other.GetComponent<IndividualCustomer>().CallWaitress == true)
            {
                isAtCustomer = true;
                StartCoroutine(TakeOrder()); //Coroutine to open the menu
                customerGO = other.gameObject;

                other.GetComponent<IndividualCustomer>().CallWaitress = false;
                other.GetComponent<IndividualCustomer>().Ordered1 = true;
            }
        }

        if (other.name == "waitress_position")
        {
            StartCoroutine(GivingOrder());
        }
    }

    private IEnumerator TakeOrder()
    {
        UpdateGUI.menu.SetActive(true);

        //Client asks for a randomly generated fraction of a pizza ----- I have a list of fractions that are usable, and am selecting from that list randomly
        //Player will respond with number of slices ---- done

        yield return new WaitUntil(() => custHasOrdered == true);
        UpdateGUI.menu.SetActive(false);

        custHasOrdered = false; //resetting it for the next customer to order
        isAtCustomer = false;
        isBusy = true;
    }

    //this is the customer ordering from the menu
    public void OnClicked(string order)
    {
        Fraction frac = new Fraction(fractionsList[Random.Range(0,fractionsList.Count)]);
        o = new Order(order, frac, customerGO);
        custHasOrdered = true;
    }

    //walk to the chef to give the order
    private void GiveOrderToChef()
    {
        //we have six clients so we need to make x number of slices.
        //show the recipie for one pizza
        //what would the fractional amount be

        if (isBusy && !isAtCustomer)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitressPosition.transform.position, 0.02f);
        }
    }

    //handing the order over to the chef
    private IEnumerator GivingOrder()
    {
        UpdateGUI.l1q1.SetActive(true);
        Chef.GetTheOrder(o); //give order to the chef

        yield return new WaitUntil(() => Chef.chefHasSelected == true);

        UpdateGUI.l1q1.SetActive(false);

        if (Chef.isCorrect)
        {
            StartCoroutine(BonusQuestionCR());
        }
        else
        {
            isBusy = false;
        }
    }

    private IEnumerator BonusQuestionCR()
    {
        //do bonus question
        BonusQuestionLogic();

        UpdateGUI.bonusQuestion.SetActive(true);

        B.PopulatePanel();

        yield return new WaitUntil(() => BonusQuestionInterface.clicked == true);
        BonusQuestionInterface.clicked = false;

        UpdateGUI.bonusQuestion.SetActive(false);

        if (correctBAns == BonusQuestionInterface.answer)
        {
            Score.Scores += 10;
        }

        isBusy = false;
    }

    private void BonusQuestionLogic()
    {
        BonusQuestion b = new BonusQuestion();
        string quest = b.GenerateQuestion();
        correctBAns = b.GenerateAnswer();
        string wrongAnsA = b.GenerateUniqueWrongAnswer();
        string wrongAnsB = b.GenerateUniqueWrongAnswer();
        string wrongAnsC = b.GenerateUniqueWrongAnswer();

        List<string> listOfAns = new List<string>()
        {
            correctBAns,
            wrongAnsA,
            wrongAnsB,
            wrongAnsC
        };

        List<int> listOfInts = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            listOfInts.Add(i);
        }

        listOfInts.Sort((a, c) => 1 - 2 * Random.Range(0, 1));

        int shuffledIntA = listOfInts[0];
        int shuffledIntB = listOfInts[1];
        int shuffledIntC = listOfInts[2];
        int shuffledIntD = listOfInts[3];

        B = new BonusQuestion(quest, listOfAns[shuffledIntA], listOfAns[shuffledIntB], listOfAns[shuffledIntC], listOfAns[shuffledIntD]);
    }
}

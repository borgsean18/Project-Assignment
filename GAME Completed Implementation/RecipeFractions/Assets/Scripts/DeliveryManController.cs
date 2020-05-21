using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManController : MonoBehaviour {

    //DeliveryMan bools
    private bool isInside;
    private bool isAtChef;
    private bool leaving;

    //GameObjects
    private GameObject deliveryman_position;
    private GameObject doorA;

    // Use this for initialization
    void Start() {
        //DeliveryMan bools
        isInside = false;
        isAtChef = false;
        leaving = false;

        //GameObjects
        deliveryman_position = GameObject.Find("deliveryman_position");
        doorA = GameObject.Find("doorA");
    }

    // Update is called once per frame
    void Update() {
        GoInside();

        WalkToChef();

        Leave();
    }

    private void GoInside()
    {
        if (!isInside)
        {
            transform.Translate(new Vector3(0, 0, 0.01f));

            if (transform.position.z <= 5)
            {
                isInside = true;
            }
        }
    }

    private void WalkToChef()
    {
        if (isInside && !isAtChef && !leaving)
        {
            do
            {
                transform.position = Vector3.MoveTowards(transform.position, deliveryman_position.transform.position, 0.02f);
            }
            while (transform.position.z <= 2.3f);

            if (transform.position.z > 2.1f && transform.position.z < 2.4f)
            {
                isAtChef = true;
                StartCoroutine(TalkToChef());
            }
        }
    }

    private IEnumerator TalkToChef()
    {
        UpdateGUI.L2[0].gameObject.SetActive(true);
        yield return new WaitUntil(() => Level2Interface.clicked == true);
        Level2Interface.clicked = false;
        UpdateGUI.L2[0].SetActive(false);
        yield return new WaitForSeconds(1);

        UpdateGUI.L2[1].gameObject.SetActive(true);
        yield return new WaitUntil(() => Level2Interface.clicked == true);
        Level2Interface.clicked = false;
        UpdateGUI.L2[1].SetActive(false);
        yield return new WaitForSeconds(1);

        UpdateGUI.L2[2].gameObject.SetActive(true);
        yield return new WaitUntil(() => Level2Interface.clicked == true);
        Level2Interface.clicked = false;
        UpdateGUI.L2[2].SetActive(false);

        leaving = true;
    }

    private void Leave()
    {
        if (leaving)
        {
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
            transform.position = Vector3.MoveTowards(transform.position, doorA.transform.position, 0.02f);
            if (transform.position.z > 5.35f)
            {
                Destroy(gameObject);
                EndTheGame();
            }
        }
    }

    private void EndTheGame()
    {
        GameOptions.timing = false;
        UpdateGUI.EndGameCanvas.SetActive(true);
        GameObject.Find("endTxtScore").GetComponent<Text>().text = "Your Score: " + Score.Scores;
        GameObject.Find("endTxtTime").GetComponent<Text>().text = "Your Time: " + Mathf.Round(GameOptions.timer);
    }
}

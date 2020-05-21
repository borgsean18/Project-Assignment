using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndividualCustomer : MonoBehaviour {

    //customer bools
    private bool inside;
    private bool foundChair;
    private bool isSeated;
    private bool callWaitress;
    private bool ordered;
    private bool hasBeenServed;
    private bool leaving;

    //chairs
    private int selectedChair;
    private GameObject[] chairs;
    private int chairLocator;
    private int customersChosenChair;
    private Vector3 chairsTransform;

    //GameObjects
    private GameObject door;

    public bool CallWaitress
    {
        get
        {
            return callWaitress;
        }

        set
        {
            callWaitress = value;
        }
    }

    public bool Ordered1
    {
        get
        {
            return ordered;
        }

        set
        {
            ordered = value;
        }
    }

    public bool Leaving
    {
        get
        {
            return leaving;
        }

        set
        {
            leaving = value;
        }
    }

    public bool HasBeenServed
    {
        get
        {
            return hasBeenServed;
        }

        set
        {
            hasBeenServed = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        //cust bools
        inside = false;
        foundChair = false;
        callWaitress = false;
        isSeated = false;
        ordered = false;
        HasBeenServed = false;
        leaving = false;

        //chairs
        chairLocator = 0;
        chairs = GameObject.FindGameObjectsWithTag("chair");

        //GameObjects
        door = GameObject.Find("doorA");
    }
	
	// Update is called once per frame
	void Update ()
    {
        GoInside();

        FindAvailableChair();

        Eat();

        Leave();
	}

    private void GoInside()
    {
        if (!inside)
        {
            transform.Translate(new Vector3(0, 0, 0.01f));

            if (transform.position.z <= 5)
            {
                inside = true;
            }
        }
    }

    private void FindAvailableChair()
    {
        if (inside && !foundChair && !isSeated)
        {
            if (chairs[chairLocator].GetComponent<Chair>().IsAvailable)
            {
                transform.position = Vector3.MoveTowards(transform.position, chairs[chairLocator].transform.position, 0.02f);
                customersChosenChair = chairLocator;
                selectedChair = chairLocator;
            }
            else
            {
                chairLocator++;
            }

            if (chairLocator == chairs.Length)
                chairLocator = 0;
        }
    }

    private void OnTriggerEnter(Collider other) //sit down and call waitress
    {
        if (inside)
        {
            if (other.gameObject.tag == "chair")
            {
                transform.position = new Vector3(other.transform.position.x, 0.15f, other.transform.position.z);

                if (other.transform.rotation.y == 0)
                    transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                if (other.transform.rotation.y < 0)
                    transform.rotation = Quaternion.Euler(0f, 90f, 0f);

                foundChair = true;
                isSeated = true;
                chairs[chairLocator].GetComponent<Chair>().IsAvailable = false;
                StartCoroutine(CallWaitressCR());
            }
        }
    }

    

    private IEnumerator CallWaitressCR()
    {
        yield return new WaitForSeconds(2.5f);
        callWaitress = true;
    }

    private void Eat()
    {
        if (hasBeenServed)
        {
            StartCoroutine(EatingTime());
        }
    }
    private IEnumerator EatingTime()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(10);
        transform.GetChild(2).gameObject.SetActive(false);
        leaving = true;
    }

    private void Leave()
    {
        if (leaving)
        {
            transform.GetChild(2).gameObject.SetActive(false);
            //walk towards door
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, door.transform.position, 0.02f);
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
            chairs[chairLocator].GetComponent<Chair>().IsAvailable = true;

            foreach (Collider c in Physics.OverlapSphere(transform.position, 0.3f))
            {
                if (c.gameObject.name == "doorA")
                {
                    gameObject.SetActive(false);

                    //reset bools
                    inside = false;
                    foundChair = false;
                    callWaitress = false;
                    isSeated = false;
                    ordered = false;
                    leaving = false;

                    Customer.customersLeft = true;
                }
            }
        }
    }
}

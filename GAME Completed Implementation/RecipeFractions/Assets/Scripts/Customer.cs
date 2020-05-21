using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

    //customerPrefab
    public List<GameObject> customerPrefabs;

    //static list of customers
    public static List<GameObject> customers;

    private int customerCounter;
    private float nextCustomerTimer;
    public static bool customersLeft;
    private int TotalCustomersHad;
    private int TotalCustomersLeft;

    // Use this for initialization
    void Start () {
        customerCounter = 0;
        nextCustomerTimer = 5f;
        customersLeft = false;
        TotalCustomersHad = 0;
        TotalCustomersLeft = 0;

        customers = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        if (customersLeft)
        {
            customerCounter--;
            TotalCustomersLeft++;
            customersLeft = false;
        }

        if (GameOptions.level == 1)
        {
            if (TotalCustomersLeft == 6)
                GameOptions.level++;

            if (customerCounter < 3 && TotalCustomersHad < 6) //three custs allowed in the shop at once
            {
                if (nextCustomerTimer >= 0)
                {
                    nextCustomerTimer -= Time.deltaTime;
                }

                if (nextCustomerTimer <= 0)
                {
                    //spawn customer
                    customers.Add(Instantiate(customerPrefabs[0], new Vector3(-2.25f, 0f, 6.363f), new Quaternion(0, 180f, 0, 0)));
                    customers[TotalCustomersHad].transform.GetChild(2).gameObject.SetActive(false);

                    //increment customer counter
                    customerCounter++;
                    TotalCustomersHad++;

                    //reset timer till next customers arrival
                    nextCustomerTimer = 10f; 
                }
            }
        }

        if (GameOptions.level == 2)
        {
            foreach(GameObject g in customers)
            {
                Destroy(g);
            }
            customers.Clear();
        }
	}
}

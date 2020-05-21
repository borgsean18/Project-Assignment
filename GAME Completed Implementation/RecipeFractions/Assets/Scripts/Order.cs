using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mehroz;

public class Order {

    private string pizzaType;
    private Fraction slices;
    private GameObject customer;

    public string PizzaType
    {
        get
        {
            return pizzaType;
        }

        set
        {
            pizzaType = value;
        }
    }

    public Fraction Slices
    {
        get
        {
            return slices;
        }

        set
        {
            slices = value;
        }
    }

    public GameObject Customer
    {
        get
        {
            return customer;
        }

        set
        {
            customer = value;
        }
    }

    public Order()
    {

    }

    public Order(string PType, Fraction Slics, GameObject cust)
    {
        this.pizzaType = PType;
        this.Slices = Slics;
        this.customer = cust;
    }
}

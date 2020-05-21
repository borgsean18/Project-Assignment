using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour {

    [SerializeField]
    private bool isAvailable;

    public bool IsAvailable
    {
        get
        {
            return isAvailable;
        }

        set
        {
            isAvailable = value;
        }
    }

    public Chair(bool isAvailable)
    {
        this.isAvailable = isAvailable;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private bool isOpen;
    private float timer;

	// Use this for initialization
	void Start () {
        isOpen = false;
        timer = 3f;
    }
	
	// Update is called once per frame
	void Update () {
		if (isOpen)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            isOpen = false;
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            timer = 3f;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "customer")
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isOpen = true;
        }
    }

    
}

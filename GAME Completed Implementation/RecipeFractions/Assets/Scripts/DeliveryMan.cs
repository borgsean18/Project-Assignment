using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMan : MonoBehaviour {

    //GameObjects
    public GameObject Prefab;

    //class vars
    public static bool spawnDeliveryMan;
    private int deliveryMen;

	// Use this for initialization
	void Start () {
        deliveryMen = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (spawnDeliveryMan && deliveryMen == 0)
        {
            spawnDeliveryMan = false;
            Instantiate(Prefab, new Vector3(-2.25f, 0f, 6.363f), new Quaternion(0, 18f, 0, 0));
            deliveryMen++;
        }
	}
}

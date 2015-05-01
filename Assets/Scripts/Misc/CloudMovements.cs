using UnityEngine;
using System.Collections;

public class CloudMovements : MonoBehaviour {
    GameObject Planet;
    public float speed;
	// Use this for initialization
	void Start () {
        speed = Random.Range(4f, 10f);
	}
	
	void Update () {
        gameObject.transform.RotateAround(new Vector3(0, 0, 0),Vector3.forward, Time.deltaTime* speed);
	}

   
}

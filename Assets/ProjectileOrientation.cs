using UnityEngine;
using System.Collections;

public class ProjectileOrientation : MonoBehaviour {
    public Rigidbody2D myRigidbody;
	// Use this for initialization
	void Start () {
        myRigidbody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.up = -myRigidbody.velocity;

	}
}

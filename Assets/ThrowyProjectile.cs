using UnityEngine;
using System.Collections;

public class ThrowyProjectile : MonoBehaviour {
    public Rigidbody2D myRigidbody;
	// Use this for initialization
	void Start () 
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        myRigidbody.AddForce(new Vector2(-1, 1) * 20, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}

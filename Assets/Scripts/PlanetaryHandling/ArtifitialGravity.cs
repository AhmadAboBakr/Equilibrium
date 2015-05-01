using UnityEngine;
using System.Collections;

public class ArtifitialGravity : MonoBehaviour {
     private Rigidbody2D myRigidBody;
	// Use this for initialization
	void Start () {
        myRigidBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        myRigidBody.velocity -= myRigidBody.position.normalized*GameState.gravity*Time.deltaTime;
	}
}

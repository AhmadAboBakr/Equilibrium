using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
    Rigidbody2D myRigidBody;

    
	// Use this for initialization
    public bool hit;
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        hit = false;
	}
	
	// Update is called once per frame
	void Update () {
         if (!hit)
        {
            this.transform.up = -myRigidBody.velocity;
        }
	}
    void OnCollisionEnter2D(Collision2D other){
        hit = true;
        StartCoroutine("destoyAfterTime");
    }
    IEnumerator destoyAfterTime(){
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
    Rigidbody2D myRigidBody;
    public GeneralPooling pooler;
    
	// Use this for initialization
    public bool hit;
    void Awake()
    {
    }
	void Start () {
        pooler = this.transform.parent.GetComponent<GeneralPooling>();
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
        hit = false;
        pooler.ReturnObjectToPool(this.gameObject);
    }
}

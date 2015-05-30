using UnityEngine;
using System.Collections;

public class RealTimeRotatingParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.up = this.transform.position;
        this.transform.rotation *= Quaternion.Euler(new Vector3(-90, 0, 0));
	
	}
}

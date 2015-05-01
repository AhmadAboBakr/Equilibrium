using UnityEngine;
using System.Collections;

public class RotatingButtons : MonoBehaviour {

    Transform myTransform;
    public float rotationSpeed;
	// Use this for initialization
	void Start () 
    {
        myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        myTransform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
	}
}

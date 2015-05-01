using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour {
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

	// Use this for initialization
	void Start () {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.rotation = target.transform.rotation;
        }
	}
}

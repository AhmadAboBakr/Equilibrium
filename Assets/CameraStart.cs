using UnityEngine;
using System.Collections;

public class CameraStart : MonoBehaviour {
    public float startSize;
    public float endSize;
    public Camera myCamera;
    public bool start;
    float time;
    public float duration;
    public float delay=.05f;
    public float elapsedTime;
	// Use this for initialization
    void Awake()
    {
        myCamera = this.GetComponent<Camera>();
        start = false;
    }
	void Start () 
    {
        elapsedTime = 0;
        myCamera.orthographicSize = startSize;
	}
	
	// Update is called once per frame
	void Update () 
    {
        myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, endSize, Time.deltaTime);      
	}
}

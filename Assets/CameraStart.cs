using UnityEngine;
using System.Collections;

public class CameraStart : MonoBehaviour {
    public float startSize;
    public float endSize;
    public Camera myCamera;
    public bool start;
    float time;
    public float duration;
	// Use this for initialization
    void Awake()
    {
        myCamera = this.GetComponent<Camera>();
        start = false;
    }
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        time = (Time.time - 0) / duration;
        myCamera.orthographicSize = Mathf.Lerp(startSize, endSize, time);
	}
}

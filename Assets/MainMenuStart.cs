using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {
    public Vector2 myPosition;
    public Quaternion myRotation;
    public GameObject startPoint;
    public Transform endPoint;
    public float time;
    public bool start;
	// Use this for initialization
	void Awake()
    {
        myPosition = endPoint.transform.position;
        myRotation = this.transform.rotation;
    }
    void Start () 
    {
        
	}
	void OnEnable()
    {
        this.transform.position = startPoint.transform.position;
        time = Random.Range(1, 3);
        start = true;
    }
	// Update is called once per frame
	void Update () 
    {
        if (start)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, myPosition, Time.deltaTime * time);
            if (Vector2.Distance(this.transform.position, myPosition) < .3f)
            {
                start = false;
            }
        }
        
	}

}

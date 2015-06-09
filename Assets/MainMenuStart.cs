using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {
    public Vector2 myPosition;
    public GameObject startPoint;
    public float time;
    public bool start;
    public Transform endPosition;
    public 
	// Use this for initialization
	void Awake()
    {
    }
    void Start () 
    {
        myPosition = endPosition.position;
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
            if (Vector2.Distance(this.transform.position, myPosition)<0.2f){
                start = false;
            }
            
        }

	}

}

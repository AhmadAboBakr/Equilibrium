using UnityEngine;
using System.Collections;

public class MainMenuStart : MonoBehaviour {
    public Vector2 myPosition;
    public Quaternion myRotation;
    public GameObject startPoint;
    public float time;
    public bool start;
	// Use this for initialization
	void Awake()
    {
        myPosition = this.transform.position;
        myRotation = this.transform.rotation;
    }
    void Start () 
    {
        this.transform.position = startPoint.transform.position;
        time = Random.Range(1, 3);
        start = false;
        StartCoroutine(Stop());
        StartCoroutine(delay());
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(start)
            this.transform.position = Vector2.Lerp(this.transform.position, myPosition, Time.deltaTime*time);
        
	}

    public IEnumerator Stop()
    {
        yield return new WaitForSeconds(5);
        this.GetComponent<MainMenuStart>().enabled = false;
    }
    public IEnumerator delay()
    {
        yield return new WaitForSeconds(0.2f);
        start = true;
    }
}

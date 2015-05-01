using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {
    public float timer;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if(timer > 5.8f)
        {
            Application.LoadLevel(1);
        }
	}
}

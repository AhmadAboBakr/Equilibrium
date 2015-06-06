using UnityEngine;
using System.Collections;

public class CloseGame : MonoBehaviour {
    public float timer;
    public bool inc;
	// Use this for initialization
	void Start () {
        timer = 0;
        inc = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(inc)
        {
            timer++;
        }
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (Input.GetKeyDown(KeyCode.Escape)&& timer >= 1)
            {
                Application.Quit();
            }
            inc = true;
            
        }
	}
}

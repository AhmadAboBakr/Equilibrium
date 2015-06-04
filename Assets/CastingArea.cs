using UnityEngine;
using System.Collections;

public class CastingArea : MonoBehaviour {
    public static CastingArea instance;
	// Use this for initialization
	void Start () {
	    if(!instance)
        {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}

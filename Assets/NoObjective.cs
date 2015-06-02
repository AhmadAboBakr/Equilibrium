using UnityEngine;
using System.Collections;

public class NoObjective : Objective {

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public override string getDescription()
    {
        return "";
    }

    public override bool checkObjective()
    {
        status = true;

        return status;
    }
}

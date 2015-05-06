using UnityEngine;
using System.Collections;

public abstract class Objective : MonoBehaviour {


    public bool status;

    public abstract string getDescription();
    
    public abstract bool checkObjective();
  

	
}

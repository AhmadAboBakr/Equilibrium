using UnityEngine;
using System.Collections;

public class TimeLimitObjective : Objective {
    public float timeLimit;
    public override string getDescription()
    {
        return "finish the level in less than "+ timeLimit + " seconds";
     }
}

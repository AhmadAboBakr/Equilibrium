using UnityEngine;
using System.Collections;

public class TimeLimitObjective : Objective {
    public float timeLimit;
    public override string getDescription()
    {
        return "finish the level in less than "+ timeLimit + " seconds";
     }

    public override bool checkObjective()
    {
        if (status)
            return status;
        if(GameManager.instance.gameElapsedTime <= timeLimit)
        {
            status = true;
        }
        else
        {
            status = false;
        }
        return status;
    }
   
}

using UnityEngine;
using System.Collections;

public class EnemyKillObjective : Objective {
    public int enemyKillCount;
	public override string getDescription()
    {
        return "Kill " + enemyKillCount + " enemies";
    }
    public override bool checkObjective()
    {
        if (status)
            return status;
        if(GameManager.instance.enemyKillCount >= enemyKillCount)
        {
            status = true;
        }
        
        return status;
    }
}

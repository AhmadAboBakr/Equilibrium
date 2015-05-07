using UnityEngine;
using System.Collections;

public class EnemyKillLimitObjective : Objective {
    public int enemyKillCount;
    public override string getDescription()
    {
        return "Kill less than " + enemyKillCount + " enemies";
    }

    public override bool checkObjective()
    {
        if (status)
            return status;
        if (GameManager.instance.enemyKillCount < enemyKillCount)
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

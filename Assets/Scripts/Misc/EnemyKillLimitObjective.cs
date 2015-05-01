using UnityEngine;
using System.Collections;

public class EnemyKillLimitObjective : Objective {
    public int enemyKillCount;
    public override string getDescription()
    {
        return "Kill less than " + enemyKillCount + " enemies";
    }
}

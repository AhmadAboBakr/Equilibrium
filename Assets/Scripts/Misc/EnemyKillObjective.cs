using UnityEngine;
using System.Collections;

public class EnemyKillObjective : Objective {
    public int enemyKillCount;
	public override string getDescription()
    {
        return "Kill " + enemyKillCount + " enemies";
    }
}

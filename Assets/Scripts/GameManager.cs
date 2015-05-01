using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    //Should refactor to have enemy instantiation add the enemy counter instead of checking here for performance sake
    public Text winCounter;
    int enemies;
    int buildings;
    int totalBuildings;
    int totalEnemies;
   void Start()
    {
        totalBuildings = GameObject.FindGameObjectsWithTag("Building").Length;
        
        StartCoroutine("checkBuildings");
    }
    IEnumerator checkBuildings()
    {
        while (true)
        {
            //counts all buildings
            buildings = GameObject.FindGameObjectsWithTag("Building").Length;
            if (buildings <= 0) 
            {   
                //when buildings are finished start counting enemies
                enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

                if(enemies <=0)
                {
                    //win condition
                    Debug.Log("you win");
                }
            }
            UpdateCounter();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpdateCounter()
    {
        if (buildings > 0)
            winCounter.text = buildings + " / " + totalBuildings;
        else
            winCounter.text = enemies.ToString();
    }
}

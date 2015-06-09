using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public enum planet{
    viking,red
}
public class GameManager : MonoBehaviour
{
    //Should refactor to have enemy instantiation add the enemy counter instead of checking here for performance sake
    public Text winCounter;
    public Image winCounterImage;
    public Sprite vikingBuildingSprite;
    public Sprite redBuildingSprite;
    public Sprite vikingEnemySprite;
    public Sprite redEnemySprite;
    public planet planetClass;
    int enemies;
    public int buildings;
    int totalBuildings;
    int totalEnemies;
    public Objective[] objectives;
    public static GameManager instance;
    public int enemyKillCount;
    public float gameElapsedTime;
    public planet currentPlanet;
    void Awake()
    {
        instance = this;
        objectives = transform.GetComponentsInChildren<Objective>();
        GameState.CurrentNumberOfEnemies = 0;
    }
    void Start()
    {
        enemyKillCount = 0;
        gameElapsedTime = 0;
        if (GameObject.FindGameObjectsWithTag("Plane").Length > 0)
        {
            totalBuildings = GameObject.FindGameObjectsWithTag("Plane").Length;
            StartCoroutine("checkPlanes");
            currentPlanet = planet.red;
        }
        else
        {
            totalBuildings = GameObject.FindGameObjectsWithTag("Building").Length;
            StartCoroutine("checkBuildings");
            currentPlanet = planet.viking;
        }

        switch (currentPlanet)
        {
            case planet.viking:
                winCounterImage.sprite = vikingBuildingSprite;
                break;
            case planet.red:
                winCounterImage.sprite = redBuildingSprite;
                break;
            default:
                break;
        }

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
                switch (currentPlanet)
                {
                    case planet.viking:
                        winCounterImage.sprite = vikingEnemySprite;
                        break;
                    case planet.red:
                        winCounterImage.sprite = redEnemySprite;
                        break;
                    default:
                        break;
                }
                if (enemies <= 0)
                {
                    //win state
                    InGameObjectiveUI.instance.gameObject.SetActive(true);
                }
            }
            UpdateCounter();
            //Check Player health for loss condition
            if (Player.player.HealthPoints <= 0)
            {
                LossUI.instance.gameObject.SetActive(true);
            }
            gameElapsedTime += 0.5f;
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator checkPlanes()
    {
        //counts all buildings
        buildings = GameObject.FindGameObjectsWithTag("Plane").Length;
        if (buildings <= 0)
        {
            //when buildings are finished start counting enemies
            enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

                //win state
                InGameObjectiveUI.instance.gameObject.SetActive(true);
        }
        UpdateCounter();
        //Check Player health for loss condition
        if (Player.player.HealthPoints <= 0)
        {
            LossUI.instance.gameObject.SetActive(true);
        }
        gameElapsedTime += 0.5f;
        yield return new WaitForSeconds(1.5f);

    }

    public void UpdateCounter()
    {
            winCounter.text = buildings + " / " + totalBuildings;
     
    }
}

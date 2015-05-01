using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {
    public List<GameObject> enemies;
    public float timer;
	// Use this for initialization
	void Start () {
        StartCoroutine("EnemySpawning");
        timer = 0;
	}
    void Update()
    {
        timer += Time.deltaTime;
        
    }
	
	// Update is called once per frame
	IEnumerator EnemySpawning () {
        while (true)
        {
            
            if (GameState.CurrentNumberOfEnemies < GameState.maxAllowedEnemies && timer > 0)
            {
                Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, transform.rotation);
                GameState.CurrentNumberOfEnemies++;
            }
            yield return new WaitForSeconds(1f);
        }
	}

    
    
}

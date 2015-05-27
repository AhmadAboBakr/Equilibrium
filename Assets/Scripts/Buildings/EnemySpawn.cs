using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {
    public GeneralPooling pooler;

	// Use this for initialization
	void Start () {
        pooler = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<GeneralPooling>();
        StartCoroutine("EnemySpawning");
	}
    void Update()
    {
        

    }
	
	// Update is called once per frame
	IEnumerator EnemySpawning () {
        while (true)
        {

            if (GameState.CurrentNumberOfEnemies < GameState.maxAllowedEnemies && Vector2.Distance(Player.player.transform.position, this.gameObject.transform.position)< 170f)
            {
                pooler.CreateObject(transform.position, transform.rotation);
                GameState.CurrentNumberOfEnemies++;
                
            }
            yield return new WaitForSeconds(1f);
        }
	}

    
    
}

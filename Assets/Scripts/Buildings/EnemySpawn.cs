using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {
    public GeneralPooling pooler;
    public int initialSpawn;
    public int spawned;
    bool isPlane;
	// Use this for initialization
    
	void Start () 
    {

        pooler = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<GeneralPooling>();
        initialSpawn = (int)(pooler.pooledObjects.Count / (float)(GameManager.instance.buildings+2));
        spawned = 0;
        StartCoroutine("EnemySpawning");
        isPlane=this.gameObject.CompareTag("Plane");
	}
    void OnDisable()
    {
        StopCoroutine("EnemySpawning");
    }
	
	// Update is called once per frame
    IEnumerator EnemySpawning()
    {
        while (true)
        {
            if (isPlane)
            {
                if (this.transform.GetComponent<PlaneScript>().dead)
                {
                    StopCoroutine("EnemySpawning");
                }
            }
            if (
                GameState.CurrentNumberOfEnemies < GameState.maxAllowedEnemies
                && 
                (
                    Vector2.Distance(Player.player.transform.position, this.gameObject.transform.position) < 160f
                    ||
                    spawned<initialSpawn
                ))
            {
                spawned++;
                pooler.CreateObject(transform.position, transform.rotation);
                GameState.CurrentNumberOfEnemies++;

            }
            yield return new WaitForSeconds(1f);
        }
    }  
}

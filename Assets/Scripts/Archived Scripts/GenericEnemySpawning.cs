using UnityEngine;
using System.Collections;

public class GenericEnemySpawning : MonoBehaviour {
    public float spawnRate=1;
    public bool isSpawning = true;
	// Use this for initialization
	void Start () 
    {
        StartCoroutine("spawnEnemy");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(isSpawning)
        {
            spawnEnemy();
        }
	}

    public IEnumerator spawnEnemy()
    {

       
        while(true)
        {

            GameObject obj = ObjectPoolerScript.current.getPooledObject();
            if(obj != null)
            {
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                obj.SetActive(true);
            }
             
            yield return new WaitForSeconds(spawnRate);

        }
        

    }
}

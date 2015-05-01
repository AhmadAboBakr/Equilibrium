using UnityEngine;
using System.Collections;

public class BuildingDestruction : MonoBehaviour {
    public float health = 5;
    float previousHealth;
    int randomObject;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if(health < previousHealth)
            {
                randomObject = Random.Range(0, transform.childCount);
                transform.GetChild(randomObject).gameObject.GetComponent<BoxCollider2D>().enabled = true;
                transform.GetChild(randomObject).gameObject.AddComponent<Rigidbody2D>();
                transform.GetChild(randomObject).gameObject.AddComponent<ArtifitialGravity>();
                transform.GetChild(randomObject).gameObject.GetComponent<Rigidbody2D>().AddForce((transform.GetChild(randomObject).position - Player.player.transform.position) * 40);
                previousHealth = health;
            }
            
            
        }
    }
	// Use this for initialization
	void Start () 
    {
        previousHealth = health;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

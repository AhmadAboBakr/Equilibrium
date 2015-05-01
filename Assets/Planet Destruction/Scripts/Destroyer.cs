using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Planet")
        {
            PlanetClass planet = other.gameObject.GetComponent<PlanetClass>();
            planet.Dig(GetComponent<PolygonCollider2D>(), transform.position+Vector3.down/10.0f);
            Destroy(gameObject, 0.1f);
        }
    }
}

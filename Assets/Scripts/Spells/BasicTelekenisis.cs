using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicTelekenisis : MonoBehaviour{
    List<Transform> pickables;
    public int maxNumberOfEnemies;
	void Start () {
        pickables = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        //get mouse position and add 10 in the z pos so it is on the same z pos with the rest of the game objects.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        //More spell marker with mouse position
        transform.position=mousePosition;

        //Add force to all pickable objects in the list towards the center of the spell marker
        foreach (var item in pickables)
        {
            item.GetComponent<Rigidbody2D>().velocity = -(item.transform.position- this.transform.position)*10 ;
        }
        Player.player.Mana -= SpellManager.manager.allSpells[0].manaCost * Time.deltaTime;
        if(Player.player.Mana<=0)
        {
            Destroy(this.gameObject);
        }
	}
    void OnTriggerEnter2D(Collider2D other) {
        //Add pickables into the list if the list isnt already full
        if (pickables.Count < maxNumberOfEnemies)
        {
            pickables.Add(other.transform);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Up for revision
        pickables.Remove(other.transform);
    }
  
}

using UnityEngine;
using System.Collections;

public class ArriveEnemy : MonoBehaviour {
    GameObject Player;
    AILib AIManager;
    public float speed = 5f;
    public float slowingArea = 200f;
    public float distanceFromGiant = 0;
	// Use this for initialization
	void Start () {

        Player = GameObject.FindGameObjectWithTag("Player");
        AIManager = new AILib(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 temp = AIManager.Arrive(new Vector2(Player.transform.position.x + distanceFromGiant + Player.GetComponent<SpriteRenderer>().bounds.size.x / 2 , Player.transform.position.y - Player.GetComponent<SpriteRenderer>().bounds.size.y / 2), speed, slowingArea);
        CircularMotion ctemp = AIManager.CartToPolar(temp);
        if (ctemp.direction == Direction.AntiClockWise && ctemp.speed>0.1)
            gameObject.GetComponent<SurfaceMovingObject>().Move(-ctemp.speed);
        else if (ctemp.direction == Direction.ClockWise && ctemp.speed > 0.1)
            gameObject.GetComponent<SurfaceMovingObject>().Move(ctemp.speed);
	}
}

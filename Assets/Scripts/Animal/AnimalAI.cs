using UnityEngine;
using System.Collections;

public class AnimalAI : MonoBehaviour {
    GameObject Player;
    AILib AIManager;
    public float fleeRange = 50;
    public float fleeSpeed = 5;
    public float wanderSpeed = 5;
    public float wanderRange = 30;
    public float timeToRepeat = 6;
    CircularMotion point; 
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        AIManager = new AILib(gameObject);
        StartCoroutine("randomPoint");
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(Player.transform.position, this.transform.position) < fleeRange)
        {
            Vector2 temp = AIManager.Flee(new Vector2(Player.transform.position.x + Player.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                Player.transform.position.y - Player.GetComponent<SpriteRenderer>().bounds.size.y / 2), fleeSpeed);
            CircularMotion ctemp = AIManager.CartToPolar(temp);
            if (ctemp.direction == Direction.AntiClockWise)
                gameObject.GetComponent<SurfaceMovingObject>().Move(-ctemp.speed);
            else if (ctemp.direction == Direction.ClockWise)
                gameObject.GetComponent<SurfaceMovingObject>().Move(ctemp.speed);
        }
        else
        {
            
            if (point.direction == Direction.AntiClockWise)
                gameObject.GetComponent<SurfaceMovingObject>().Move(-point.speed);
            else if (point.direction == Direction.ClockWise)
                gameObject.GetComponent<SurfaceMovingObject>().Move(point.speed);
        }
	}

    IEnumerator randomPoint()
    {
        while (true)
        {
            
            float temp = Random.Range(-wanderRange,wanderRange+1);
            float temp2 = Random.Range(-wanderRange, wanderRange + 1);
            Vector2  tempPoint = new Vector2(transform.position.x + temp,transform.position.y + temp2);
             point = AIManager.Wander(tempPoint, wanderSpeed);
             //GameObject.Instantiate(this.gameObject,tempPoint,Quaternion.identity);
            yield return new WaitForSeconds(timeToRepeat);
        }
    }
}


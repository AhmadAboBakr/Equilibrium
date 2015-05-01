using UnityEngine;
using System.Collections;

public class BirdAI : MonoBehaviour
{
    public GameObject Player;
    public bool isActivated;
    public float escapeRange;
    public int direction;
    float dottaya;
    MoveItMoveIt SMO;
    // Use this for initialization
    void Start()
    {
        isActivated = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        SMO = gameObject.GetComponent<MoveItMoveIt>();
        direction = (int)Random.Range(4f, 12f);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isActivated && Vector2.Distance(Player.transform.position, this.transform.position) < escapeRange)
        {
            isActivated = true;
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            dottaya = Vector2.Dot(this.transform.right, Player.transform.position - this.transform.position);
            if (dottaya > 0)
            {
                dottaya = -1;
            }
            else if (dottaya < 0)
            {
                dottaya = 1;
            }
            direction *= (int)dottaya;
            SMO.Move(direction);
            isActivated = true; 
        }
        if(isActivated)
        {
            StartCoroutine("destroyAfterEscape");
        }
        
    }

    public IEnumerator destroyAfterEscape()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    }
}

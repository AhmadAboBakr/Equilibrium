using UnityEngine;
using System.Collections;

public class GiantDeath : MonoBehaviour {
    public static GiantDeath instance;
    Animator myAnimator;
    GiantMovementController myMovement;
	// Use this for initialization
	void Start () 
    {
        if(!instance)
        {
            instance = this;
        }
        myAnimator = this.GetComponentInChildren<Animator>();
        myMovement = this.GetComponentInChildren<GiantMovementController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Die()
    {
        myAnimator.enabled = false;
        myMovement.enabled = false;
        foreach (var item in GetComponentsInChildren<DestructableComponent>())
        {
            item.SelfDestruct(this.transform);
        }
    }
}

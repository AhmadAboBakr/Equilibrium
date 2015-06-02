using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {
    public ParticleSystem myParticle;
    public float timer;
	// Use this for initialization
	void Start () 
    {
        timer = 0;
        myParticle = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer>myParticle.startLifetime)
        {
            Destroy(this.gameObject);
            
        }
	}
}

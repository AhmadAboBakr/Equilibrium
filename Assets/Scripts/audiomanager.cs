using UnityEngine;
using System.Collections;

public class audiomanager : MonoBehaviour {
    AudioSource soundtrack;
    public static audiomanager instance;
    void Awake()
    {
        soundtrack = gameObject.GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}
}

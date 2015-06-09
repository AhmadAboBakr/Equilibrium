using UnityEngine;
using System.Collections;

public class audiodisable : MonoBehaviour {
    void Awake()
    {
        FindObjectOfType<audiomanager>().gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

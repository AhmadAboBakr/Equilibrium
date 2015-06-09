using UnityEngine;
using System.Collections;

public class audioenable : MonoBehaviour {
    void Awake()
    {
        audiomanager.instance.gameObject.SetActive(true);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

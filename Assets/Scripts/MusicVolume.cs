using UnityEngine;
using System.Collections;

public class MusicVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource soundtrack = gameObject.GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("MusicValue"))
        {
           soundtrack.volume =  PlayerPrefs.GetFloat("MusicValue");
        }
	
	}
	
	
}

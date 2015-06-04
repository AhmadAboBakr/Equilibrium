using UnityEngine;
using System.Collections;

public class SoundeffectVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource soundeffect = gameObject.GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("SoundValue"))
        {
            soundeffect.volume = PlayerPrefs.GetFloat("SoundValue");
        }
	
	}
	
	
}

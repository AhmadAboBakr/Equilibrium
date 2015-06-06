using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject musicSlider;
    public GameObject soundSlider;
	// Use this for initialization
	void Start () {
       if(!PlayerPrefs.HasKey("MusicValue"))
       {
           PlayerPrefs.SetFloat("MusicValue", 1f);
       }
       if (!PlayerPrefs.HasKey("SoundValue"))
       {
           PlayerPrefs.SetFloat("SoundValue", 1f);
       }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
	}

    void OnEnable() {

        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicValue");
        soundSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundValue");

    }

    public void Back()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SetMusic()
    {

        PlayerPrefs.SetFloat("MusicValue", musicSlider.GetComponent<Slider>().value);

    }
    public void SetSound()
    {
        PlayerPrefs.SetFloat("SoundValue", soundSlider.GetComponent<Slider>().value);

    }
}

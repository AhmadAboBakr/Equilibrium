using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FrameCounter : MonoBehaviour {
    int frames = 0;
    public Text txt_FrameCounter;
	// Use this for initialization
	void Start () {
	
	}
    float elapsedTime = 0;
	// Update is called once per frame
	void Update () {
        frames++;
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 1)
        {
            elapsedTime = 0;
            //update frmae counter ui
            txt_FrameCounter.text ="Frames Count:"+ frames.ToString();
            frames = 0;
        }
	}
}

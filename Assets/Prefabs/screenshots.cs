using UnityEngine;
using System.Collections;
using System.IO;

public class screenshots : MonoBehaviour {
    public RenderTexture myRenderTexture;
    Texture2D myTexture2D;
	// Use this for initialization
	void Start () {
        myTexture2D = new Texture2D(4096, 4096);
        
	}
	
	// Update is called once per frame
	void Update () {
        RenderTexture.active = myRenderTexture;
        myTexture2D.ReadPixels(new Rect(0, 0, myRenderTexture.width, myRenderTexture.height), 0, 0);
        myTexture2D.Apply();
        var bytes = myTexture2D.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "SavedScreen.png", bytes);
	}
}

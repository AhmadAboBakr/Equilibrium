using UnityEngine;
using System.Collections;

public class Screenshots : MonoBehaviour
{
    public int hamada;
    // Use this for initialization
    void Start()
    {
        hamada = 0;

        //StartCoroutine(GetScreen());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator GetScreen()
    {
        while(true)
        {
            
            hamada++;
            Application.CaptureScreenshot("C:\\Users\\SDAndGaming\\Documents\\Bakr\\ssss\\Equilibrium\\Assets\\Hamada\\screenshot"+ hamada+".png");
            yield return new WaitForSeconds(1.5f);
        }
    }
}

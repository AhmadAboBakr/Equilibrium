using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {
    public PlanetClass planet;
    public GameObject d;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos+Vector3.forward;
            //planet.Dig(GetComponent<PolygonCollider2D>(), transform.position);
            GameObject destroyer= (Instantiate(d) as GameObject);
            destroyer.transform.position = mousePos + Vector3.forward;
        }
	}
}

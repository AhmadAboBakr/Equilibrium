using UnityEngine;
using System.Collections;

public class  Planet : MonoBehaviour {
    public float width = 183.5f;
    public GameObject outerSegmentPrefab;
    public GameObject  midSegmentPrefabs;
    public GameObject innersegmentprefab;
    public int count = 0;
    public float scale;
    public void Build()
    {
        count = 0;
        float innerWidth = width;
        float segmentWidth = outerSegmentPrefab.GetComponent<Renderer>().bounds.size.x ;
        float deltaTheta = outerSegmentPrefab.GetComponent<Renderer>().bounds.size.y / (innerWidth + segmentWidth);
        //for (float theta = 0; theta < 2 * Mathf.PI; theta += deltaTheta)
        //{
        //    var segment = GameObject.Instantiate(outerSegmentPrefab, new Vector3(innerWidth, 0, 0), Quaternion.identity) as GameObject;
        //    segment.transform.RotateAround(Vector3.zero, this.transform.forward, theta * 180 / Mathf.PI);
        //    segment.transform.parent = this.transform;
        //    count++;
        //}
        innerWidth -= segmentWidth ;
        segmentWidth = midSegmentPrefabs.GetComponent<Renderer>().bounds.size.x ; 
        deltaTheta = midSegmentPrefabs.GetComponent<Renderer>().bounds.size.y  / (innerWidth+segmentWidth);
        
        for (float theta = 0; theta < 2 * Mathf.PI; theta += deltaTheta)
        {
            var segment = GameObject.Instantiate(midSegmentPrefabs, new Vector3(innerWidth, 0, 0), Quaternion.identity) as GameObject;
            segment.transform.RotateAround(Vector3.zero, this.transform.forward, theta * 180 / Mathf.PI);
            segment.transform.parent = this.transform;
            count++;
        }
        innerWidth -= segmentWidth;

        scale= 1;
        int i = 1;
        while (innerWidth > width/2)
        {
            innerWidth -= segmentWidth * .99f;
            segmentWidth = innersegmentprefab.GetComponent<Renderer>().bounds.size.x * scale ;
            deltaTheta = innersegmentprefab.GetComponent<Renderer>().bounds.size.y *scale/ (innerWidth+segmentWidth);
  
            for (float theta = 0; theta < 2 * Mathf.PI; theta += deltaTheta)
            {
                var segment = GameObject.Instantiate(innersegmentprefab, new Vector3(innerWidth, 0, 0), Quaternion.identity) as GameObject;
                segment.transform.localScale *= scale;
                segment.transform.RotateAround(Vector3.zero, this.transform.forward, theta * 180 / Mathf.PI);
                segment.transform.parent = this.transform;
                segment.name = "inner" + i;
                count++;
            }
            i++;
            scale *= 1.1f;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPooler : MonoBehaviour
{
    public SurfaceMovingObject[] enimies;
    void Start()
    {
        var pooledObjects = this.GetComponent<GeneralPooling>().pooledObjects;
        enimies = new SurfaceMovingObject[pooledObjects.Count];
        for (int i = 0; i < enimies.Length; i++)
        {
            enimies[i] = pooledObjects[i].GetComponent<SurfaceMovingObject>();
        }
        StartCoroutine("ResetCollider");
    }
    IEnumerator ResetCollider()
    {
        for (int i = 0; i < enimies.Length; i++)
        {
            if (enimies[i].gameObject.activeInHierarchy)
                enimies[i].DisableCollider();
        }
        yield return new WaitForSeconds(.1f) ;
        for (int i = 0; i < enimies.Length; i++)
        {
            if (enimies[i].gameObject.activeInHierarchy)
                enimies[i].EnableCollider();
        }
        yield return new WaitForSeconds(1);
    }
}

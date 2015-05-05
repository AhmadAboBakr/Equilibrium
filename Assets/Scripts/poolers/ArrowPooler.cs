using UnityEngine;
using System.Collections;

public class ArrowPooler : GeneralPooling{
    //List<GameObject> pooledObjects;
    //override void Initialize()
    //{
    //    pooledObjects = new List<GameObject>();

    //}
    //override public GameObject CreateObject(Vector3 position, Quaternion angle)
    //{
    //    GameObject pooledObject;
    //    if (pooledObjects.Count > 0)
    //    {
    //        pooledObject = pooledObjects[0];
    //        pooledObject.SetActive(true);
    //        pooledObject.transform.parent = null;
    //    }
    //    else
    //    {
    //        pooledObject = GameObject.Instantiate(objectsToPool[Random.Range(0, objectsToPool.Length)]) as GameObject;
    //    }
    //    return pooledObject;
    //}
    //override public GameObject ReturnObjectToPool(GameObject pooledObject)
    //{
    //    pooledObject.transform.parent = this.transform;
    //    pooledObject.SetActive(false);
    //    return pooledObject;
    //}
}

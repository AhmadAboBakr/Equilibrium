using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GeneralPooling : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    //public GameObject [] objectsToPool;
    public int numberOfObjects;
    
    public void Build(pooledObjectData[] objectsToPool)
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < objectsToPool.Length; i++)
        {
            for (int j = 0; j < objectsToPool[i].count; j++)
            {
                var pooledObject = GameObject.Instantiate(objectsToPool[i].pooledObject) as GameObject;
                pooledObject.transform.parent = this.transform;
                pooledObject.SetActive(false);
                pooledObjects.Add(pooledObject);
            }
        }
        pooledObjects.Sort((x, y) => Random.Range(-7, 8));
        pooledObjects.Sort((x, y) => Random.Range(-7, 8));
        pooledObjects.Sort((x, y) => Random.Range(-7, 8));
        pooledObjects.Sort((x, y) => Random.Range(-7, 8));
        pooledObjects.Sort((x, y) => Random.Range(-7, 8));
    }
    public void Clear()
    {

    }
    virtual public GameObject CreateObject(Vector3 position, Quaternion angle)
    {

        GameObject pooledObject;
        if (pooledObjects.Count > 0)
        {
            pooledObject = pooledObjects[0];
            pooledObjects.Remove(pooledObject);
            pooledObject.SetActive(true);
            //pooledObject.transform.parent = null;
            pooledObject.transform.position = position;
            pooledObject.transform.rotation = angle;
        }
        else
        {
            //pooledObject = GameObject.Instantiate(objectsToPool[Random.Range(0, objectsToPool.Length)]) as GameObject;
            //pooledObject.transform.parent = (parentObject) ? parentObject : this.transform;
            return null;
        }
        return pooledObject;
    }
    virtual public GameObject ReturnObjectToPool(GameObject pooledObject)
    {
        pooledObject.transform.parent = this.transform;
        pooledObject.SetActive(false);
        pooledObjects.Add(pooledObject);
        return pooledObject;
    }
}

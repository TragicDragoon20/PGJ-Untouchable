using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> freePooledObjects;
    private List<GameObject> usedPoolObjects;

    [SerializeField]
    private int poolSize;

    [SerializeField]
    private GameObject prefab;

    public static ObjectPooler Instance;

    public void Awake()
    {
        Instance = this;

        freePooledObjects = new List<GameObject>(poolSize);
        usedPoolObjects = new List<GameObject>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledObject = Instantiate(prefab, this.transform);
            pooledObject.SetActive(false);
            freePooledObjects.Add(pooledObject);
        }
    }

    public GameObject GetPooledObject()
    {
        int currentPoolSize = freePooledObjects.Count;
        if (currentPoolSize == 0)
        {
            return null;
        }

        GameObject pooledObject = freePooledObjects[currentPoolSize - 1];
        freePooledObjects.RemoveAt(currentPoolSize - 1);
        usedPoolObjects.Add(pooledObject);
        return pooledObject;
    }

    public void ReturnObject(GameObject poolObject)
    {
        var pooledObjectTransform = poolObject.transform;
        pooledObjectTransform.parent = transform;
        pooledObjectTransform.localPosition = Vector3.zero;
        poolObject.gameObject.SetActive(false);

    }
}

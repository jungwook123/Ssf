using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pooler
{
    ObjectPool<GameObject> pool;
    GameObject prefab;
    public Pooler(GameObject prefab, int maxSize = 100, int defaultSize = 0)
    {
        this.prefab = prefab;
        pool = new ObjectPool<GameObject>(
            PoolCreate,
            OnPoolTakeout,
            OnPoolInsert,
            PoolDestroy,
            true,
            defaultSize,
            maxSize);
    }
    public GameObject GetObject()
    {
        return pool.Get();
    }
    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetObject();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }
    public GameObject GetObject(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject obj = GetObject(position, rotation);
        obj.transform.parent = parent;
        return obj;
    }
    public GameObject GetObject(Transform parent)
    {
        GameObject obj = GetObject();
        obj.transform.parent = parent;
        return obj;
    }
    public void ReleaseObject(GameObject obj)
    {
        pool.Release(obj);
    }
    void OnPoolTakeout(GameObject pooledObject)
    {
        pooledObject.SetActive(true);
    }
    void OnPoolInsert(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
    }
    GameObject PoolCreate()
    {
        return MonoBehaviour.Instantiate(prefab);
    }
    void PoolDestroy(GameObject pooledObject)
    {
        MonoBehaviour.Destroy(pooledObject);
    }
}

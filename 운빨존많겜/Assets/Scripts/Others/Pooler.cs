using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SearchService;

public class Pooler<T> where T : Component
{
    ObjectPool<T> pool;
    T prefab;
    public Pooler(T prefab, int maxSize = 100, int defaultSize = 0)
    {
        this.prefab = prefab;
        pool = new ObjectPool<T>(
            PoolCreate,
            OnPoolTakeout,
            OnPoolInsert,
            PoolDestroy,
            true,
            defaultSize,
            maxSize);
    }
    public T GetObject()
    {
        return pool.Get();
    }
    public T GetObject(Vector3 position, Quaternion rotation)
    {
        T obj = GetObject();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }
    public T GetObject(Vector3 position, Quaternion rotation, Transform parent)
    {
        T obj = GetObject(position, rotation);
        obj.transform.parent = parent;
        return obj;
    }
    public T GetObject(Transform parent)
    {
        T obj = GetObject();
        obj.transform.parent = parent;
        return obj;
    }
    public void ReleaseObject(T obj)
    {
        pool.Release(obj);
    }
    void OnPoolTakeout(T pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }
    void OnPoolInsert(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }
    T PoolCreate()
    {
        return MonoBehaviour.Instantiate(prefab);
    }
    void PoolDestroy(T pooledObject)
    {
        MonoBehaviour.Destroy(pooledObject.gameObject);
    }
}
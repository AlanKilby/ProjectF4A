using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pooler : MonoBehaviour
{
    public static Pooler instance;

    Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    [SerializeField] private List<PoolKey> poolKeys = new List<PoolKey>();

    private PhotonView view;

    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public Queue<GameObject> queue = new Queue<GameObject>();

        public int baseCount;
        public float baseRefreshSpeed = 5;
        public float refreshSpeed = 5;
    }

    [System.Serializable]
    public class PoolKey
    {
        public string key;
        public Pool pool;
    }

    private void Awake()
    {
        instance = this;

        view = transform.GetComponent<PhotonView>();

        InitPools();
        PopulatePools();
    }

    int i;

    private void InitPools()
    {
        for (i = 0; i < poolKeys.Count; i++)
        {
            pools.Add(poolKeys[i].key, poolKeys[i].pool);
        }
    }


    private void PopulatePools()
    {
        foreach (var pool in pools)
        {
            PopulatePool(pool.Value);
        }
    }

    private void PopulatePool(Pool pool)
    {
        for (i = 0; i < pool.baseCount; i++)
        {
            AddInstance(pool);
        }
    }

    GameObject objectInstance;
    private void AddInstance(Pool pool)
    {
        objectInstance = PhotonNetwork.Instantiate(pool.prefab.name, new Vector3(-1000, -1000, -1000), Quaternion.identity);
        objectInstance.SetActive(false);

        pool.queue.Enqueue(objectInstance);
    }

    private void Start()
    {
        InitRefreshCount();
    }

    private void InitRefreshCount()
    {
        foreach (KeyValuePair<String, Pool> pool in pools)
        {
            StartCoroutine(RefreshPool(pool.Value, pool.Value.refreshSpeed));
        }
    }

    IEnumerator RefreshPool(Pool pool, float t)
    {
        yield return new WaitForSeconds(t);

        if (pool.queue.Count < pool.baseCount)
        {
            AddInstance(pool);
            pool.refreshSpeed = pool.baseRefreshSpeed * pool.queue.Count / pool.baseCount;
        }

        StartCoroutine(RefreshPool(pool, pool.refreshSpeed));
    }

    public GameObject Pop(string key)
    {
        if (pools[key].queue.Count == 0)
        {
            Debug.LogWarning("pool of " + key + " is empty");
            AddInstance(pools[key]);
        }

        objectInstance = pools[key].queue.Dequeue();
        objectInstance.SetActive(true);

        return objectInstance;
    }

    private string keyRPC;
    private GameObject goRPC;
    private Rigidbody rbRPC;
    

    public void Depop(string key, GameObject go, Rigidbody rb)
    {
        keyRPC = key;
        goRPC = go;
        rbRPC = rb;

        view.RPC("DepopRPC", RpcTarget.All);
    }

    [PunRPC]
    public void DepopRPC() 
    {
        pools[keyRPC].queue.Enqueue(goRPC);
        goRPC.transform.parent = transform;
        rbRPC.velocity = Vector3.zero;
        goRPC.SetActive(false);
    }

    public void DelayedDepop(float t, string key, GameObject go, Rigidbody rb)
    {
        StartCoroutine(DelayedDepopCoroutine(t, key, go, rb));
    }

    IEnumerator DelayedDepopCoroutine(float t, string key, GameObject go, Rigidbody rb)
    {
        yield return new WaitForSeconds(t);

        Depop(key, go, rb);
    }
}

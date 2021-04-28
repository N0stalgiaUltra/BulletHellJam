using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    //Singleton
    // Controla a instancia de inimigos
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public Transform pos;
        public int size;
    }
    #region Singleton
    public static BulletPooling Instance;
        private void Awake() {
            Instance = this;
        }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool p in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < p.size; i++)
            {
                GameObject a = Instantiate(p.prefab);
                a.SetActive(false);
                objectPool.Enqueue(a);
            }
            poolDictionary.Add(p.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("There´s no key in the queue");
            return null; 
        }

        if(poolDictionary.Count != 0)
        {
            GameObject o = poolDictionary[tag].Dequeue();  
            o.SetActive(true);
            return o;
        }
        else
        {
            Debug.LogWarning("Empty Queue!");
            return null;
        }
    }

    public void SetFalse(GameObject go, string tag)
    {
        if(go != null)
        {
            go.SetActive(false);
            poolDictionary[tag].Enqueue(go);
        }
            
        else 
            return;
    }
}

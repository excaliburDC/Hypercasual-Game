using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject[] itemPrefab;
    //void Start()
    //{
    //    SpawnItem();
    //}

    public void SpawnItem()
    {
        int count = Random.Range(0, itemPrefab.Length);
        GameObject item_Obj = Instantiate(itemPrefab[count]);
        item_Obj.transform.position = transform.position;
    }
}

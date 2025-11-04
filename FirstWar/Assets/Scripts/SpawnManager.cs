using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Monster monsterPrefab;
    public Item itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn_Coroutine());
        StartCoroutine(Spawn_Item_Coroutine());
    }

    IEnumerator Spawn_Coroutine()
    {
        float xPos = Random.Range(-4.3f, 4.3f);
        float zPos = Random.Range(33.5f, 55.5f);
        Instantiate(monsterPrefab, new Vector3(xPos, 0.05f, zPos), Quaternion.Euler(0, 178.0f, 0));
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

        StartCoroutine(Spawn_Coroutine());
    }

    IEnumerator Spawn_Item_Coroutine()
    {
        float zPos = Random.Range(33.5f, 55.5f);
        Instantiate(itemPrefab, new Vector3(0.0f, 1.5f, zPos), Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(3.0f, 3.0f));

        StartCoroutine(Spawn_Item_Coroutine());
    }
}

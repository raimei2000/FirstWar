using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Item itemPrefab;

    public ScoreText scoreText;

    public WeightedRandomPicker picker;

    void Start()
    {
        StartCoroutine(Spawn_Coroutine());
        StartCoroutine(Spawn_Item_Coroutine());
    }

    IEnumerator Spawn_Coroutine()
    {
        if (GameManager.Instance.isGameOver == false)
        {
            float xPos = Random.Range(-4.3f, 4.3f);
            float zPos = Random.Range(50.5f, 55.5f);

            // monster의 체력 가져오기
            int hpToGive = GameManager.Instance.GetCurrentMonsterHealth();

            // 생성된 monster에 ScoreText reference, hp 넘겨주기
            GameObject newMonster = Instantiate(monsterPrefab, new Vector3(xPos, 0.05f, zPos), Quaternion.Euler(0, 178.0f, 0));
            Monster monsterScript = newMonster.GetComponent<Monster>();
            if (monsterScript != null )
            {
                monsterScript.Initialize(scoreText, hpToGive);
            }

            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

            StartCoroutine(Spawn_Coroutine());
        }
        else
        {
            StopCoroutine(Spawn_Coroutine());
        }
    }

    IEnumerator Spawn_Item_Coroutine()
    {
        if (GameManager.Instance.isGameOver == false)
        {
            var items = picker.PickTwo();
            //float zPos = Random.Range(33.5f, 55.5f);

            Item newItem = Instantiate(itemPrefab, new Vector3(0.0f, 1.5f, 50.0f), Quaternion.identity);
            newItem.Initiate(items.item1.itemIndex, items.item2.itemIndex);
            Destroy(newItem.gameObject, 14.0f);

            yield return new WaitForSeconds(5.0f);

            StartCoroutine(Spawn_Item_Coroutine());
        }
        else
        {
            StopCoroutine(Spawn_Item_Coroutine());
        }
    }
}

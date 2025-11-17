using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class WeightedItem
{
    // 0: ATK_Speed     1: ATK_Count    2: ATK_Up
    public int itemIndex;
    public int weight;
}

public class WeightedRandomPicker : MonoBehaviour
{
    public List<WeightedItem> itemPool;

    public WeightedItem PickOne(List<WeightedItem> pool)
    {
        int totalWeight = pool.Sum(item => item.weight);

        int randomNumber = Random.Range(0, totalWeight);

        foreach (var item in pool)
        {
            if (randomNumber < item.weight)
            {
                return item;
            }
            randomNumber -= item.weight;
        }
        // 정상 작동시 여기까지 오진않음.
        return null;
    }

    public (WeightedItem item1, WeightedItem item2) PickTwo()
    {
        List<WeightedItem> tempPool = new List<WeightedItem>(itemPool);

        WeightedItem item1 = PickOne(tempPool);
        if (item1 == null) return (null, null);

        tempPool.Remove(item1);
        WeightedItem item2 = PickOne(tempPool);

        return (item1, item2);
    }
}

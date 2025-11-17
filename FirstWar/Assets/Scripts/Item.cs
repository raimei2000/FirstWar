using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ItemState { ATK_Speed, ATK_Count, ATK_Up, SPD_Up };

public class Item : MonoBehaviour
{
    private string[] itemNames =
    {
        "Attack Speed",
        "Attack Count",
        "Attack Damage",
        "Speed Up"
    };

    public GameObject[] Cubes;
    ItemState[] items = new ItemState[2];

    public Material[] materials;

    public TextMeshProUGUI[] texts;

    public float speed;

    void Start()
    {
        //items[0] = (ItemState)Random.Range(0, 2);
        //items[1] = items[0] == ItemState.ATK_Speed ? ItemState.ATK_Count : ItemState.ATK_Speed;

        //Debug.Log("Item Start: " + items[0] + " " + items[1]);

        for (int i = 0; i < Cubes.Length; i++)
        {
            Cubes[i].GetComponent<Renderer>().material = materials[(int)items[i]];
            texts[i].text = itemNames[(int)items[i]];
            Cubes[i].tag = items[i].ToString();
        }
    }

    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }

    public void Initiate(int index1, int index2)
    {
        items[0] = (ItemState)index1;
        items[1] = (ItemState)index2;
    }
}

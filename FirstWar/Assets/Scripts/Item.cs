using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ItemState { ATK_Speed, ATK_Count };

public class Item : MonoBehaviour
{
    public GameObject[] Cubes;
    ItemState[] items = new ItemState[2];

    public Material[] materials;

    public TextMeshProUGUI[] texts;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        items[0] = (ItemState)Random.Range(0, 2);
        items[1] = items[0] == ItemState.ATK_Speed ? ItemState.ATK_Count : ItemState.ATK_Speed;

        //Debug.Log("Item Start: " + items[0] + " " + items[1]);

        for (int i = 0; i < Cubes.Length; i++)
        {
            Cubes[i].GetComponent<Renderer>().material = materials[(int)items[i]];
            texts[i].text = items[i].ToString();
            Cubes[i].tag = items[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }
}

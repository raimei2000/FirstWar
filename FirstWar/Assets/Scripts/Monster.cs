using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    // speed of monster
    public float speed;
    public int HP = 3;

    public Slider healthBar;
    public int maxHP;

    Animator anim;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = HP;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit!");
            HP--;

            healthBar.value = (float)HP / maxHP;

            if (isDead == false)
            {
                if (HP <= 0)
                {
                    isDead = true;

                    Destroy(GetComponent<Rigidbody>());
                    GetComponent<CapsuleCollider>().enabled = false;
                    anim.SetTrigger("Death");
                }
                Destroy(other.transform.parent.gameObject); // 만난 건 capsule 오브젝트.
            }
        }
    }
}

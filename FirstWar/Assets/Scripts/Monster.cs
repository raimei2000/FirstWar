using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    // speed of monster
    public float speed;
    public int HP = 2;

    public Slider healthBar;
    public int maxHP;

    Animator anim;
    bool isDead = false;

    public GameObject hitParticle;

    private ScoreText scoreText;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = HP;
        anim = GetComponent<Animator>();
        //Debug.Log("HP " + maxHP.ToString() + "인 몬스터 생성.");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false && GameManager.Instance.isGameOver == false)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    public void Initialize(ScoreText target, int hp)
    {
        scoreText = target;
        HP = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            //Debug.Log("Hit!");
            HP -= GameManager.Instance.playerAttack;
            //Debug.Log(GameManager.Instance.playerAttack + "만큼 데미지");

            SoundManager.instance.AudioStart(0);

            Instantiate(hitParticle, other.gameObject.transform.position, Quaternion.identity);


            healthBar.value = (float)HP / maxHP;

            if (isDead == false)
            {
                if (HP <= 0)
                {
                    isDead = true;

                    scoreText.UpdateScore(maxHP);

                    Destroy(GetComponent<Rigidbody>());
                    GetComponent<CapsuleCollider>().enabled = false;
                    anim.SetTrigger("Death");
                    Destroy(gameObject, 1.0f);
                }
                //Destroy(other.transform.parent.gameObject); // 만난 건 capsule 오브젝트.
            }
        }
    }
}

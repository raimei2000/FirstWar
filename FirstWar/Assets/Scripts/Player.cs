using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Animator ani;
    public GameObject bulletPrefab;
    public float speed;
    Vector3 StartPos;
    Vector3 EndPos;
    public float bullet_speed;

    // bullet count
    public int bullet_count = 0;

    float[] PosX01 = { 0.0f };
    float[] PosX02 = { -0.15f, 0.15f };
    float[] PosX03 = { -0.15f, 0.0f, 0.15f };
    float[] PosX04 = { -0.3f, -0.15f, 0.15f, 0.3f };
    float[] PosX05 = { -0.3f, -0.15f, 0.0f, 0.15f, 0.3f };

    public ParticleSystem LevelUp_Particle;

    //public SoundManager audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        StartCoroutine(Bullet_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                EndPos = Input.mousePosition;
                Vector3 Distance = EndPos - StartPos;
                //Debug.Log("StartPos : " + StartPos + ", EndPos : " + EndPos + ", dir : " + Distance.x);
                //sign of Distance.x (1 or -1)
                int value = (int)Mathf.Sign(Distance.x);

                if (Vector3.Distance(StartPos, EndPos) > 0.5f)
                {
                    if (value == 1)
                    {
                        //Debug.Log("Right Swipe");
                        //transform.Translate(Vector3.right * speed * Time.deltaTime);
                        StartPos = new Vector3(EndPos.x - 1.0f, StartPos.y, StartPos.z);
                        rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

                        AnimatorChange("RUN");
                    }
                    else if (value == -1)
                    {
                        //Debug.Log("Left Swipe");
                        //transform.Translate(Vector3.left * speed * Time.deltaTime);
                        StartPos = new Vector3(EndPos.x + 1.0f, StartPos.y, StartPos.z);
                        rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
                        AnimatorChange("RUN");
                    }
                    else
                    {
                        Debug.Log("No Swipe");
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                StartPos = Vector3.zero;
                EndPos = Vector3.zero;
                rb.velocity = Vector3.zero;
                AnimatorChange("IDLE");
            }
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    Bullet_Make();
            //    AnimatorChange("SHOOT");
            //}
        }
    }

    private IEnumerator Bullet_Coroutine()
    {
        if (GameManager.Instance.isGameOver == false)
        {
            Bullet_Make();
            yield return new WaitForSeconds(bullet_speed);

            StartCoroutine(Bullet_Coroutine());
        }
        else
        {
            StopCoroutine(Bullet_Coroutine());
        }
    }

    private void AnimatorChange(string temp)
    {
        if (temp == "SHOOT")
        {
            ani.SetTrigger("SHOOT");
            return;
        }

        ani.SetBool("RUN", false);
        ani.SetBool("IDLE", false);

        ani.SetBool(temp, true);
    }

    private void Bullet_Make()
    {
        AnimatorChange("SHOOT");

        //audio.AudioStart(1);
        SoundManager.instance.AudioStart(1);

        for (int i = 0; i < PosX(bullet_count).Length; i++)
        {
            GameObject go = Instantiate(bulletPrefab, new Vector3(transform.position.x + PosX(bullet_count)[i], transform.position.y + 0.5f, transform.position.z + 1.0f), Quaternion.identity);
            Destroy(go, 4.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isGameOver == false)
        {
            if (other.CompareTag("ATK_Speed"))
            {
                LevelUp(other.gameObject);

                bullet_speed -= 0.1f;

                if (bullet_speed <= 0.1f)
                {
                    bullet_speed = 0.1f;
                }
                Debug.Log("공격속도: " + bullet_speed);
            }
            else if (other.CompareTag("ATK_Count"))
            {
                LevelUp(other.gameObject);

                bullet_count++;
                if (bullet_count >= 4)
                {
                    bullet_count = 4;
                }
            }
            else if (other.CompareTag("ATK_Up"))
            {
                LevelUp(other.gameObject);

                GameManager.Instance.playerAttack++;
                //Debug.Log("공격력 상승. 현재 공격력: " + GameManager.Instance.playerAttack.ToString());
            }
            else if (other.CompareTag("SPD_Up"))
            {
                LevelUp(other.gameObject);

                speed += 0.2f;
                if (speed >= 7.0f)
                {
                    speed = 7.0f;
                }
                Debug.Log("현재 스피드: " + speed);
            }
        }
    }

    private float[] PosX(int count)
    {
        switch (count)
        {
            case 0:
                return PosX01;
            case 1:
                return PosX02;
            case 2:
                return PosX03;
            case 3:
                return PosX04;
            case 4:
                return PosX05;
            default:
                return null;
        }
    }

    private void LevelUp(GameObject obj)
    {
        SoundManager.instance.AudioStart(2);
        Destroy(obj);
        LevelUp_Particle.Play();
    }

}

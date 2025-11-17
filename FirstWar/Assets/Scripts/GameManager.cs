using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float elapsedTime = 0f;         // 총 경과 시간
    private int difficultyLevel = 1;        // 현재 난이도 레벨
    public float timePerLevel;              // 몬스터 레벨업에 걸리는 시간

    [Header("Monster Stats")]
    public int baseMonsterHealth = 2;       // 몬스터 기본 체력 (레벨 1)
    public float healthIncreasePerLevel;    // 레벨당 증가하는 체력

    public int playerAttack = 1;
    public bool isGameOver = false;

    void Awake()
    {
        // 씬에 GameManager가 이미 있다면 자신을 파괴, 없다면 자신을 Instance로 지정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timePerLevel)
        {
            difficultyLevel += 1;
            elapsedTime -= timePerLevel;
        }
    }

    public int GetCurrentMonsterHealth()
    {
        int hp = baseMonsterHealth + Mathf.FloorToInt(Mathf.Pow(healthIncreasePerLevel, difficultyLevel));
        return hp;
    }

    public void GameOver()
    {
        isGameOver= true;
    }
}

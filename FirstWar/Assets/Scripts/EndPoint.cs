using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject gameOverPanel;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Monster"))
        {
            GameManager.Instance.GameOver();
            gameOverPanel.SetActive(true);
            Destroy(collider.gameObject);
        }
    }
}

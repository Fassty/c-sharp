using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUpScript : MonoBehaviour
{
    // Při kolizi hráče s objektem zvýší počet životů o 1, pokud už hráč nepřesáhl limit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (FindObjectOfType<GameMaster>().lives < FindObjectOfType<GameMaster>().maxLives)
            {
                FindObjectOfType<GameMaster>().lives++;
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

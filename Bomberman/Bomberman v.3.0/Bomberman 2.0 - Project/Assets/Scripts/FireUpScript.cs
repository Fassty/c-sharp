using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUpScript : MonoBehaviour {

    // Při kolizi hráče s objektem zvýší sílu exploze o 1, pokud už hráč nepřesáhl limit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (FindObjectOfType<Player>().bombPower < FindObjectOfType<Player>().maxPower)
            {
                FindObjectOfType<Player>().bombPower++;
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

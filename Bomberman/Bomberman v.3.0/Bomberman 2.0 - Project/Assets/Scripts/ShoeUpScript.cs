using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeUpScript : MonoBehaviour {

    // Při kolizi hráče s objektem zvýší rychlost o 1, pokud už hráč nepřesáhl limit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (FindObjectOfType<Player>().velocity < FindObjectOfType<Player>().maxVelocity)
            {
                FindObjectOfType<Player>().velocity++;
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

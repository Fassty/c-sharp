using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombUpScript : MonoBehaviour {

    // Při kolizi hráče s objektem zvýší počet bomb, které hráč může položit o 1, pokud už hráč nepřesáhl limit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (FindObjectOfType<Player>().bombs < FindObjectOfType<Player>().maxBombs)
            {
                FindObjectOfType<Player>().bombs++;
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

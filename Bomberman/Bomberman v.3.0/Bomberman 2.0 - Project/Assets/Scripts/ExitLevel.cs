using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour {

    // Pokud jsou dveře otevřené, umožní hráči postup do další úrovně
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(FindObjectOfType<GameMaster>().nextLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}

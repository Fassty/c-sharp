using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosionScript : MonoBehaviour {

    public GameObject enemyPrefab;

    // Při kolizi nepřítele a exploze vytvoří instanci mrtvého nepřítele, objekt nepřítele zničí a hráči přičte skóre
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Instantiate(enemyPrefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            FindObjectOfType<Score>().score += 100;
        }
    }

    // Update is called once per frame
    // Po 1 vteřině zničí instanci objektu Explosion
    void Update () {
        Destroy(gameObject, 0.6f);
	}

}

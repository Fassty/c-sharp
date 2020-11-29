using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float countdown = 2f;

	// Update is called once per frame
    // Po vytvoření instance bomba počká 2 vteřiny, zavolá funkci třídy MapDestroyer Explode() a zničí objekt
    // Zároveň uvolní pole na kterém byla bomba, aby na něj opět bylo možné bombu položit a sníží počet položených bomb
	void Update () {
        countdown -= Time.deltaTime;

        if (countdown <= 0f) 
        {
            FindObjectOfType<MapDestroyer>().Explode(transform.position);
            Destroy(gameObject);
            if (FindObjectOfType<BombSpawnerScript>().occupied.Count != 0)
            {
                FindObjectOfType<BombSpawnerScript>().occupied.RemoveAt(0);
            }
            FindObjectOfType<BombSpawnerScript>().bombsDown--;
        }
	}
}

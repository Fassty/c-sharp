using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PowerUpSpawner : MonoBehaviour {

    public Tilemap tilemap;
    public GameObject heartUp;
    public GameObject bombUp;
    public GameObject fireUp;
    public GameObject shoeUp;

    // Rozhodne jaký PowerUp bude vytvořen
    public void Spawn(Vector3 cellPos)
    {
        int rand = Random.Range(0, 100);
        if (rand >=0 && rand < 25) 
        {
            Instantiate(heartUp, cellPos, Quaternion.identity);
        }
        else if (rand >= 25 && rand < 50)
        {
            Instantiate(bombUp, cellPos, Quaternion.identity);
        }
        else if (rand >= 50 && rand < 75)
        {
            Instantiate(fireUp, cellPos, Quaternion.identity);
        }
        else
        {
            Instantiate(shoeUp, cellPos, Quaternion.identity);
        }
    }
}

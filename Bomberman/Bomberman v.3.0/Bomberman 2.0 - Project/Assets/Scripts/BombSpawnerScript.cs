using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawnerScript : MonoBehaviour {

    public Tilemap tilemap;
    public GameObject bombPrefab; 
    public int bombsDown; 
    public List<Vector3Int> occupied = new List<Vector3Int>(); // List polí, na kterých je položená bomba. Na konci je vždy naposledy položená

    public void Start()
    {
        bombsDown = 0;
    }

    // Update is called once per frame
    // Po stisknutí mezerníku ověří, jestli hráč může položit bombu a jestli na poli kam jí pokládá už bomba není
    // Pokud jsou podmínky splněny, vytvoří instanci bomby
    void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bombsDown < FindObjectOfType<Player>().bombs)
            {
                Vector3 worldPos = FindObjectOfType<Player>().transform.position;
                Vector3Int cell = tilemap.WorldToCell(worldPos);
                Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

                if (!occupied.Contains(cell))
                {
                    occupied.Add(cell);
                    Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
                    bombsDown++;
                }
            }
        }
	}
}

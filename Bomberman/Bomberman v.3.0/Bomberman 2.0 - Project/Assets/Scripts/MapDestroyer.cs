using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Třída pro ničení objektů při explozi
public class MapDestroyer : MonoBehaviour {

    public Tilemap tilemap; // Mapa
    public List<Tile> wallTiles; // Seznam objektů, které program bere jako zeď
    public List<Tile> destructibleTiles; // Seznam ničitelných objektů

    public GameObject explosionPrefab; // Objekt s animací exploze

    // Podle síly expolze vytváří na polích animace exploze
    // Pokud narazí na zeď, ve směru už nepokračuje
    // Pokud narazí na zničitelný objekt, zničí ho, vytvoři animaci exploze a ve směru už nepokračuje
    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        bool up = true;
        bool down = true;
        bool left = true;
        bool right = true;

        if(!ExplodeCell(originCell))
        {
            up = false;
            down = false;
            left = false;
            right = false;
        }

        for (int i = 1; i <= FindObjectOfType<Player>().bombPower; i++)
        {
            if (right == true)
            {
                if (!ExplodeCell(originCell + new Vector3Int(i, 0, 0)))
                {
                    right = false;
                }
            }
            if (down == true)
            {
                if (!ExplodeCell(originCell + new Vector3Int(0, i, 0)))
                {
                    down = false;
                }
            }
            if (up == true)
            {
                if (!ExplodeCell(originCell + new Vector3Int(0, -i, 0)))
                {
                    up = false;
                }
            }
            if (left == true)
            {
                if (!ExplodeCell(originCell + new Vector3Int(-i, 0, 0)))
                {
                    left = false;
                }
            }
        }
    }

    // Vrací true, pokud nenarazil na zeď nebo zničitelný objekt
    // Na zadaném poli se pokusí vytvořit animaci exploze a případně pole zničit
    // Pokud je hráč v době exploze na tomto poli, ubere mu život a zavolá funkci třídy GameMaster Die()
    bool ExplodeCell (Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        Vector3Int playerCell = tilemap.WorldToCell(playerPos);
        Vector3 playerCenterPos = tilemap.GetCellCenterWorld(playerCell);

        if(playerCenterPos==cellCenterPos)
        {
            FindObjectOfType<GameMaster>().lives--;
            FindObjectOfType<GameMaster>().Die();
        }

        if (wallTiles.Contains(tile))
        {
            return false;
        }

        // Zničí objekt a s určitou pravděpodobností vytvoří náhodný PowerUp
        if (destructibleTiles.Contains(tile))
        {
            tilemap.SetTile(cell, null);
            FindObjectOfType<Score>().score += 25;

            int rand = Random.Range(0, 100);
            if (rand >= 50)
            {
                FindObjectOfType<PowerUpSpawner>().Spawn(cellCenterPos);
            }
            Vector3 posi = tilemap.GetCellCenterWorld(cell);
            Instantiate(explosionPrefab, posi, Quaternion.identity);
            return false;
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);
        return true;
    }

}

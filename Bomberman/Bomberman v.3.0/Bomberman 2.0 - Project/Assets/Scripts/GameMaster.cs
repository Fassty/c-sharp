using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Hlavní řídící třída programu
public class GameMaster : MonoBehaviour {
   
    public Tilemap tilemap; // Mapa pro určování polohy podle políček
    public Tilemap exitTile; // Mapa obsahující východ
    public TileBase openDoor; // Pole s otevřenými dveřmi
    public GameObject exit; // Pozice východu
    public int lives;
    public GameObject deathPrefab; // Objekt s animací smrti
    public bool nextLevel;

    public int maxLives = 9;

    void Awake () {
        SpawnPlayer();
        lives = 3;
        nextLevel = false;
	}

    // Ověřuje, jestli ještě žije nějaký nepřítel
    // Pokud ne, otevře dveře pro průchod do další úrovně
    private void Update()
    {
        if (!GameObject.FindWithTag("Enemy"))
        {
            if(!nextLevel)
            {

                exitTile.SetTile(exitTile.WorldToCell(exit.transform.position), openDoor);
                nextLevel = true;
            }
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;

    // Vytvoří instanci objektu Player
    public void SpawnPlayer()
    {
        Vector3Int cell = tilemap.WorldToCell(spawnPoint.position);
        Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);
        Instantiate(playerPrefab, cellCenter, spawnPoint.rotation);
    }

    // Pokud neexistuje instance objektu Player, vytvoří ji
    // Pokud už hráči nezbývají žádné životy, zobrazí nápis GameOver a nabídku pro návrat do menu nebo ukončení hry
    public void RespawnPlayer ()
    {
        if (lives > 0)
        {
            if (!GameObject.Find("Player(Clone)"))
            {
                Vector3Int cell = tilemap.WorldToCell(spawnPoint.position);
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);
                Instantiate(playerPrefab, cellCenter, spawnPoint.rotation);
            }
        }
        else
        {
            PauseScript.isDead = true;
            FindObjectOfType<PauseScript>().endMenu.SetActive(true);
        }
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
    }

    // Zabije hráče, vytvoří animaci smrti a po 2 vteřinách zavolá funkci RespawnPlayer()
    public void Die()
    {
        StartCoroutine(ExecuteAfterTime(2));
        Vector3 playerPos = FindObjectOfType<Player>().transform.position;
        Quaternion rotation = FindObjectOfType<Player>().transform.rotation;
        Instantiate(deathPrefab, playerPos, rotation);
    }

    public IEnumerator ExecuteAfterTime(float time)
    {
        KillPlayer(FindObjectOfType<Player>());
        yield return new WaitForSeconds(time);
        FindObjectOfType<GameMaster>().RespawnPlayer();
    }
}

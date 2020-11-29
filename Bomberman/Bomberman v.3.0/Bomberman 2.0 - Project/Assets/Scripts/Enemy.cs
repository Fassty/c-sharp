using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour {

    Vector2Int direction;
    Vector3 enemyPos;
    Vector3 cellCenterPos;
    Vector3 relativePos; // Proměnná pro prohledávání polí podle pozice jejich středu
    Tile relativeTile; // Proměnná pro ověření typu pole (zeď/ničitelný/průchozí)
    Vector3Int enemyCell; // Pole na kterém se nachází nepřítel
    Vector3Int relativeCell; // Proměnná pro prohledávání polí(získaná z proměnné relativePos)
    int velocity;
    int dir;
    float delta;

    bool wallRight;
    bool canMove;
    bool shouldMove;

    // Inicializace, všem nepřátelům nastaví směr pohybu nahoru
    // dir -> směr pohybu: 0-nahoru, 1-doleva, 2-dolů, 3-doprava
    void Start()
    {
        velocity = 2;
        dir = 0;
        direction = new Vector2Int(0, velocity);
        delta = 0.01f;
        shouldMove = false;
    }

    // Zabije hráče při kolizi
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameMaster>().Die();
            FindObjectOfType<GameMaster>().lives--;
        }
    }

    // Update is called once per frame
    // Algoritmus pohybu nepřátel
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x,direction.y);
        enemyCell = FindObjectOfType<MapDestroyer>().tilemap.WorldToCell(transform.position);
        cellCenterPos = FindObjectOfType<MapDestroyer>().tilemap.GetCellCenterWorld(enemyCell);

        if (Mathf.Abs(cellCenterPos.x - transform.position.x) <= delta && Mathf.Abs(cellCenterPos.y - transform.position.y) <= delta) 
        {
            if (shouldMove == true)
            {
                shouldMove = false;
                return;
            }
            else
            {
                wallRight = CheckRight();
                canMove = !CheckFront();

                if (wallRight && canMove)
                {
                    return;
                }
                else if (wallRight && !canMove)
                {
                    RotateLeft();
                }
                else if (!wallRight)
                {
                    RotateRight();
                    shouldMove = true;
                }
            }
        }

    }

    // Vrátí true pokud je pole v argumentu zeď 
    bool CheckTile(Tile tile)
    {
        if (FindObjectOfType<MapDestroyer>().wallTiles.Contains(relativeTile) || FindObjectOfType<MapDestroyer>().destructibleTiles.Contains(relativeTile))
        {
            return true;
        }
        else return false;
    }

    // Vrací true pokud je zeď napravo od směru pohybu nepřítele
    bool CheckRight()
    {
        if (dir == 0)
        {
            // Check right
            relativeCell.Set(enemyCell.x + 1, enemyCell.y, enemyCell.z);
            relativeTile = FindObjectOfType<MapDestroyer>().tilemap.GetTile<Tile>(relativeCell);
            if (CheckTile(relativeTile))
            {
                return true;
            }
            else return false;
        }
        else if (dir == 1)
        {
            // Check up
            relativeCell.Set(enemyCell.x, enemyCell.y + 1, enemyCell.z);
            relativeTile = FindObjectOfType<MapDestroyer>().tilemap.GetTile<Tile>(relativeCell);
            if (CheckTile(relativeTile))
            {
                return true;
            }
            else return false;
        }
        else if (dir == 2)
        {
            // Check left
            relativeCell.Set(enemyCell.x - 1, enemyCell.y, enemyCell.z);
            relativeTile = FindObjectOfType<MapDestroyer>().tilemap.GetTile<Tile>(relativeCell);
            if (CheckTile(relativeTile))
            {
                return true;
            }
            else return false;
        }
        else 
        {
            // Check down
            relativeCell.Set(enemyCell.x, enemyCell.y - 1, enemyCell.z);
            relativeTile = FindObjectOfType<MapDestroyer>().tilemap.GetTile<Tile>(relativeCell);
            if (CheckTile(relativeTile))
            {
                return true;
            }
            else return false;
        }
    }

    // Vrací true pokud je před nepřítelem zeď
    bool CheckFront()
    {
        if (dir == 0)
        {
            // Check up
            relativeCell.Set(enemyCell.x, enemyCell.y + 1, enemyCell.z);
            relativeTile = FindObjectOfType<MapDestroyer>().tilemap.GetTile<Tile>(relativeCell);
            if (CheckTile(relativeTile))
            {
                return true;
            }
            else return false;
        }
        else if (dir == 1)
        {
            // Check left
            relativeCell.Set(enemyCell.x - 1, enemyCell.y, enemyCell.z);
            relativeTile = FindObjectOfType<MapDestroyer>().tilemap.GetTile<Tile>(relativeCell);
            if (CheckTile(relativeTile))
            {
                return true;
            }
            else return false;
        }
        else if (dir == 2)
        {
            // Check down
            relativeCell.Set(enemyCell.x, enemyCell.y - 1, enemyCell.z);
            relativeTile = FindObjectOfType<MapDestroyer>().tilemap.GetTile<Tile>(relativeCell);
            if (CheckTile(relativeTile))
            {
                return true;
            }
            else return false;
        }
        else
        {
            // Check right
            relativeCell.Set(enemyCell.x + 1, enemyCell.y, enemyCell.z);
            relativeTile = FindObjectOfType<MapDestroyer>().tilemap.GetTile<Tile>(relativeCell);
            if (CheckTile(relativeTile))
            {
                return true;
            }
            else return false;
        }
    }

    void RotateLeft()
    {
        int oldX = direction.x;
        direction.x = -direction.y;
        direction.y = oldX;
        dir = (dir + 1) % 4;
    }

    void RotateRight()
    {
        int oldX = direction.x;
        direction.x = direction.y;
        direction.y = -oldX;
        dir = ((dir - 1) + 4) % 4;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Nastavení atributů hráče
public class Player : MonoBehaviour {

    public int maxBombs = 5;
    public int maxVelocity = 5;
    public int maxPower = 6;

    public int bombs;
    public int velocity;
    public int bombPower;

    // Use this for initialization
    void Start()
    {
        bombs = 1;
        velocity = 2;
        bombPower = 1;
    }
}

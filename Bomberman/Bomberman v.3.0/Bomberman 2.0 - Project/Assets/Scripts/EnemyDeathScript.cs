using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathScript : MonoBehaviour {
	
	// Update is called once per frame
    // Po 2 vteřinách zničí animaci mrtvého nepřítele
	void Update () {
        Destroy(gameObject, 2f);
	}
}

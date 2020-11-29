using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDestroyScript : MonoBehaviour {

	// Update is called once per frame
    // Po 2 vteřinách zničí instanci objektu Death
	void Update () {
        Destroy(gameObject, 2f);
    }
}

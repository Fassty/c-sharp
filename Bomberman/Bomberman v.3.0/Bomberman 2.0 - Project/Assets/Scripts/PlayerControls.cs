using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Ovládání hráče
public class PlayerControls : MonoBehaviour {

    int velocity;

	// Update is called once per frame
	void Update () {
        velocity = FindObjectOfType<Player>().velocity;
        if (Input.GetKey("w"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity);
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey("s"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocity);
            this.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-velocity, 0);
            this.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
            this.transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour {

    public TextMeshProUGUI endScore;
    int score;

	// Use this for initialization
    // Na konci hry zničí pole se skórem a menu pausy a zárověn zapíše konečné skóre
	void Start () {
        score = FindObjectOfType<Score>().score;
        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        endScore.text = score.ToString();
	}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Zobrazuje na Canvas celkové skóre
public class Score : MonoBehaviour {

    public Text scoreText;
    public int score;

    private void Start()
    {
        score = 0;
    }

    public void Update()
    {
        scoreText.text = score.ToString();
    }
}

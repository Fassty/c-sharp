using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour {

    public Text livesText;

    // Zobrazuje na Canvas počet životů
    private void Update()
    {
        livesText.text = "x " + FindObjectOfType<GameMaster>().lives.ToString();
    }
}

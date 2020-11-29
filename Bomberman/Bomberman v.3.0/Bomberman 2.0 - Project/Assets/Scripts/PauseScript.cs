using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Třída pro zobrazování UI při hře - menu pausy a menu GameOver
public class PauseScript : MonoBehaviour {

    public static bool isPaused = false;
    public static bool isDead = false;
    public GameObject pauseMenuUI;
    public GameObject endMenu;

    public static PauseScript pauseScript;

    // Zajistí, aby menu nebylo zničeno při změně scén a bylo zachováno skóre
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (pauseScript == null)
        {
            pauseScript = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else Pause();
        }
	}

    // Zastaví čas a zobrazí menu pausy
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Spustí čas a skryje menu pausy
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Načte první scénu(hlavní menu)
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        if(isDead)
        {
            endMenu.SetActive(false);
        }
        Destroy(GameObject.FindWithTag("Canvas"));
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gui_Level_Manager : MonoBehaviour
{
    public Canvas Main_HUD_Canvas;
    public Canvas PauseCanvas;
    public Canvas GameOverCanvas;

    public GameObject Player;

    private void Awake()
    {
        PauseCanvas.enabled = false;
        GameOverCanvas.enabled = false;
        Main_HUD_Canvas.enabled = true;
    }

    public void OnClickPause()
    {
        Time.timeScale = 0;
        PauseCanvas.enabled = true;
        Main_HUD_Canvas.enabled = false;
    }

    public void OnClickResume()
    {
        Time.timeScale = 1;
        PauseCanvas.enabled = false;
        Main_HUD_Canvas.enabled = true;

    }

    public void OnRestart()
    {
        PlatformManager.Instance.Restart(0);
        Time.timeScale = 1;
        PauseCanvas.enabled = false;
        Main_HUD_Canvas.enabled = true;
    }

    public void OnClickHomeButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main_Menu");

    }
    //----------------GameOver Managment---------------------------//
    public void CheckGameOver()
    {
        Health health = Player.GetComponent<Health>();
        if (health.NumberOfLives == 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        GameOverCanvas.enabled = true;
        Main_HUD_Canvas.enabled = false;
    }
}
*/
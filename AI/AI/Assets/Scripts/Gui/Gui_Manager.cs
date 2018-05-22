using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CanvasType
{
    Main,
    LevelSelection,
    Ranking,
    Shop,
    Settings,
    Pause,
    GameOver,
    Main_Hud,
}
public class Gui_Manager : MonoBehaviour
{
    public Canvas[] Canvases;

    CanvasType current;

    private void Awake()
    {
        //this gameobject will be persistent in all your scene   ---- is this needed???
        //DontDestroyOnLoad(gameObject);
        current = CanvasType.Main;
    }
    private void Update()
    {
        CurrentActivatedCanvas(current);
        //Disactive all canvas apart the current
        for (int i = 0; i < Canvases.Length; i++)
        {
            if (i != (int)current)
                Canvases[i].gameObject.SetActive(false);
        }
    }
    //set active current canvas
    public void CurrentActivatedCanvas(CanvasType canvasToActive)
    {
        Canvases[(int)canvasToActive].gameObject.SetActive(true);
    }
    public void HomeButton()
    {
        Ultimate_Audio_Management.Instance.PlaySound("Button");
        current = CanvasType.Main;
    }
    public void LevelSelection()
    {
        Ultimate_Audio_Management.Instance.PlaySound("Button");
        current = CanvasType.LevelSelection;
    }
    public void Ranking()
    {
        Ultimate_Audio_Management.Instance.PlaySound("Button");
        current = CanvasType.Ranking;
    }
    public void Shop()
    {
        Ultimate_Audio_Management.Instance.PlaySound("Button");
        current = CanvasType.Shop;
    }
    public void Settings()
    {
        Ultimate_Audio_Management.Instance.PlaySound("Button");
        current = CanvasType.Settings;
    }

    public void ConfirmLevel_01()
    {
        SceneManager.LoadScene("Level01");

    }
    public void ConfirmLevel_02()
    {
        SceneManager.LoadScene(2);


    }
    public void ConfirmLevel_03()
    {
        SceneManager.LoadScene(3);


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowModelButton : MonoBehaviour
{
    private Transform objectToShow;
    //this will be our action that call our enableModel
    private Action<Transform> ClickAction;

    public void Initialize(Transform objectToShow, Action<Transform> ClickAction)
    {
        this.objectToShow = objectToShow;
        this.ClickAction = ClickAction;
        //GetComponentInChildren<Text>().text = objectToShow.gameObject.name;
        ////--------------
        var charInfo = objectToShow.GetComponent<CharacterInfo>();
        if (charInfo != null)
            GetComponent<Image>().sprite = charInfo.Icon;
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtoClick);
    }
    private void HandleButtoClick()
    {
        ClickAction(objectToShow);
    }
}

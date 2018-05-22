using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Manager : MonoBehaviour
{
    private List<Transform> models;
    private void Awake()
    {
        models = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var Model = transform.GetChild(i);
            //every model is off as default
            Model.gameObject.SetActive(false);
            models.Add(Model);
            Model.gameObject.SetActive(i == 0);
        }
        //set your first character in your list as Active
    }

    public void EnableModel(Transform modelToActivate)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var transformToActivate = transform.GetChild(i);
            bool shouldBeActive = transformToActivate == modelToActivate;

            transformToActivate.gameObject.SetActive(shouldBeActive);
        }
    }

    public List<Transform> GetModels()
    {
        return new List<Transform>(models);
    }

    //public void SetModelActive(int index)
    //{
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        if (i == index)
    //            Characters[index].gameObject.SetActive(true);
    //        else
    //            Characters[index].gameObject.SetActive(false);
    //    }
    //}
    //public int clickButtonLeft()
    //{
    //    index--;
    //    if (index < 0)
    //        index = transform.childCount;
    //    return index;
    //}
    //public void clickButtonRight()
    //{
    //    index++;
    //    if (index > transform.childCount)
    //        index = 0;
    //}

}

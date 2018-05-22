using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowModelUI : MonoBehaviour
{
    public ShowModelButton ButtonPrefab;
    private Character_Manager CManager;
    private void Start()
    {
        CManager = FindObjectOfType<Character_Manager>();
        var models = CManager.GetModels();
        foreach (var model in models)
        {
            CreateButtonForModel(model);
        }
    }
    private void CreateButtonForModel(Transform Model)
    {
        var button = Instantiate(ButtonPrefab);
        button.transform.SetParent(this.transform);
        button.transform.localScale = Vector3.one;
        button.transform.localRotation = Quaternion.identity;

        button.Initialize(Model, CManager.EnableModel);
    }
}

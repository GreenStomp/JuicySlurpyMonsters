using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GlobalEvents")]
public class GlobalEvents : ScriptableObject
{
    public void LoadSceneAtIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadSceneNamed(string name)
    {
        SceneManager.LoadScene(name);
    }
}

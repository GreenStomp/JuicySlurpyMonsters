using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Level")]
[TestOf(typeof(PlatformManager))]
public class TestPlatformManager {
    GameObject go;
    PlatformManager manager;
    [SetUp]
    public void SetupPlatformManager()
    {
        go = new GameObject();
        manager = go.AddComponent<PlatformManager>();
    }
    [TearDown]
    public void TearDownAllGo()
    {
        GameObject[] arr = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < arr.Length; i++)
        {
            GameObject.Destroy(arr[i]);
        }
    }
}

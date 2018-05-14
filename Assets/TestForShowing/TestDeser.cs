using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeser : MonoBehaviour
{
    public int age;
    public int life;
    public int play;

    public void Do()
    {
        SerializerHandler.Save(SerializerHandler.DefaultDirectory, "Do.json", JsonUtility.ToJson(this));
    }
    public void Re()
    {
        SerializerHandler.RestoreData(SerializerHandler.DefaultDirectory, "Do.json", this);
    }

}

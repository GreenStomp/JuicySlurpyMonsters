using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SerializationManager
{
    public delegate void SerializeAction();
    //here we transform every listener in json file
    public static event SerializeAction OnSerialize;
    //here we transform every listener from json to objects
    public static event SerializeAction OnDeserialize;
    public static void OnSave()
    {
        OnSerialize();
    }
    public static void OnLoad()
    {
        OnDeserialize();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class SerializerHandler
{
    public static string DefaultDirectory { get { return defaultDirectory; } }

    #region fields
    private static string defaultDirectory = Path.Combine(Application.persistentDataPath, "SerializedData");
    #endregion

    /// <summary>
    /// Save a Json file in a File in the specified Directory with the filename you have chosen for it.
    /// If Directory doesn't Exist yet, it will create it for you,than will put file in it.
    /// </summary>
    /// <param name="directory">directory where file will be.</param>
    /// <param name="filename">name of the file.</param>
    /// <param name="jsonRappresentation">json rappresentation of the object that will be in the file.</param>
    public static void Save(string directory, string filename, string jsonRappresentation)
    {
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, filename), jsonRappresentation);
    }
    /// <summary>
    /// Restore data for an object if is json rappresentation exist at that path
    /// </summary>
    /// <param name="directory">directory where file is.</param>
    /// <param name="filename">name of the file.</param>
    /// <param name="jsonRappresentation">json rappresentation of the object that will be in the file.</param>
    public static void RestoreData(string directory, string filename, object objToRestore)
    {
        if (File.Exists(Path.Combine(directory, filename)))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Path.Combine(directory, filename)), objToRestore);
        }
    }
}



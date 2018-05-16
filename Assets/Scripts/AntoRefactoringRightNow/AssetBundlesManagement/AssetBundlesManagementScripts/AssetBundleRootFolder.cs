using System.IO;
using UnityEngine;

public enum AssetBundleRootPath
{
    DataPath,
    StreamingAssetsPath,
    PersistentPath,
}
[CreateAssetMenu(fileName = "AssetBundleRootFolder")]
public class AssetBundleRootFolder : ScriptableObject
{
    //were AssetBundles folder is stored
    [SerializeField]
    private AssetBundleRootPath rootPath = AssetBundleRootPath.DataPath;

    //our assetBundles folder
    [SerializeField]
    private string assetBundleFolder = "AssetBundles";
    //our platform Folder Name(ex:Windows) we build all bundles eachtime for different platforms
    [SerializeField]
    private string platformFolderName;
    public string GetAssetBundlePath(string assetBundleName)
    {
        string directory = Path.Combine(GetRootPath(), assetBundleFolder);
        return Path.Combine(directory, Path.Combine(platformFolderName, assetBundleName));
    }
    public string GetManifestBundlePath()
    {
        //this because when we build our manifest assetbundle will be called as the platform folder
        return this.GetAssetBundlePath(platformFolderName);
    }
    private string GetRootPath()
    {
        string initialPath = string.Empty;
        if (rootPath == AssetBundleRootPath.DataPath)
            initialPath = Application.dataPath;
        else if (rootPath == AssetBundleRootPath.StreamingAssetsPath)
            initialPath = Application.streamingAssetsPath;
        else if (rootPath == AssetBundleRootPath.PersistentPath)
            initialPath = Application.persistentDataPath;
        return initialPath;
    }
}

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class AssetBundleLoader : MonoBehaviour
{
    [SerializeField]
    private AssetBundleRootFolder assetBundlesFolder;

    [SerializeField]
    private string assetBundleName;
    [SerializeField]
    private string assetToInstantiate;

    // Buttons simulation
    // ------------------
    [Header("Buttons")]

    [Header("Load")]
    [SerializeField]
    private bool loadBundleAndInstantiate;

    [Header("Unload")]
    [SerializeField]
    private bool unloadBundle;

    [SerializeField]
    private bool unloadUnusedAssets;

    [Header("    Unload Bundle Parameters")]

    [Tooltip("Set the name of the bundle to unload. It can be a dependency")]
    [SerializeField]
    private string bundleToUnload;

    [Tooltip("This boolean is used as parameter for AssetBundle.Unload(bool unloadAllLoadedObjects)")]
    [SerializeField]
    private bool unloadAll;

    [Header("Log")]
    [SerializeField]
    private bool logLoadedBundles;
    // ------------------

    private AssetBundle mainBundle;
    private AssetBundleManifest manifest;
    private List<AssetBundle> loadedBundles = new List<AssetBundle>();
    private StringBuilder stringBuilder = new StringBuilder();

    private void Update()
    {
        // Each "if" statement simulates a button

        if (this.loadBundleAndInstantiate)
        {
            this.loadBundleAndInstantiate = false;

            if (!string.IsNullOrEmpty(this.assetBundleName))
            {
                this.LoadAssetBundle();
                Debug.Log(this.GetLoadedBundlesInfo());
            }

            if (!string.IsNullOrEmpty(this.assetToInstantiate))
            {
                // The field this.mainBundle is set by the method LoadAssetBundle
                GameObject asset = this.mainBundle.LoadAsset<GameObject>(this.assetToInstantiate);

                Object.Instantiate(asset);
            }
        }

        if (this.unloadBundle)
        {
            this.unloadBundle = false;

            AssetBundle bundleToUnload = this.GetLoadedBundle(this.bundleToUnload);

            if (bundleToUnload != null)
            {
                bundleToUnload.Unload(this.unloadAll);

                // Clean-up the loaded bundles list.
                // After calling Unload an AssetBundle variable goes "null"
                this.loadedBundles.RemoveAll(x => x == null);

                Debug.Log(this.GetLoadedBundlesInfo());
            }
        }

        if (this.unloadUnusedAssets)
        {
            this.unloadUnusedAssets = false;

            Resources.UnloadUnusedAssets();
        }

        if (this.logLoadedBundles)
        {
            this.logLoadedBundles = false;

            Debug.Log(this.GetLoadedBundlesInfo());
        }
    }

    /// <summary>
    /// Loads the bundle that was set in the inspector and assigns it to the field "mainBundle"
    /// </summary>
    private void LoadAssetBundle()
    {
        // Load and store the AssetBundleManifest only the first time
        if (this.manifest == null)
        {
            // Get the full path of the AssetBundleManifest bundle
            string manifestBundlePath = this.assetBundlesFolder.GetManifestBundlePath();

            // Load the manifest bundle
            AssetBundle manifestBundle = AssetBundle.LoadFromFile(manifestBundlePath);

            // Load and store the manifest asset
            this.manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        // Get the bundle dependencies names
        string[] dependencies = this.manifest.GetAllDependencies(this.assetBundleName);

        // Load and store the bundle dependencies
        foreach (var dependency in dependencies)
        {
            // Check if the dependency was already loaded. We cannot load a bundle more than once, otherwise an error is thrown
            if (!this.IsBundleLoaded(dependency))
            {
                // Get the full path of the AssetBundle
                string dependencyPath = this.assetBundlesFolder.GetAssetBundlePath(dependency);

                // Load the bundle
                AssetBundle dependencyBundle = AssetBundle.LoadFromFile(dependencyPath);

                // Store the bundle
                this.loadedBundles.Add(dependencyBundle);
            }
        }

        // Repeat the loading process for the main bundle
        if (!this.IsBundleLoaded(this.assetBundleName))
        {
            string assetBundlePath = this.assetBundlesFolder.GetAssetBundlePath(this.assetBundleName);

            AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
            this.loadedBundles.Add(assetBundle);

            this.mainBundle = assetBundle;
        }
        else
        {
            this.mainBundle = this.GetLoadedBundle(this.assetBundleName);
        }
    }

    /// <summary>
    /// Returns if a bundle is loaded
    /// </summary>
    private bool IsBundleLoaded(string assetBundleName)
    {
        // this should be a dictionary
        return this.loadedBundles.Find(x => x != null && x.name == Path.GetFileName(assetBundleName)) != null;
    }


    /// <summary>
    /// Returns the AssetBundle instance if it was loaded. Returns null otherwise
    /// </summary>
    private AssetBundle GetLoadedBundle(string assetBundleName)
    {
        // this should be a dictionary
        return this.loadedBundles.Find(x => x != null && x.name == Path.GetFileName(assetBundleName));
    }

    /// <summary>
    /// Returns a string with all the loaded bundles
    /// </summary>
    private string GetLoadedBundlesInfo()
    {
        this.stringBuilder.Length = 0;

        this.stringBuilder.Append("LoadedBundles: [");

        for (int i = 0; i < this.loadedBundles.Count; i++)
        {
            this.stringBuilder.Append(this.loadedBundles[i] != null ? this.loadedBundles[i].name : "null");
            if (i < this.loadedBundles.Count - 1)
            {
                this.stringBuilder.Append(",");
            }
        }

        this.stringBuilder.Append("]");

        return this.stringBuilder.ToString();
    }
}

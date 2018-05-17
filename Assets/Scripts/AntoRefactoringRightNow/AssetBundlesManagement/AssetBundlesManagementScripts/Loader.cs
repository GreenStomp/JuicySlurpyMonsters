using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public AssetBundleManager mng;

    //every loading MUST be done in a start function for now! 
    //we have some wierd stuffs if is done on an Awake func
    private void Start()
    {
        //mng.LoadAssetBundle("AssetBundleName");
        //Instantiate(mng.LoadAsset<T>("AssetBundleName", "AssetName"));
    }
}

using UnityEngine;
using System.Collections.Generic;
using SOPRO.Containers;
[CreateAssetMenu(fileName = "Level", menuName = "Level/Level")]
public class Level : ScriptableObject
{
    public SOListPoolPlatformContainer platforms;

#if UNITY_EDITOR
    void OnDisable()
    {
        if (platforms == null)
            return;

        List<PoolPlatform> pools = new List<PoolPlatform>();
        for (int i = 0; i < platforms.Elements.Count; i++)
        {
            PoolPlatform p = platforms.Elements[i];
            if (p != null)
            {
                bool unique = true;
                for (int j = 0; j < pools.Count; j++)
                {
                    PoolPlatform other = pools[j];
                    if (other == p || other.Prefab.gameObject == p.Prefab.gameObject)
                        unique = false;
                }
                if (unique)
                    pools.Add(p);
            }
        }
        platforms.Elements = pools;
    }
#endif
}
using UnityEngine;
using System.Collections.Generic;
using SOPRO;
[CreateAssetMenu(fileName = "Level", menuName = "Level/Level")]
public class Level : ScriptableObject
{
    public List<SOPool> platforms;

#if UNITY_EDITOR
    void OnDisable()
    {
        if (platforms == null)
            return;

        List<SOPool> pools = new List<SOPool>();
        for (int i = 0; i < platforms.Count; i++)
        {
            SOPool p = platforms[i];
            if (p != null)
            {
                bool unique = true;
                for (int j = 0; j < pools.Count; j++)
                {
                    SOPool other = pools[j];
                    if (other == p || other.Prefab.gameObject == p.Prefab.gameObject)
                        unique = false;
                }
                if (unique)
                    pools.Add(p);
            }
        }
        platforms = pools;
    }
#endif
}
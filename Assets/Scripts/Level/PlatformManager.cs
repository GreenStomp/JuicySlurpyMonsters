using System.Collections.Generic;
using UnityEngine;
using SOPRO;
[CreateAssetMenu(fileName = "PlatManager", menuName = "Level/Platform/Manager")]
public class PlatformManager : ScriptableObject
{
    public Platform FirstPlatform { get { return firstPlatform; } }
    public Platform LastPlatform { get { return lastPlatform; } }
    public Level CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            if (currentLevel != value)
            {
                ChangeLevel(value);
            }
        }
    }
    public bool IsActive { get { return isActive; } }
    public PlaneSide DeathPlane;
    public SOEvPlatform PlatformSpawned;
    public ReferenceInt MaxActivePlatforms;

    private Queue<Tuple<Platform, SOPool>> platforms;

    private bool isActive = true;

    private Level currentLevel;
    private Platform firstPlatform;
    private Platform lastPlatform;
    private uint platformsSurpassed;

    public void UpdateActivePlatforms()
    {
        //controllo se il primo elemento della coda abbia superato la camera, in caso affermativo verrà rimosso
        while (DeathPlane.Plane.SameSide(firstPlatform.MiddleLaneEndPos, DeathPlane.Point))
        {
            platformsSurpassed++;

            //prendo l'elemento da riciclare
            Tuple<Platform, SOPool> tuple = platforms.Dequeue();

            firstPlatform = tuple.Item1.Next;

            //Riciclo l'elemento che ha superato la camera
            tuple.Item2.Recycle(tuple.Item1.gameObject);

            if (isActive)
            {
                //Prendo un nuovo platform date quelle richieste o dato normalPrefabID
                tuple.Item2 = currentLevel.platforms[UnityEngine.Random.Range(0, currentLevel.platforms.Count)];
                tuple.Item1 = tuple.Item2.DirectGet().GetComponent<Platform>();
                tuple.Item1.Reposition(lastPlatform);
                tuple.Item1.gameObject.SetActive(true);

                //inserisco la platform alla fine della coda e setto il nuovo last
                platforms.Enqueue(tuple);
                lastPlatform = tuple.Item1;

                PlatformSpawned.Raise(lastPlatform);
            }
        }
    }
    public void RestartCurrentLevel()
    {
        if (!currentLevel)
            return;

        int activePlat = MaxActivePlatforms.Value <= 0 ? 1 : MaxActivePlatforms.Value;

        if (platforms != null)
        {
            while (platforms.Count > 0)
            {
                Tuple<Platform, SOPool> tuple = platforms.Dequeue();
                tuple.Item2.Recycle(tuple.Item1.gameObject);
                tuple.Item1 = null;
                tuple.Item2 = null;
                PoolBasic<Tuple<Platform, SOPool>>.Recycle(tuple);
            }
        }
        else
        {
            platforms = new Queue<Tuple<Platform, SOPool>>(activePlat);
        }

        firstPlatform = null;
        lastPlatform = null;


        int poolsLength = currentLevel.platforms.Count;

        while (platforms.Count < activePlat)
        {
            int index = UnityEngine.Random.Range(0, poolsLength);
            Tuple<Platform, SOPool> tuple = PoolBasic<Tuple<Platform, SOPool>>.Get();
            tuple.Item2 = currentLevel.platforms[index];
            tuple.Item1 = tuple.Item2.DirectGet().GetComponent<Platform>();

            if (!firstPlatform)
            {
                tuple.Item1.Reposition(Vector3.zero, Quaternion.identity);
                firstPlatform = tuple.Item1;
            }
            else
            {
                tuple.Item1.Reposition(lastPlatform);
            }

            tuple.Item1.gameObject.SetActive(true);

            lastPlatform = tuple.Item1;

            platforms.Enqueue(tuple);
        }
    }
    public void StopLevel(bool smoothStop = true)
    {
        if (!currentLevel)
            return;
        throw new System.NotImplementedException();
    }
    public void Clear(bool clearActivePlatforms = true, bool clearPools = true)
    {
        if (!currentLevel)
            return;

        if (clearActivePlatforms)
        {
            while (platforms.Count > 0)
            {
                Tuple<Platform, SOPool> tuple = platforms.Dequeue();
                tuple.Item2.Recycle(tuple.Item1.gameObject);
                tuple.Item1 = null;
                tuple.Item2 = null;
                PoolBasic<Tuple<Platform, SOPool>>.Recycle(tuple);
            }
        }

        if (clearPools)
        {
            for (int i = 0; i < currentLevel.platforms.Count; i++)
            {
                currentLevel.platforms[i].Clear();
            }
        }
    }
    public void ChangeLevel(Level newLevel, bool smoothChange = true, bool clearPreviousLevelPools = true)
    {
        int activePlat = MaxActivePlatforms.Value <= 0 ? 1 : MaxActivePlatforms.Value;

        if (platforms != null)
        {
            if (!smoothChange)
            {
                while (platforms.Count > 0)
                {
                    Tuple<Platform, SOPool> tuple = platforms.Dequeue();
                    tuple.Item2.Recycle(tuple.Item1.gameObject);
                    tuple.Item1 = null;
                    tuple.Item2 = null;
                    PoolBasic<Tuple<Platform, SOPool>>.Recycle(tuple);
                }
                firstPlatform = null;
                lastPlatform = null;
            }
        }
        else
        {
            platforms = new Queue<Tuple<Platform, SOPool>>(activePlat);
        }


        if (clearPreviousLevelPools && currentLevel)
        {
            for (int i = 0; i < currentLevel.platforms.Count; i++)
            {
                currentLevel.platforms[i].Clear();
            }
        }

        currentLevel = newLevel;

        int poolsLength = currentLevel.platforms.Count;

        while (platforms.Count < activePlat)
        {
            int index = UnityEngine.Random.Range(0, poolsLength);
            Tuple<Platform, SOPool> tuple = PoolBasic<Tuple<Platform, SOPool>>.Get();
            tuple.Item2 = currentLevel.platforms[index];
            tuple.Item1 = tuple.Item2.DirectGet().GetComponent<Platform>();

            if (!firstPlatform)
            {
                tuple.Item1.Reposition(Vector3.zero, Quaternion.identity);
                firstPlatform = tuple.Item1;
            }
            else
            {
                tuple.Item1.Reposition(lastPlatform);
            }

            tuple.Item1.gameObject.SetActive(true);

            lastPlatform = tuple.Item1;

            platforms.Enqueue(tuple);
        }
    }
}
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
    public PlaneSide DeathPlane;
    public SOEvPlatform PlatformSpawned;
    public ReferenceInt MaxActivePlatforms;

    private Queue<Tuple<Platform, SOPool>> platforms;

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

            //Riciclo l'elemento che ha superato la camera
            tuple.Item2.Recycle(tuple.Item1.gameObject);

            //Prendo un nuovo platform date quelle richieste o dato normalPrefabID
            tuple.Item2 = currentLevel.platforms[UnityEngine.Random.Range(0, currentLevel.platforms.Count)];
            tuple.Item1 = tuple.Item2.DirectGet().GetComponent<Platform>();
            tuple.Item1.Reposition(lastPlatform);
            tuple.Item1.gameObject.SetActive(true);

            //inserisco la platform alla fine della coda e setto il nuovo last
            platforms.Enqueue(tuple);
            lastPlatform = tuple.Item1;
            firstPlatform = platforms.Peek().Item1;

            PlatformSpawned.Raise(lastPlatform);
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
    /*
    public static PlatformManager Instance { get; private set; }

    /// <summary>
    /// Current difficulty percentage clamped to 01, used to determine which platform will be created
    /// </summary>
    public float CurrentDifficulty { get { return currentDifficulty; } set { currentDifficulty = Mathf.Clamp01(value); } }
    /// <summary>
    /// Number of platforms that have been surpassed by the camera death plane
    /// </summary>
    public uint PlatformsSurpassed { get; private set; }
    /// <summary>
    /// Count of platforms scheduled next
    /// </summary>
    public int ProgrammedPlatformsCount { get { return platformsInQueue.Count; } }
    /// <summary>
    /// Gets all current platform prefab names
    /// </summary>
    public IEnumerable<string> PlatformNames { get { return normalPrefabs.Union(specialPrefabs).Select(x => x.gameObject.name); } }
    /// <summary>
    /// Debug, property temporanea. Ritorna la prima platform storata
    /// </summary>
    public Platform FirstPlatform { get { return platforms.Peek(); } }
    /// <summary>
    /// Number of platform prefabs currently used stored and used by the platformmanager
    /// </summary>
    public int NumberOfCurrentPlatformPrefabs { get { return specialPrefabs.Length + normalPrefabs.Length; } }
    /// <summary>
    /// Death plane
    /// </summary>
    public PlatDeathPlane DeathPlane { get { return deathPlane; } }
    /// <summary>
    /// Value used as preallocation value when initializing platform pools
    /// </summary>
    public int PlatformPoolPreallocation { get { return platformPoolPreallocation; } set { platformPoolPreallocation = value; } }

    private float currentDifficulty;

    [SerializeField]
    private string[] platformsPathNames;
    private int currentPlatformPathId;

    [SerializeField]
    private Transform initialPosition;
    [SerializeField]
    private PlatDeathPlane deathPlane; //piano usato per determinare quando la piattaforma corrente è stata superata e quindi è pronta per il riciclo
    [SerializeField]
    private int platformsInScene = 10;
    [SerializeField]
    private int platformPoolPreallocation = 1;
    [SerializeField]
    private Level[] levels;

    private Level currentLevel;

    private Queue<Platform> platforms;
    private Queue<Platform> platformsInQueue;
    private Platform last;

    private int normalPrefabId;
    private Platform[] normalPrefabs;
    private Platform[] specialPrefabs;

    void Awake()
    {
        //Singleton. 
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        if (platforms == null)
            Restart(0);
    }
    void Update()
    {
        //controllo se il primo elemento della coda abbia superato la camera, in caso affermativo verrà rimosso
        if (deathPlane.DeathPlane.SameSide(platforms.Peek().EndLocation.position, deathPlane.DeathSidePoint))
        {
            PlatformsSurpassed++;

            //prendo l'elemento da riciclare
            Platform toRecycle = platforms.Dequeue();

            //Riciclo l'elemento che ha superato la camera
            PoolManager.Recycle(toRecycle);

            //Prendo un nuovo platform date quelle richieste o dato normalPrefabID
            Platform toGet = PoolManager.Get(platformsInQueue.Count == 0 ? normalPrefabs[normalPrefabId] : platformsInQueue.Dequeue());

            //riposiziono la platform
            toGet.Reposition(last);

            //inserisco la platform alla fine della coda e setto il nuovo last
            platforms.Enqueue(toGet);
            last = toGet;

            //richiamo l'evento del coinManager sulla piattaforma gettata
            CoinManager.Instance.OnPlatformCreated(toGet);

            //Mi prendo un nuovo valore valido per il get della prossima piattaforma
            int length = normalPrefabs.Length;
            normalPrefabId = UnityEngine.Random.Range((int)((length - 1) * currentDifficulty), length);
        }
    }
    void OnDestroy()
    {
        if (platforms != null)
        {
            while (platforms.Count != 0)
            {
                Platform p = platforms.Dequeue();
                if (p != null)
                    PoolManager.Recycle(p);
            }
        }
        if (normalPrefabs != null)
        {
            for (int i = 0; i < normalPrefabs.Length; i++)
            {
                PoolManager.Clear(normalPrefabs[i]);
            }
        }
        if (specialPrefabs != null)
        {
            for (int i = 0; i < specialPrefabs.Length; i++)
            {
                PoolManager.Clear(specialPrefabs[i]);
            }
        }
        if (platformsInQueue != null)
            platformsInQueue.Clear();
        if (Instance == this)
            Instance = null;
    }
    /// <summary>
    /// Dato un uint array ritorna tutti gli id delle platform correntemente caricati nel platformManager
    /// </summary>
    /// <param name="listToFill">list to fill, needs to have the right length</param>
    /// <returns>false if list length is not correct</returns>
    public bool GetAllCurrentPlatformIDS(uint[] listToFill)
    {
        if (listToFill.Length != NumberOfCurrentPlatformPrefabs)
            return false;

        int last = 0;
        for (int i = 0; i < normalPrefabs.Length; i++)
        {
            listToFill[i] = normalPrefabs[i].ID;
            last = i;
        }

        for (int i = 0; i < specialPrefabs.Length; i++ , last++)
        {
            listToFill[last] = specialPrefabs[i].ID;
        }

        return true;
    }
    /// <summary>
    /// Changes the level from which the new platform are taken. The next level will be automatically selected
    /// </summary>
    /// <param name="clearPreviousLevelPools">true if previously used pools has to be cleared</param>
    public void ChangeLevel(bool clearPreviousLevelPools = false)
    {
        //Chiamo ChangeLevel con un nextId valido
        ChangeLevel(currentPlatformPathId + 1 >= platformsPathNames.Length ? 0 : currentPlatformPathId + 1, clearPreviousLevelPools);
    }
    /// <summary>
    /// Changes the level from which the new platform are taken.
    /// </summary>
    /// <param name="nextIndex">Index of the new level</param>
    /// <param name="clearPreviousLevelPools">true if previously used pools has to be cleared</param>
    public void ChangeLevel(int nextIndex, bool clearPreviousLevelPools = false)
    {
        //Se nextIndex è identico al corrente non fare niente
        if (currentPlatformPathId == nextIndex)
            return;
        currentPlatformPathId = nextIndex;

        //Se richiesto pulire i pool dei prefab usati in precedenza
        if (clearPreviousLevelPools)
        {
            for (int i = 0; i < normalPrefabs.Length; i++)
            {
                PoolManager.Clear(normalPrefabs[i]);
            }
            for (int i = 0; i < specialPrefabs.Length; i++)
            {
                PoolManager.Clear(specialPrefabs[i]);
            }
        }

        //Pulisco le piattaforme che erano state programmate nel livello precedente
        platformsInQueue.Clear();

        //Carico i nuovi prefab dal nuovo path in ordine di difficoltà, dividendoli fra platform normali e speciali
        Platform[] temp = Resources.LoadAll<Platform>(platformsPathNames[currentPlatformPathId]).OrderBy(x => x.Difficulty).ToArray();

        normalPrefabs = temp.Where(x => x.GetSpecialPlatform() == null).OrderBy(x => x.Difficulty).ToArray();
        specialPrefabs = temp.Except(normalPrefabs).OrderBy(x => x.Difficulty).ToArray();

        //Mi accerto che i pool dei nuovi prefab siano inizializzati
        int length = normalPrefabs.Length;
        for (int i = 0; i < length; i++)
        {
            PoolManager.InitializePool(normalPrefabs[i], platformPoolPreallocation);
        }
        for (int i = 0; i < specialPrefabs.Length; i++)
        {
            PoolManager.InitializePool(specialPrefabs[i], platformPoolPreallocation);
        }

        //Mi accerto che l'id usato per gettare la prossima piattaforma sia un valore valido
        normalPrefabId = UnityEngine.Random.Range((int)((length - 1) * currentDifficulty), length);
    }
    /// <summary>
    /// Restart the level generation abruptly, reinitializing the platformmanager and clearing previously used pools.
    /// </summary>
    /// <param name="index">Index of the level to restart from</param>
    /// <param name="platformsIdToQueueNext">Indexes of the platforms to put in queue</param>
    public void Restart(int index, uint[] platformsIdToQueueNext = null)
    {
        //Creo la coda. Se la coda è inizializzata riciclo gli elementi presenti.
        if (platforms != null)
        {
            while (platforms.Count != 0)
            {
                Platform toRecycle = platforms.Dequeue();

                currentLevel.Recycle(toRecycle);
            }
        }
        platforms = new Queue<Platform>(platformsInScene);

        //Pulisco tutte le coin nella scena
        CoinManager.Instance.ResetCoins();

        //Setto l'id e carico i prefab in ordine di difficoltà dividendoli fra normal e special. Se i prefab erano gia inizializzati ripulisco i pool dei prefab precedenti
        currentPlatformPathId = index;
        if (normalPrefabs != null)
        {
            for (int i = 0; i < normalPrefabs.Length; i++)
            {
                PoolManager.Clear(normalPrefabs[i]);
            }
        }
        if (specialPrefabs != null)
        {
            for (int i = 0; i < specialPrefabs.Length; i++)
            {
                PoolManager.Clear(specialPrefabs[i]);
            }
        }
        //Pulisco le piattaforme che erano state programmate nel livello precedente se presenti
        if (platformsInQueue != null)
            platformsInQueue.Clear();

        if (platformsIdToQueueNext == null)
            platformsInQueue = new Queue<Platform>();
        else
        {
            platformsInQueue = new Queue<Platform>(platformsIdToQueueNext.Length);
            for (int i = 0; i < platformsIdToQueueNext.Length; i++)
            {
                SetNextPlatform(platformsIdToQueueNext[i]);
            }
        }

        Platform[] temp = Resources.LoadAll<Platform>(platformsPathNames[currentPlatformPathId]).OrderBy(x => x.Difficulty).ToArray();

        normalPrefabs = temp.Where(x => x.GetSpecialPlatform() == null).OrderBy(x => x.Difficulty).ToArray();
        specialPrefabs = temp.Except(normalPrefabs).OrderBy(x => x.Difficulty).ToArray();

        //Mi assicuro che i pool dei prefab correnti siano inizializzati
        for (int i = 0; i < normalPrefabs.Length; i++)
        {
            PoolManager.InitializePool(normalPrefabs[i], platformPoolPreallocation);
        }
        for (int i = 0; i < specialPrefabs.Length; i++)
        {
            PoolManager.InitializePool(specialPrefabs[i], platformPoolPreallocation);
        }

        //resetto la difficoltà, la variabile last che indica l'ultimo elemento della coda e l'id currentPrefab usato per gettare la prossima piattaforma. La prima piattaforma sara sempre Id = 0
        last = null;
        normalPrefabId = 0;
        currentDifficulty = 0f;
        PlatformsSurpassed = 0;

        //creo le piattaforme di gioco in numero loadedPlatforms
        for (int i = 0; i < platformsInScene; i++)
        {
            //getto la nuova piattaforma dato l'id
            Platform getted = PoolManager.Get(platformsInQueue.Count == 0 ? normalPrefabs[normalPrefabId] : platformsInQueue.Dequeue());

            //Riposiziono la piattaforma gettata, setto il last ed incodo l'elemento. Se last è null significa che non ci sono ancora piattaforme e verrà usato l'initialPosition
            if (last == null)
            {
                getted.Reposition(initialPosition);
            }
            else
            {
                getted.Reposition(last);
            }
            last = getted;
            platforms.Enqueue(getted);

            //richiamo l'evento del coinManager sulla piattaforma gettata
            CoinManager.Instance.OnPlatformCreated(getted);

            //Mi prendo un nuovo valore valido per il get della prossima piattaforma
            normalPrefabId = UnityEngine.Random.Range(0, normalPrefabs.Length);
        }
    }
    /// <summary>
    /// Sets a special platform of the given type in the queue of the scheduled platforms
    /// </summary>
    /// <typeparam name="T">ISpecialPlatform type</typeparam>
    /// <returns>false if it was not possible to find the given type</returns>
    public bool SetNextSpecialPlatform<T>() where T : SpecialPlatform
    {
        for (int i = 0; i < specialPrefabs.Length; i++)
        {
            Platform current = specialPrefabs[i];
            if (current.Special is T)
            {
                platformsInQueue.Enqueue(current);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Sets a platform with the given name in the queue of the scheduled platforms. Use SetNextPlatform(uint platformId) instead if possible
    /// </summary>
    /// <param name="prefabName">platform prefab name</param>
    /// <returns>false if it was not possible to find a platform with the given name</returns>
    public bool SetNextPlatform(string prefabName)
    {
        for (int i = 0; i < normalPrefabs.Length; i++)
        {
            Platform current = normalPrefabs[i];
            if (current.gameObject.name == prefabName)
            {
                platformsInQueue.Enqueue(current);
                return true;
            }
        }
        for (int i = 0; i < specialPrefabs.Length; i++)
        {
            Platform current = specialPrefabs[i];
            if (current.gameObject.name == prefabName)
            {
                platformsInQueue.Enqueue(current);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Sets a platform with the given id in the queue of the scheduled platforms.
    /// </summary>
    /// <param name="platformId">Id of the requested platform</param>
    /// <returns>false if it was not possible to find a platform with the given id</returns>
    public bool SetNextPlatform(uint platformId)
    {
        for (int i = 0; i < normalPrefabs.Length; i++)
        {
            Platform current = normalPrefabs[i];
            if (current.ID == platformId)
            {
                platformsInQueue.Enqueue(current);
                return true;
            }
        }
        for (int i = 0; i < specialPrefabs.Length; i++)
        {
            Platform current = specialPrefabs[i];
            if (current.ID == platformId)
            {
                platformsInQueue.Enqueue(current);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Class used to navigate correctly through the platforms
    /// </summary>
    public class Step
    {
        /// <summary>
        /// Current platform where Step is located
        /// </summary>
        public Platform Current
        {
            get
            {
                if (current == null)
                    Reset();
                return current;
            }
        }
        /// <summary>
        /// Step Owner
        /// </summary>
        public Transform Owner { get { return owner; } }
        /// <summary>
        /// Position representing the current Center of the platform
        /// </summary>
        public Vector3 Center { get; private set; }
        /// <summary>
        /// Up orientation representing the current Up of the platform
        /// </summary>
        public Vector3 Up { get; private set; }
        /// <summary>
        /// Forward orientation representing the current Forward of the platform
        /// </summary>
        public Vector3 TangentToCenter { get; private set; }
        /// <summary>
        /// Percentage (based on current platform lenght) representing the current point on the platform
        /// </summary>
        public float Percentage { get; private set; }
        /// <summary>
        /// Total distance walked since last Reset
        /// </summary>
        public float TotalDistanceWalked { get; private set; }
        /// <summary>
        /// Total platforms surpassed since last Reset
        /// </summary>
        public uint TotalPlatformsPassed { get; private set; }
        /// <summary>
        /// Number of special platforms surpassed since last Reset
        /// </summary>
        public uint SpecialPlatformsPassed { get; private set; }
        private Platform current;
        private Transform owner;
        public Step(Transform owner)
        {
            this.owner = owner;
            Reset();
        }
        /// <summary>
        /// Calculates next step data
        /// </summary>
        /// <param name="totalMovement">total amount of movement to do</param>
        public void CalculateNextStep(float totalMovement)
        {
            if (current == null)
                Reset();

            //converto la speedScaled in percentuale rispetto la lunghezza della curva
            float movementPercentage = totalMovement * current.InverseLength;
            //calcolo la percentuale risultante dal movimento
            float newPercentage = Percentage + movementPercentage;

            //Se la percentuale supera 1f significa che ho superato la piattaforma corrente, per cui setto lo step alla fine della piattaforma e richiedo alla prossima piattaforma di calcolare lo step rimanente
            if (newPercentage > 1f)
            {
                //Richiamo evento Exit della special platform e update counters
                if (current.Special != null)
                {
                    SpecialPlatformsPassed++;
                    current.Special.OnExit(this);
                }
                TotalPlatformsPassed++;

                newPercentage -= 1f;
                current = current.Next;
                Percentage = 0f;

                float overMovement = newPercentage * current.Length;

                //Update distance counter
                TotalDistanceWalked += totalMovement - overMovement;

                //Richiamo evento Enter della special platform
                if (current.Special != null)
                    current.Special.OnEnter(this);

                CalculateNextStep(overMovement);
                return;
            }

            //Update distance counter
            TotalDistanceWalked += totalMovement;

            //calcolo la posizione finale
            Vector3 newCenter = current.CalculateBezierCurve(newPercentage);

            //calcolo la tangente dato il Center appena calcolato e il Center dello step precedente
            Vector3 newTangent = (newCenter - Center).normalized;

            float prevPercentage = Percentage;

            //Setto i valori
            Center = newCenter;
            TangentToCenter = newTangent;
            Percentage = newPercentage;
            Up = Vector3.Lerp(current.StartLocation.up, current.EndLocation.up, Percentage);

            //Richiamo evento StepTaken della special platform
            if (current.Special != null)
                current.Special.OnStepTaken(this, prevPercentage);
        }
        /// <summary>
        /// Resets values of Step
        /// </summary>
        public void Reset()
        {
            current = PlatformManager.Instance.platforms.Peek();
            Center = current.StartLocation.position;
            TangentToCenter = current.StartLocation.forward;
            Up = current.StartLocation.up;
            Percentage = 0f;
            TotalDistanceWalked = 0f;
            TotalPlatformsPassed = 0;
            SpecialPlatformsPassed = 0;
        }
    }*/
}

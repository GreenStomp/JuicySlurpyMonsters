using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using System.Linq;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayer : MonoBehaviour
{
    #region WithMatteoScript
    //private const float distanceOffset = 35f;
    //public PlatformManager PlatformManager;
    //delegate void StateHuman();
    //private StateHuman currentState;
    //private Transform NextPos;
    //private Platform FirstPlatform;
    //public float Distance;
    //private int index;
    #endregion

    #region Comment
    //public Text TextCoin;
    //public Text TextPowerUp;
    //private int coin;
    //public int Coin
    //{
    //    get
    //    {
    //        return coin;
    //    }
    //}
    //private int powerUp;
    //public int PowerUp { get { return powerUp; } }
    //private int indexer = 0;
    //public int Indexer
    //{
    //    get
    //    {
    //        return indexer;
    //    }
    //}
    #endregion

    private NavMeshAgent navMesh;
    [HideInInspector]
    public List<GameObject> EndPointsToAdd;

    private Transform navMeshBuilder;

    public Vector3 NavMeshBuilderPosition;
    public Vector3 SizeNavMeshBuilder;


    private void Awake()
    {
        EndPointsToAdd = new List<GameObject>();
        Debug.Log("Awake");
    }

    // Use this for initialization
    void Start()
    {
        #region WithMatteoScript
        //currentState = MoveState;
        //FirstPlatform = PlatformManager.FirstPlatform;
        //NextPos = FirstPlatform.EndLocation;
        //player = FindObjectOfType<PlayerController>().transform;
        #endregion

        navMesh = GetComponent<NavMeshAgent>();
        navMesh.updateRotation = true;
        Debug.Log("Start");
        navMeshBuilder = this.transform.GetChild(0);
        navMeshBuilder.position = NavMeshBuilderPosition;
        navMeshBuilder.localScale = SizeNavMeshBuilder;
    }

    // Update is called once per frame
    void Update()
    {
        #region Comment
        //foreach (GameObject item in GameObject.FindGameObjectsWithTag("Target"))
        //{
        //    if(!Path.Contains(item))
        //    Path.Add(item);
        //}
        //Vector3 centerObj = (Starts.position + End.transform.position) / 2;
        //Vector3 direction = GroupCenterObj.transform.TransformDirection(transform.forward);
        //Ray ray = new Ray(centerObj, direction * 20f);
        //Debug.DrawRay(ray.origin,ray.direction,Color.magenta);

        //if(!first && DestinationReached())
        //navMesh.destination = Waypoints[indexer].transform.position;

        // navMesh.SetDestination(WayPoints[indexer].transform.position);

        //if (DestinationReached()) //Vector3.Distance(destination, target.position) > 1.0f)
        //{
        //    destination = target.position;
        //    agent.destination = destination;
        //if (!first)
        //navMesh.destination = WayPoints[indexer].transform.position;
        //}
        //WayPoints = WayPoints.OrderBy(WayPoints => WayPoints.transform.position.z).ToList();

        //foreach (GameObject item in GameObject.FindGameObjectsWithTag("End"))
        //{
        //    if (!WayPoints.Contains(item))
        //        WayPoints.Add(item);
        //}
        //foreach (Waypoint item in FindObjectsOfType<Waypoint>())
        //{
        //    if (!Waypoints.Contains(item))
        //        Waypoints.Add(item);
        //}

        #endregion

        #region Comment
        //if (navMesh.isOnNavMesh)
        //    navMesh.destination = WayPoints[indexer].transform.position;

        //if (DestinationReached() && indexer != WayPoints.Count - 1)
        //    indexer++;
        #endregion

        #region
        //WayPoints = FindObjectsOfType<Waypoint>().OrderBy(Waypoints => Waypoints.transform.position.z).ToList();
        //NewPosItem = FindObjectsOfType<NewPosItem>().OrderBy(Waypoints => Waypoints.transform.position.z).ToList();
        //indexer = Mathf.Clamp(indexer, 0, WayPoints.Count - 1);
        //TextCoin.text = coin.ToString();
        //TextPowerUp.text = powerUp.ToString();
        // navMesh.SetDestination(NextPos.position);
        //NextPos = OtherPlatform.Next.EndLocation;
        //ProbablyNextpos= FirstPlatform.Next.EndLocation;
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    FirstPlatform = FirstPlatform.Next;
        //    NextPos = FirstPlatform.EndLocation;
        //}
        #endregion
        //currentState();

        MoveState();
    }

    //bool DestinationReached()
    //{
    //    if (Vector3.Distance(this.transform.position, NextPos.position) <= distanceOffset)
    //        return true;
    //    else
    //        return false;
    //}
 

    //void StartState()
    //{

        //if (navMesh.isOnNavMesh)
        //{
        //    navMesh.destination = NextPos.position;

        //    //*****
        //    //1)FindObjectOfType<SpawnPoint>() => SOLO QUANDO raggiunge la destinazione e non ad ogni frame.
        //    //2)SphereCast=>
        //}

        //if (DestinationReached())
        //    currentState = ArrivedState;
    //}

    void MoveState()
    {
        if (navMesh.isOnNavMesh /*&& navMesh.hasPath*/)
            navMesh.destination = EndPointsToAdd[1].transform.position;//NextPos.position;

        #region WithMatteoScript
        //if (DestinationReached())
        //currentState = ArrivedState;
        #endregion
    }

    //public void ArrivedState()
    //{
        //FirstPlatform = FirstPlatform.Next;
        //NextPos = FirstPlatform.EndLocation;
        //index++;
        //currentState = MoveState;
    //}

    //public GameObject AddEndPoint(GameObject itemToAdd)
    //{
    //    //if(EndPointsToAdd == null)
    //    //EndPointsToAdd.Add(itemToAdd);

    //    return itemToAdd;
    //}

    //public void RemoveEndPoint(GameObject itemToRemove)
    //{
    //    if (EndPointsToAdd != null)
    //        EndPointsToAdd.Remove(itemToRemove);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<Coin>())
    //    {
    //other.gameObject.SetActive(false);
    //coin++;
    // other.gameObject.transform.position = NewPos();
    //}
    //if(other.gameObject.GetComponent<PowerUp>())
    // {
    //     other.gameObject.SetActive(false);
    //     powerUp++;
    // }

    //}

    //private Vector3 NewPos()
    //{
    //    return NewPosItem[indexer].transform.position;
    //}
}

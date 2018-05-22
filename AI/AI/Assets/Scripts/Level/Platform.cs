
using UnityEngine;
using System;
using System.Collections.Generic;
public class Platform : MonoBehaviour, IPoolable
{
    public const float DefaultLanesDistance = 6f;
    public const uint DefaultLanesNumber = 3;
    #region IPoolable
    public IPoolable Prefab { get; set; }
    public GameObject Self { get { return this.gameObject; } }
    #endregion
    /// <summary>
    /// Unique id of this type of platform
    /// </summary>
    public uint ID { get { return id; } }
    /// <summary>
    /// Next platform. Null if this is the last latform
    /// </summary>
    public Platform Next { get; private set; }
    /// <summary>
    /// Returns special features if present. If platform may not be initializzated use GetSpecialPlatform() instead
    /// </summary>
    public SpecialPlatform Special { get { return special; } }
    /// <summary>
    /// Platform difficulty value
    /// </summary>
    public uint Difficulty { get { return this.difficulty; } }
    /// <summary>
    /// Lenght of the main curve representing the platform
    /// </summary>
    public float Length { get { return curve.Length; } }
    /// <summary>
    /// 1/Lenght
    /// </summary>
    public float InverseLength { get { return curve.InverseLength; } }
    /// <summary>
    /// Valid point of the main curve
    /// </summary>
    public int CurveValidPoints { get { return curve.ValidPoints; } }
    /// <summary>
    /// Distance from one lane to another lane of the platform
    /// </summary>
    public float DistanceBetweenLanes { get { return distanceBetweenLanes; } }
    /// <summary>
    /// Number of lanes in the platform
    /// </summary>
    public uint Lanes { get { return totalLanes; } }
    /// <summary>
    /// Waypoint that indicates position and orientation of the start of the platform
    /// </summary>
    public Transform StartLocation { get { return start; } }
    /// <summary>
    /// Waypoint that indicates position and orientation of the end of the platform
    /// </summary>
    public Transform EndLocation { get { return end; } }
    [SerializeField]
    private uint id;
    [SerializeField]
    private Transform terrainAndObstacles;
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform bezierP1;
    [SerializeField]
    private Transform bezierP2;
    [SerializeField]
    private Transform end;
    [SerializeField]
    private uint difficulty;
    [SerializeField]
    private float distanceBetweenLanes = DefaultLanesDistance;
    [SerializeField]
    private uint totalLanes = DefaultLanesNumber;
    [SerializeField]
    private BezierCurve curve = new BezierCurve();

    private SpecialPlatform special;
    /// <summary>
    /// Reposition the platform at the end of the given platform and updates curve and platform data
    /// </summary>
    /// <param name="lastPlaform">given platform which end will overlap with this platform start</param>
    public void Reposition(Platform lastPlaform)
    {
        //Posiziono questa piattaforma in modo tale che il waypoint start sia posizionato ed orientato nello stesso modo dell'end della platform precedente
        Transform platform = this.transform;

        Quaternion startToMainRotation = new Quaternion();
        startToMainRotation.SetFromToRotation(start.forward, platform.forward);

        platform.rotation = lastPlaform.end.rotation * startToMainRotation;
        platform.position = lastPlaform.end.position + (platform.position - start.position);

        //updato la posizione dei punti della curva. Questa operazione non provoca altri calcoli
        curve.Set(this.start.position, this.bezierP1.position, this.bezierP2.position, this.end.position, curve.ValidPoints);

        //Setto il next della piattaforma precedente
        lastPlaform.Next = this;
    }
    /// <summary>
    /// Reposition the platform start at the given position and rotation and updates curve data
    /// </summary>
    /// <param name="location"></param>
    public void Reposition(Transform location)
    {
        //Posiziono questa piattaforma in modo tale che il waypoint start sia posizionato ed orientato nello stesso modo della transform passata
        Transform platform = this.transform;

        Quaternion startToMainRotation = new Quaternion();
        startToMainRotation.SetFromToRotation(start.forward, platform.forward);

        platform.rotation = location.rotation * startToMainRotation;
        platform.position = location.position + (platform.position - start.position);

        //updato la posizione dei punti della curva. Questa operazione non provoca altri calcoli
        curve.Set(this.start.position, this.bezierP1.position, this.bezierP2.position, this.end.position, curve.ValidPoints);
    }
    /// <summary>
    /// Calculates and sets Platform curve and supplement curves data. This method costs, best to be used on editor
    /// </summary>
    /// <param name="id">Platform unique id</param>
    /// <param name="start">start point</param>
    /// <param name="p1">first control point</param>
    /// <param name="p2">second control point</param>
    /// <param name="end">end point</param>
    /// <param name="validPoints">number of points to consider</param>
   
    public void SetPlatform(uint id, Transform start, Transform p1, Transform p2, Transform end, Transform terrain, int validPoints)
    {
        this.id = id;

        this.terrainAndObstacles = terrain;

        //Set points
        this.start = start;
        this.bezierP1 = p1;
        this.bezierP2 = p2;
        this.end = end;

        //Create and set main curve
        this.curve = new BezierCurve();
        this.curve.Set(this.start.position, this.bezierP1.position, this.bezierP2.position, this.end.position, validPoints);
        this.curve.ForceUpdateLenghts();        
    }
    /// <summary>
    /// Calculates and sets Platform curve and supplement curves data. This method costs, best to be used on editor
    /// </summary>
    /// <param name="id">Platform unique id</param>
    public void SetPlatform(uint id)
    {
        SetPlatform(id, start, bezierP1, bezierP2, end, terrainAndObstacles, CurveValidPoints);
    }
    /// <summary>
    /// Calculate point on curve given a percentage
    /// </summary>
    /// <param name="percentage">percentage to be used clamped internally to 01</param>
    /// <returns>point on the curve at the given percentage</returns>
    public Vector3 CalculateBezierCurve(float percentage)
    {
        return curve.GetPoint(Mathf.Clamp01(percentage));
    }    
    /// <summary>
    /// Method that forces the platform to check for ISpecialPlatform components
    /// </summary>
    /// <returns>special platform if present</returns>
    public SpecialPlatform GetSpecialPlatform()
    {
        if (special == null)
            special = GetComponent<SpecialPlatform>();
        return special;
    }
    void OnDisable()
    {
        Next = null;
        transform.rotation = Quaternion.identity;
    }
    void OnEnable()
    {
        if (terrainAndObstacles != null)
            terrainAndObstacles.SetActiveRecursive(true);
        if (special == null)
            special = GetComponent<SpecialPlatform>();
    }
}
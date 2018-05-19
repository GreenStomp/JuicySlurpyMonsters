using System;
using UnityEngine;
[CreateAssetMenu(fileName = "PlatformData", menuName = "Level/Platform/Data")]
public class PlatformData : ScriptableObject
{
    //public const float MinAngleForStraightCondition = 0.85f;
    //public const float MinPlaneDistancePercentage = 0.01f;

    public uint PlatformUniqueId;

    public RenderingData SidesRendering;
    public RenderingData PlatformRendering;


    public SpecialPlatform SpecialEffect
    {
        get { return specialEffect; }
        set
        {
            if (specialEffect != value)
            {
                specialEffect = value;
                //OnValidate();
            }
        }
    }
    //public PlatformType TypeFlag { get { return typeFlag; } }
    //public int DeltaLanes { get { return deltaLanes; } }
    //public float StartLanesSize { get { return startLaneDistance; } }
    //public float EndLanesSize { get { return endLaneDistance; } }
    public V3BezierCurve[] Lanes
    {
        get { return lanes; }
        set
        {
            if (lanes != value)
            {
                lanes = value;
                //OnValidate();
            }
        }
    }

    //[SerializeField]
    //private PlatformType typeFlag;
    [SerializeField]
    private SpecialPlatform specialEffect;
    [SerializeField]
    private V3BezierCurve[] lanes; //nell awake della platform mi assicuro che pos e rot siano a default, poi setto local position delle 4 transform ai valori della curva
    //Lanes should always have their start point setted so that it's rotation coincides with the world orientation
    //[SerializeField]
    //private int deltaLanes;
    //[SerializeField]
    //private float startLaneDistance;
    //[SerializeField]
    //private float endLaneDistance;
    /*
    /// <summary>
    /// Updates BezierCurves Lenght and TypeFlag by doing several checks. Automatically called in Unity editor inspector and on Lanes and SpecialEffect property sets. Should be used when an internal value in the Lanes array has been modified to make sure TypeFlag is updated
    /// </summary>
    public void OnValidate()
    {
        typeFlag = specialEffect ? PlatformType.SpecialPlatform : PlatformType.None;

        if (lanes != null && lanes.Length > 0)
        {
            Tuple<Vector3, Vector3>[] lanesInfo = new Tuple<Vector3, Vector3>[lanes.Length];

            for (int i = 0; i < lanes.Length; i++)
            {
                V3BezierCurve currLane = lanes[i];
                currLane.ForceUpdateLenghts();
                Tuple<Vector3, Vector3> current = new Tuple<Vector3, Vector3>(currLane.Start, currLane.End);
            }

            V3BezierCurve lane = lanes[lanes.Length / 2].Curve;

            Vector3 startDir = (lane.GetPoint(0.0001f) - lane.GetPoint(-0.0001f)).normalized;
            Vector3 endDir = (lane.GetPoint(1.0001f) - lane.GetPoint(0.9999f)).normalized;

            float angle = Vector3.Dot(startDir, endDir);

            //Is end direction more or less the same as the start direction?
            if (angle >= MinAngleForStraightCondition)
                typeFlag |= PlatformType.Straight;

            //Goes right/left ?

            Plane comparisonPlane = new Plane(lane.Start, lane.Start + startDir, lane.Start + Vector3.up);
            Vector3 comparisonPoint = lane.Start + Vector3.right;

            typeFlag |= CalculateCurveEndType(PlatformType.TurnRight, PlatformType.TurnLeft, comparisonPlane, comparisonPoint, lane);

            //Goes up/down ?

            comparisonPlane = new Plane(lane.Start, lane.Start + startDir, lane.Start + Vector3.right);
            comparisonPoint = lane.Start + Vector3.up;

            typeFlag |= CalculateCurveEndType(PlatformType.GoesUp, PlatformType.GoesDown, comparisonPlane, comparisonPoint, lane);

            //Goes spiralLeft/Right ?


            //Controllare se lane aumentano o diminuiscano. deltaLanes = endLanes - startLanes
            deltaLanes = 0;
            startLaneDistance = 0f;
            endLaneDistance = 0f;
            for (int i = 0; i < lanesInfo.Length; i++)
            {
                Tuple<Vector3, Vector3> first = lanesInfo[i];
                for (int j = i + 1; j < lanesInfo.Length; j++)
                {
                    Tuple<Vector3, Vector3> second = lanesInfo[j];
                    if (first.Item1.IsApproximatelyEqualTo(second.Item1))
                    {
                        deltaLanes++;
                    }
                    else
                    {
                        startLaneDistance = Vector3.Distance(first.Item1, second.Item1);
                    }
                    if (first.Item2.IsApproximatelyEqualTo(second.Item2))
                    {
                        deltaLanes--;
                    }
                    else
                    {
                        endLaneDistance = Vector3.Distance(first.Item2, second.Item2);
                    }
                }
            }
        }
    }
    */
    /*
    public static PlatformType CalculateCurveEndType(PlatformType firstCondition, PlatformType secondCondition, Plane comparisonPlane, Vector3 comparisonPointForFirstCondition, V3BezierCurve curve)
    {
        PlatformType toReturn = PlatformType.None;

        Vector3 end = curve.End;

        if (comparisonPlane.GetDistanceToPoint(end) * MinPlaneDistancePercentage >= MinPlaneDistancePercentage)
        {
            toReturn |= (comparisonPlane.SameSide(comparisonPointForFirstCondition, end) ? firstCondition : secondCondition);
        }

        return toReturn;
    }
    */
}
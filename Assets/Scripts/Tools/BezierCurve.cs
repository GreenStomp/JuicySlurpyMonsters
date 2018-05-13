using System;
using UnityEngine;
[CreateAssetMenu(fileName = "BezierCurve" , menuName = "Level/BezierCurve")]
public class BezierCurve : ScriptableObject
{
    private const float minLenghtUpdFrequency = 0.001f;
    public const int MinValidPoints = 2;
    public const int MaxValidPoints = 4;
    /// <summary>
    /// Length of the curve
    /// </summary>
    public float Length { get { return length; } }
    /// <summary>
    /// 1/Length
    /// </summary>
    public float InverseLength { get { return inverseLength; } }
    /// <summary>
    /// Number of points to consider in the curve.
    /// </summary>
    public int ValidPoints
    {
        get { return validPoints; }
        set
        {
            int v = Mathf.Clamp(value, MinValidPoints, MaxValidPoints);
            if (validPoints != v)
            {
                validPoints = v;
                this.ForceUpdateLenghts();
            };
        }
    }
    /// <summary>
    /// Start point of the curve
    /// </summary>
    public Vector3 Start { get; private set; }
    /// <summary>
    /// First control point of the curve
    /// </summary>
    public Vector3 P1 { get; private set; }
    /// <summary>
    /// Second control point of the curve
    /// </summary>
    public Vector3 P2 { get; private set; }
    /// <summary>
    /// End point of the curve
    /// </summary>
    public Vector3 End { get; private set; }
    [SerializeField]
    private float length;
    [SerializeField]
    private float inverseLength;
    [SerializeField]
    [Range(MinValidPoints, MaxValidPoints)]
    private int validPoints = MinValidPoints;
    public BezierCurve()
    {

    }
    /// <summary>
    /// Sets curve data. If the given valid points value differs from that of the curve additional operations will be performed
    /// </summary>
    /// <param name="start">start position</param>
    /// <param name="p1">first control point</param>
    /// <param name="p2">second control point</param>
    /// <param name="end">end point</param>
    /// <param name="validPoints">number of points to consider</param>
    public void Set(Vector3 start, Vector3 p1, Vector3 p2, Vector3 end, int validPoints)
    {
        this.Start = start;
        this.P1 = p1;
        this.P2 = p2;
        this.End = end;
        this.ValidPoints = validPoints;
    }
    /// <summary>
    /// Sets curve data. If the given valid points value differs from that of the curve additional operations will be performed
    /// </summary>
    /// <param name="start">start position</param>
    /// <param name="p1">first control point</param>
    /// <param name="end">end point</param>
    /// <param name="validPoints">number of points to consider</param>
    public void Set(Vector3 start, Vector3 p1, Vector3 end, int validPoints)
    {
        Set(start, p1, p1, end, validPoints);
    }
    /// <summary>
    /// Sets curve data. If the given valid points value differs from that of the curve additional operations will be performed
    /// </summary>
    /// <param name="start">start position</param>
    /// <param name="end">end point</param>
    /// <param name="validPoints">number of points to consider</param>
    public void Set(Vector3 start, Vector3 end, int validPoints)
    {
        Vector3 halfWay = (start + end) * 0.5f;
        Set(start, halfWay, halfWay, end, validPoints);
    }
    /// <summary>
    /// Copies the values of the given curve
    /// </summary>
    /// <param name="toCopy">Curve to copy</param>
    public void Copy(BezierCurve toCopy)
    {
        this.Start = toCopy.Start;
        this.P1 = toCopy.P1;
        this.P2 = toCopy.P2;
        this.End = toCopy.End;
        this.length = toCopy.length;
        this.inverseLength = toCopy.inverseLength;
        this.validPoints = Mathf.Clamp(toCopy.validPoints, MinValidPoints, MaxValidPoints);
    }
    /// <summary>
    /// Forces the update of Length and InverseLength.
    /// </summary>
    public void ForceUpdateLenghts()
    {
        this.length = this.validPoints == 2 ? Vector3.Distance(Start, End) : GetLength();
        this.inverseLength = Mathf.Approximately(0f, this.length) ? 0f : 1f / this.length;
    }
    /// <summary>
    /// Calculates a point on the curve given a percentage using the correct number of control points
    /// </summary>
    /// <param name="percentage">percentage of the curve</param>
    /// <returns>point on the curve</returns>
    public Vector3 GetPoint(float percentage)
    {
        return this.validPoints == 2 ? CalculateBezierFirst(percentage) : (this.validPoints == 3 ? CalculateQuadraticBezier(percentage) : CalculateCubicBezier(percentage));
    }
    /// <summary>
    /// Calculates the total length of the curve. Highly inefficient, Use ForceUpdateLengths and the properties Length and InverseLength instead
    /// </summary>
    /// <returns>total lenght of the curve</returns>
    public float GetLength()
    {
        float pointsFrequency = minLenghtUpdFrequency;
        float current = pointsFrequency;

        Vector3 prevPoint = Start;
        Vector3 nextPoint = Start;
        float currentLenght = 0f;

        Func<float, Vector3> calculator;
        if (this.validPoints == 2)
            calculator = CalculateBezierFirst;
        else if (this.validPoints == 3)
            calculator = CalculateQuadraticBezier;
        else
            calculator = CalculateCubicBezier;


        while (current < 1f)
        {
            nextPoint = calculator(current);
            currentLenght += Vector3.Distance(prevPoint, nextPoint);

            prevPoint = nextPoint;
            current += pointsFrequency;
        }
        nextPoint = calculator(1f);
        currentLenght += Vector3.Distance(prevPoint, nextPoint);
        return currentLenght;
    }
    /// <summary>
    /// Calculates a point on the curve given all of the control points and a percentage
    /// </summary>
    /// <param name="percentage">bezier curve percentage</param>
    /// <returns>point on the curve</returns>
    public Vector3 CalculateCubicBezier(float percentage)
    {
        float u = 1 - percentage;
        float tSquare = percentage * percentage;
        float uSquare = u * u;
        float uCubic = uSquare * u;
        float tCubic = tSquare * percentage;

        Vector3 p = uCubic * Start;
        p += 3 * uSquare * percentage * P1;
        p += 3 * u * tSquare * P2;
        p += tCubic * End;

        return p;
    }
    /// <summary>
    /// Calculates a point on the curve given the 2 end points and the first control point and a percentage
    /// </summary>
    /// <param name="percentage">bezier curve percentage</param>
    /// <returns>point on the curve</returns>
    public Vector3 CalculateQuadraticBezier(float percentage)
    {
        float u = 1 - percentage;
        float tSquare = percentage * percentage;
        float uSquare = u * u;

        Vector3 p = uSquare * Start;
        p += 2 * u * percentage * P1;
        p += tSquare * End;

        return p;
    }
    /// <summary>
    /// Calculates a point on the curve given the 2 end points and a percentage
    /// </summary>
    /// <param name="percentage">bezier curve percentage</param>
    /// <returns>point on the curve</returns>
    public Vector3 CalculateBezierFirst(float percentage)
    {
        float u;
        u = 1f - percentage;

        return u * Start
            + percentage * End;
    }
}
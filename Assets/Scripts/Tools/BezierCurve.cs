using System;
using UnityEngine;
public abstract class BezierCurve<T> : ScriptableObject
{
    protected const float minLenghtUpdFrequency = 0.001f;
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
    public abstract T Start { get; protected set; }
    /// <summary>
    /// First control point of the curve
    /// </summary>
    public abstract T P1 { get; protected set; }
    /// <summary>
    /// Second control point of the curve
    /// </summary>
    public abstract T P2 { get; protected set; }
    /// <summary>
    /// End point of the curve
    /// </summary>
    public abstract T End { get; protected set; }

    [SerializeField]
    private float length;
    [SerializeField]
    private float inverseLength;

    [SerializeField]
    [Range(MinValidPoints, MaxValidPoints)]
    private int validPoints = MinValidPoints;

    /// <summary>
    /// Sets curve data. If the given valid points value differs from that of the curve additional operations will be performed
    /// </summary>
    /// <param name="start">start</param>
    /// <param name="p1">first control point</param>
    /// <param name="p2">second control point</param>
    /// <param name="end">end</param>
    public void Set(T start, T p1, T p2, T end)
    {
        this.Start = start;
        this.P1 = p1;
        this.P2 = p2;
        this.End = end;
        this.ValidPoints = 4;
    }
    /// <summary>
    /// Sets curve data. If the given valid points value differs from that of the curve additional operations will be performed
    /// </summary>
    /// <param name="start">start</param>
    /// <param name="p1">first control point</param>
    /// <param name="p2">second control point</param>
    /// <param name="end">end</param>
    /// <param name="end">valid points to consider</param>
    public void Set(T start, T p1, T p2, T end, int validPoints)
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
    /// <param name="start">start</param>
    /// <param name="p1">first control point</param>
    /// <param name="end">end</param>
    public void Set(T start, T p1, T end)
    {
        this.Start = start;
        this.P1 = p1;
        this.P2 = default(T);
        this.End = end;
        this.ValidPoints = 3;
    }
    /// <summary>
    /// Sets curve data. If the given valid points value differs from that of the curve additional operations will be performed
    /// </summary>
    /// <param name="start">start</param>
    /// <param name="end">end</param>
    public void Set(T start, T end)
    {
        this.Start = start;
        this.P1 = default(T);
        this.P2 = default(T);
        this.End = end;
        this.ValidPoints = 2;
    }
    /// <summary>
    /// Copies the values of the given curve
    /// </summary>
    /// <param name="toCopy">Curve to copy</param>
    public void Copy(BezierCurve<T> toCopy)
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
        this.length = GetLength();
        this.inverseLength = Mathf.Approximately(0f, this.length) ? 0f : 1f / this.length;
    }
    /// <summary>
    /// Calculates a point on the curve given a percentage using the correct number of control points
    /// </summary>
    /// <param name="percentage">percentage of the curve</param>
    /// <returns>point on the curve</returns>
    public T GetPoint(float percentage)
    {
        return this.validPoints == 2 ? CalculateBezierFirst(percentage) : (this.validPoints == 3 ? CalculateQuadraticBezier(percentage) : CalculateCubicBezier(percentage));
    }
    /// <summary>
    /// Calculates the total length of the curve. Highly inefficient, Use ForceUpdateLengths and the properties Length and InverseLength instead
    /// </summary>
    /// <returns>total lenght of the curve</returns>
    public float GetLength()
    {
        if (ValidPoints == 2)
            return Distance(Start, End);

        float pointsFrequency = minLenghtUpdFrequency;
        float current = pointsFrequency;

        T prevPoint = Start;
        T nextPoint = Start;
        float currentLenght = 0f;

        Func<float, T> calculator;
        if (this.ValidPoints == 3)
            calculator = CalculateQuadraticBezier;
        else
            calculator = CalculateCubicBezier;


        while (current < 1f)
        {
            nextPoint = calculator(current);
            currentLenght += Distance(prevPoint, nextPoint);

            prevPoint = nextPoint;
            current += pointsFrequency;
        }
        nextPoint = End;
        currentLenght += Distance(prevPoint, nextPoint);
        return currentLenght;
    }

    /// <summary>
    /// Calculates a point on the curve given all of the control points and a percentage
    /// </summary>
    /// <param name="percentage">bezier curve percentage</param>
    /// <returns>point on the curve</returns>
    public abstract T CalculateCubicBezier(float percentage);
    /// <summary>
    /// Calculates a point on the curve given the 2 end points and the first control point and a percentage
    /// </summary>
    /// <param name="percentage">bezier curve percentage</param>
    /// <returns>point on the curve</returns>
    public abstract T CalculateQuadraticBezier(float percentage);
    /// <summary>
    /// Calculates a point on the curve given the 2 end points and a percentage
    /// </summary>
    /// <param name="percentage">bezier curve percentage</param>
    /// <returns>point on the curve</returns>
    public abstract T CalculateBezierFirst(float percentage);
    /// <summary>
    /// Method used to calculate the distance given 2 curve points
    /// </summary>
    /// <param name="p1">first point</param>
    /// <param name="p2">second point</param>
    /// <returns>distance between p1 and p2</returns>
    public abstract float Distance(T p1, T p2);
}
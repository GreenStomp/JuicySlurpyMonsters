using System;
using UnityEngine;
[Serializable]
public class SimpleBezierCurve
{
    public const int MinValidPoints = 2;
    public const int MaxValidPoints = 4;
    /// <summary>
    /// Number of valid points in the curve
    /// </summary>
    public int ValidPoints { get { return validPoints; } set { this.validPoints = Mathf.Clamp(value, MinValidPoints, MaxValidPoints); } }
    /// <summary>
    /// Start value of the curve
    /// </summary>
    public float Start { get { return start; } }
    /// <summary>
    /// First control point of the curve
    /// </summary>
    public float P1 { get { return p1; } }
    /// <summary>
    /// Second control point of the curve
    /// </summary>
    public float P2 { get { return p2; } }
    /// <summary>
    /// End point of the curve
    /// </summary>
    public float End { get { return end; } }
    [SerializeField]
    private float start;
    [SerializeField]
    private float p1;
    [SerializeField]
    private float p2;
    [SerializeField]
    private float end;
    [SerializeField]
    [Range(MinValidPoints, MaxValidPoints)]
    private int validPoints = MinValidPoints;
    public SimpleBezierCurve()
    {

    }
    /// <summary>
    /// Sets curve data.
    /// </summary>
    /// <param name="start">start value</param>
    /// <param name="p1">first control point</param>
    /// <param name="p2">second control point</param>
    /// <param name="end">end point</param>
    /// <param name="validPoints">number of points to consider</param>
    public void Set(float start, float p1, float p2, float end, int validPoints)
    {
        this.start = start;
        this.p1 = p1;
        this.p2 = p2;
        this.end = end;
        this.ValidPoints = validPoints;
    }
    /// <summary>
    /// Copies the given curve
    /// </summary>
    /// <param name="other">curve to copy</param>
    public void Copy(SimpleBezierCurve other)
    {
        this.start = other.start;
        this.p1 = other.p1;
        this.p2 = other.p2;
        this.end = other.end;
        this.ValidPoints = other.validPoints;
    }
    /// <summary>
    /// Calculates the value of the curve at the given percentage using the correct number of control points
    /// </summary>
    /// <param name="percentage">percentage of the curve</param>
    /// <returns>value calculated</returns>
    public float GetValue(float percentage)
    {
        return this.validPoints == 2 ? CalculateBezierFirst(percentage) : (this.validPoints == 3 ? CalculateBezierSecond(percentage) : CalculateBezierThird(percentage));
    }
    /// <summary>
    /// Calculates the value of the curve at the given percentage using all of the control points
    /// </summary>
    /// <param name="percentage">percentage of the curve</param>
    /// <returns>value calculated</returns>
    public float CalculateBezierThird(float percentage)
    {
        float u, u2, u3, t2, t3;
        u = 1f - percentage;
        u2 = u * u;
        u3 = u2 * u;
        t2 = percentage * percentage;
        t3 = t2 * percentage;

        return u3 * start
            + 3 * percentage * u2 * p1
            + 3 * t2 * u * p2
            + t3 * end;
    }
    /// <summary>
    /// Calculates the value of the curve at the given percentage using the end points and the first control point
    /// </summary>
    /// <param name="percentage">percentage of the curve</param>
    /// <returns>value calculated</returns>
    public float CalculateBezierSecond(float percentage)
    {
        float u, u2, t2;
        u = 1f - percentage;
        u2 = u * u;
        t2 = percentage * percentage;

        return u2 * start
            + 2 * percentage * u * p1
            + t2 * end;
    }
    /// <summary>
    /// Calculates the value of the curve at the given percentage using the 2 end points
    /// </summary>
    /// <param name="percentage">percentage of the curve</param>
    /// <returns>value calculated</returns>
    public float CalculateBezierFirst(float percentage)
    {
        float u;
        u = 1f - percentage;

        return u * start
            + percentage * end;
    }
}
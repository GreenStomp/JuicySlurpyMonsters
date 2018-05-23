using UnityEngine;
[CreateAssetMenu(fileName = "FloatBezierCurve", menuName = "Level/BezierCurves/Float")]
public class SimpleBezierCurve : BezierCurve<float>
{
    public override float Start { get { return start; } protected set { start = value; } }
    public override float P1 { get { return p1; } protected set { p1 = value; } }
    public override float P2 { get { return p2; } protected set { p2 = value; } }
    public override float End { get { return end; } protected set { end = value; } }

    [SerializeField]
    private float start;
    [SerializeField]
    private float p1;
    [SerializeField]
    private float p2;
    [SerializeField]
    private float end;

    /// <summary>
    /// Calculates the value of the curve at the given percentage using all of the control points
    /// </summary>
    /// <param name="percentage">percentage of the curve</param>
    /// <returns>value calculated</returns>
    public override float CalculateCubicBezier(float percentage)
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
    public override float CalculateQuadraticBezier(float percentage)
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
    public override float CalculateBezierFirst(float percentage)
    {
        float u;
        u = 1f - percentage;

        return u * start
            + percentage * end;
    }
    public override float Distance(float p1, float p2)
    {
        return p1 > p2 ? p1 - p2 : p2 - p1;
    }
}
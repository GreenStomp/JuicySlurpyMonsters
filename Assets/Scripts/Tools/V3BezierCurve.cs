using UnityEngine;
[CreateAssetMenu(fileName = "V3BezierCurve" , menuName = "Level/BezierCurves/Vector3")]
public class V3BezierCurve : BezierCurve<Vector3>
{
    public override Vector3 Start { get { return start; } protected set { start = value; } }
    public override Vector3 P1 { get { return p1; } protected set { p1 = value; } }
    public override Vector3 P2 { get { return p2; } protected set { p2 = value; } }
    public override Vector3 End { get { return end; } protected set { end = value; } }

    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 p1;
    [SerializeField]
    private Vector3 p2;
    [SerializeField]
    private Vector3 end;
    public override Vector3 CalculateBezierFirst(float percentage)
    {
        float u;
        u = 1f - percentage;

        return u * start
            + percentage * end;
    }
    public override Vector3 CalculateCubicBezier(float percentage)
    {
        float u = 1 - percentage;
        float tSquare = percentage * percentage;
        float uSquare = u * u;
        float uCubic = uSquare * u;
        float tCubic = tSquare * percentage;

        Vector3 p = uCubic * start;
        p += 3 * uSquare * percentage * p1;
        p += 3 * u * tSquare * p2;
        p += tCubic * end;

        return p;
    }
    public override Vector3 CalculateQuadraticBezier(float percentage)
    {
        float u = 1 - percentage;
        float tSquare = percentage * percentage;
        float uSquare = u * u;

        Vector3 p = uSquare * start;
        p += 2 * u * percentage * p1;
        p += tSquare * end;

        return p;
    }
    public override float Distance(Vector3 p1, Vector3 p2)
    {
        return Vector3.Distance(p1, p2);
    }
}
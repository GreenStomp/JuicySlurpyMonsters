using UnityEngine;
[ExecuteInEditMode]
public class BezierCurveShower : MonoBehaviour
{
    public BezierCurve CurveToDraw { get; set; }
    [Range(0.01f, 1f)]
    public float PointFrequency = 0.01f;
    public bool ShowLine = true;
    public LineRenderer Line;
    void Update()
    {
        if (CurveToDraw != null)
        {
            int length = (int)(1 / PointFrequency) + 1;
            float current = 0f;

            if (Line != null)
            {
                if (ShowLine)
                {
                    Line.enabled = true;
                    Line.positionCount = length;
                    for (int i = 0; current < 1f; i++)
                    {
                        Line.SetPosition(i, CurveToDraw.GetPoint(current));
                        current += PointFrequency;
                    }
                    Line.SetPosition(length - 1, CurveToDraw.GetPoint(1f));
                }
                else
                {
                    Line.enabled = false;
                    Line.positionCount = 0;
                }
            }
        }
    }
    void OnEnable()
    {
        if (Line)
            Line.enabled = true;
    }
    void OnDisable()
    {
        CurveToDraw = new BezierCurve();
        if (Line)
            Line.positionCount = 0;
    }
}
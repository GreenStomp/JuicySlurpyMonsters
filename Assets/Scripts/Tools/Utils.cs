using System;
using UnityEngine;
public static class Utils
{
    #region Not Used
    /// <summary>
    /// Get open-ended Bezier Spline Control Points.
    /// </summary>
    /// <param name="knots">Input Knot Bezier spline points.</param>
    /// <param name="firstControlPoints">Output First Control points
    /// array of knots.Length - 1 length.</param>
    /// <param name="secondControlPoints">Output Second Control points
    /// array of knots.Length - 1 length.</param>
    /// <exception cref="ArgumentNullException"><paramref name="knots"/>
    /// parameter must be not null.</exception>
    /// <exception cref="ArgumentException"><paramref name="knots"/>
    /// array must contain at least two points.</exception>
    public static void GetCurveControlPoints(V2[] knots, out V2[] firstControlPoints, out V2[] secondControlPoints)
    {
        if (knots == null)
            throw new ArgumentNullException("knots");
        int n = knots.Length - 1;
        if (n < 1)
            throw new ArgumentException
            ("At least two knot points required", "knots");
        if (n == 1)
        { // Special case: Bezier curve should be a straight line.
            firstControlPoints = new V2[1];
            // 3P1 = 2P0 + P3
            firstControlPoints[0].X = (2 * knots[0].X + knots[1].X) / 3;
            firstControlPoints[0].Y = (2 * knots[0].Y + knots[1].Y) / 3;

            secondControlPoints = new V2[1];
            // P2 = 2P1 – P0
            secondControlPoints[0].X = 2 *
                firstControlPoints[0].X - knots[0].X;
            secondControlPoints[0].Y = 2 *
                firstControlPoints[0].Y - knots[0].Y;
            return;
        }

        // Calculate first Bezier control points
        // Right hand side vector
        double[] rhs = new double[n];

        // Set right hand side X values
        for (int i = 1; i < n - 1; ++i)
            rhs[i] = 4 * knots[i].X + 2 * knots[i + 1].X;
        rhs[0] = knots[0].X + 2 * knots[1].X;
        rhs[n - 1] = (8 * knots[n - 1].X + knots[n].X) / 2.0;
        // Get first control points X-values
        double[] x = GetFirstControlPoints(rhs);

        // Set right hand side Y values
        for (int i = 1; i < n - 1; ++i)
            rhs[i] = 4 * knots[i].Y + 2 * knots[i + 1].Y;
        rhs[0] = knots[0].Y + 2 * knots[1].Y;
        rhs[n - 1] = (8 * knots[n - 1].Y + knots[n].Y) / 2.0;
        // Get first control points Y-values
        double[] y = GetFirstControlPoints(rhs);

        // Fill output arrays.
        firstControlPoints = new V2[n];
        secondControlPoints = new V2[n];
        for (int i = 0; i < n; ++i)
        {
            // First control point
            firstControlPoints[i] = new V2(x[i], y[i]);
            // Second control point
            if (i < n - 1)
                secondControlPoints[i] = new V2(2 * knots
                    [i + 1].X - x[i + 1], 2 *
                    knots[i + 1].Y - y[i + 1]);
            else
                secondControlPoints[i] = new V2((knots
                    [n].X + x[n - 1]) / 2,
                    (knots[n].Y + y[n - 1]) / 2);
        }
    }
    /// <summary>
    /// Solves a tridiagonal system for one of coordinates (x or y)
    /// of first Bezier control points.
    /// </summary>
    /// <param name="rhs">Right hand side vector.</param>
    /// <returns>Solution vector.</returns>
    private static double[] GetFirstControlPoints(double[] rhs)
    {
        int n = rhs.Length;
        double[] x = new double[n]; // Solution vector.
        double[] tmp = new double[n]; // Temp workspace.

        double b = 2.0;
        x[0] = rhs[0] / b;
        for (int i = 1; i < n; i++) // Decomposition and forward substitution.
        {
            tmp[i] = 1 / b;
            b = (i < n - 1 ? 4.0 : 3.5) - tmp[i];
            x[i] = (rhs[i] - x[i - 1]) / b;
        }
        for (int i = 1; i < n; i++)
            x[n - i - 1] -= tmp[n - i] * x[n - i]; // Backsubstitution.

        return x;
    }
    #endregion
    /// <summary>
    /// Algorithm used to calculate 2 control points of a Bezier curve given start + 2 point on curve + end + t1 and t2 values relative to the 2 known points
    /// </summary>
    /// <param name="x0">start</param>
    /// <param name="x4">first point on the curve</param>
    /// <param name="x5">second point on the curve</param>
    /// <param name="x3">end</param>
    /// <param name="t1">first point t</param>
    /// <param name="t2">second point t</param>
    /// <param name="x1">first control point</param>
    /// <param name="x2">second control point</param>
    public static void Bez4pts1(double x0, double x4, double x5, double x3, double t1, double t2, out double x1, out double x2)
    {
        //forse è possibile usare queste due parti commentate per capire quanto i contro point influenziono la "velocità" della curva in certi punti o qualcosa del genere
        // find chord lengths
        //double c1 = Math.Sqrt((x4 - x0) * (x4 - x0));
        //double c2 = Math.Sqrt((x5 - x4) * (x5 - x4));
        //double c3 = Math.Sqrt((x3 - x5) * (x3 - x5));

        // guess "best" t
        //double t1 = c1 / (c1 + c2 + c3);
        //double t2 = (c1 + c2) / (c1 + c2 + c3);

        // transform x1 and x2
        Solvexy(B1(t1), B2(t1), x4 - (x0 * B0(t1)) - (x3 * B3(t1)), B1(t2), B2(t2), x5 - (x0 * B0(t2)) - (x3 * B3(t2)), out x1, out x2);
    }
    // linear equation solver utility for ai + bj = c and di + ej = f
    private static void Solvexy(double a, double b, double c, double d, double e, double f, out double i, out double j)
    {
        j = (c - a / d * f) / (b - a * e / d);
        i = (c - (b * j)) / a;
    }
    // basis functions
    private static double B0(double t) { return (1 - t) * (1 - t) * (1 - t); }
    private static double B1(double t) { return t * (1 - t) * (1 - t) * 3; }
    private static double B2(double t) { return (1 - t) * t * t * 3; }
    private static double B3(double t) { return t * t * t; }
    public static float Lerp(float min, float max, float percentage)
    {
        return (min * (1.0f - percentage)) + (max * percentage);
    }
    /// <summary>
    /// Set Active status of the given obj and all child objs
    /// </summary>
    /// <param name="rootObject">root object</param>
    /// <param name="isActive">active value</param>
    public static void DoRecursiveGOActive(this Transform rootObject, bool isActive)
    {
        rootObject.gameObject.SetActive(isActive);
        foreach (Transform childTransform in rootObject)
        {
            childTransform.DoRecursiveGOActive(isActive);
        }
    }
    /// <summary>
    /// Set Active status of the given obj and all child objs
    /// </summary>
    /// <param name="rootObject">root object</param>
    /// <param name="isActive">active value</param>
    public static void DoRecursiveGOActive(this GameObject rootObject, bool isActive)
    {
        rootObject.SetActive(isActive);
        Transform baseT = rootObject.transform;
        foreach (Transform childTransform in baseT)
        {
            childTransform.DoRecursiveGOActive(isActive);
        }
    }
    /// <summary>
    /// Performs a given action on the given obj and all child objs
    /// </summary>
    /// <param name="rootObject">root object</param>
    /// <param name="action">action to perform</param>
    public static void DoRecursiveGOAction(this Transform rootObject, Action<Transform> action)
    {
        action(rootObject);
        foreach (Transform childTransform in rootObject)
        {
            action(childTransform);
        }
    }
    /// <summary>
    /// Performs a given action on the given obj and all child objs
    /// </summary>
    /// <param name="rootObject">root object</param>
    /// <param name="action">action to perform</param>
    public static void DoRecursiveGOAction(this GameObject rootObject, Action<Transform> action)
    {
        Transform baseT = rootObject.transform;
        action(baseT);
        foreach (Transform childTransform in baseT)
        {
            action(childTransform);
        }
    }
    /// <summary>
    /// Set Enabled status to all child Behaviours of the given type to the given value
    /// </summary>
    /// <typeparam name="T">Behaviour type</typeparam>
    /// <param name="rootObject">root obj</param>
    /// <param name="isEnabled">enabled value</param>
    /// <param name="includeInactiveGO">determines if inactive child GameObjects shall be included</param>
    public static void DoRecursiveEnabled<T>(this Transform rootObject, bool isEnabled, bool includeInactiveGO = true) where T : Behaviour
    {
        T[] list = rootObject.GetComponentsInChildren<T>(includeInactiveGO);
        int length = list.Length;
        for (int i = 0; i < length; i++)
        {
            list[i].enabled = isEnabled;
        }
    }
    /// <summary>
    /// Set Enabled status to all child Behaviours of the given type to the given value
    /// </summary>
    /// <typeparam name="T">Behaviour type</typeparam>
    /// <param name="rootObject">root obj</param>
    /// <param name="isEnabled">enabled value</param>
    /// <param name="includeInactiveGO">determines if inactive child GameObjects shall be included</param>
    public static void DoRecursiveEnabled<T>(this GameObject rootObject, bool isEnabled, bool includeInactiveGO = true) where T : Behaviour
    {
        T[] list = rootObject.GetComponentsInChildren<T>(includeInactiveGO);
        int length = list.Length;
        for (int i = 0; i < length; i++)
        {
            list[i].enabled = isEnabled;
        }
    }
    /// <summary>
    /// Performs a given action on all child Components of the given type
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <param name="rootObject">root obj</param>
    /// <param name="action">action to perform</param>
    /// <param name="includeInactiveGO">determines if inactive child GameObjects shall be included</param>
    public static void DoRecursiveAction<T>(this Transform rootObject, Action<T> action, bool includeInactiveGO = true) where T : Component
    {
        T[] list = rootObject.GetComponentsInChildren<T>(includeInactiveGO);
        int length = list.Length;
        for (int i = 0; i < length; i++)
        {
            action(list[i]);
        }
    }
    /// <summary>
    /// Performs a given action on all child Components of the given type
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <param name="rootObject">root obj</param>
    /// <param name="action">action to perform</param>
    /// <param name="includeInactiveGO">determines if inactive child GameObjects shall be included</param>
    public static void DoRecursiveAction<T>(this GameObject rootObject, Action<T> action, bool includeInactiveGO = true) where T : Component
    {
        T[] list = rootObject.GetComponentsInChildren<T>(includeInactiveGO);
        int length = list.Length;
        for (int i = 0; i < length; i++)
        {
            action(list[i]);
        }
    }
}
public struct V2
{
    public double X;
    public double Y;
    public V2(double x, double y)
    {
        this.X = x;
        this.Y = y;
    }
}
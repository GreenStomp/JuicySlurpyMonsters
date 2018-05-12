using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Tools")]
[TestOf(typeof(BezierCurve))]
public class TestBezierCurve
{
    BezierCurve curve;
    [SetUp]
    public void SetUpBezierCurve()
    {
        curve = new BezierCurve();
    }
    [Test]
    public void TestInitializzationLength()
    {
        Assert.That(curve.Length, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationLengthRedLight()
    {
        Assert.That(curve.Length, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializzationInverseLength()
    {
        Assert.That(curve.InverseLength, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationInverseLengthRedLight()
    {
        Assert.That(curve.InverseLength, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializzationValidPoints()
    {
        Assert.That(curve.ValidPoints, Is.EqualTo(BezierCurve.MinValidPoints));
    }
    [Test]
    public void TestInitializzationValidPointsRedLight()
    {
        Assert.That(curve.ValidPoints, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestInitializzationStartPosition()
    {
        Assert.That(curve.Start.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.Start.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.Start.z, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationStartPositionRedLight()
    {
        Assert.That(curve.Start.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.Start.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.Start.z, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializzationP1Position()
    {
        Assert.That(curve.P1.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.P1.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.P1.z, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationP1PositionRedLight()
    {
        Assert.That(curve.P1.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.P1.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.P1.z, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializzationP2Position()
    {
        Assert.That(curve.P2.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.P2.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.P2.z, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationP2PositionRedLight()
    {
        Assert.That(curve.P2.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.P2.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.P2.z, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializzationEndPosition()
    {
        Assert.That(curve.End.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.End.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.End.z, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationEndPositionRedLight()
    {
        Assert.That(curve.End.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.End.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.End.z, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSetLength()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.Length, Is.EqualTo(60).Within(0.0001));
    }
    [Test]
    public void TestSetLengthRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.Length, Is.Not.EqualTo(61).Within(0.0001));
    }
    [Test]
    public void TestSetInverseLength()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.InverseLength, Is.EqualTo(0.0166).Within(0.0001));
    }
    [Test]
    public void TestSetInverseLengthRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.InverseLength, Is.Not.EqualTo(0.02).Within(0.0001));
    }
    [Test]
    public void TestGetLength()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.GetLength(), Is.EqualTo(60).Within(0.0001));
    }
    [Test]
    public void TestGetLengthRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.GetLength(), Is.Not.EqualTo(61).Within(0.0001));
    }
    [Test]
    public void TestGetInverseLength()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(1f / curve.GetLength(), Is.EqualTo(0.0166).Within(0.0001));
    }
    [Test]
    public void TestGetInverseLengthRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(1f / curve.GetLength(), Is.Not.EqualTo(0.02).Within(0.0001));
    }
    [Test]
    public void TestSetLengthNoValidPointsChange()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 2);
        Assert.That(curve.Length, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetLengthNoValidPointsChangeRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 2);
        Assert.That(curve.Length, Is.Not.EqualTo(60).Within(0.0001));
    }
    [Test]
    public void TestSetInverseLengthNoValidPointsChange()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 2);
        Assert.That(curve.InverseLength, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetInverseLengthNoValidPointsChangeRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 2);
        Assert.That(curve.InverseLength, Is.Not.EqualTo(0.0166).Within(0.0001));
    }
    [Test]
    public void TestForcedSetLengthNoValidPointsChange()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 2);
        curve.ForceUpdateLenghts();
        Assert.That(curve.Length, Is.EqualTo(60).Within(0.0001));
    }
    [Test]
    public void TestForcedSetLengthNoValidPointsChangeRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 2);
        curve.ForceUpdateLenghts();
        Assert.That(curve.Length, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestForcedSetInverseLengthNoValidPointsChange()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 2);
        curve.ForceUpdateLenghts();
        Assert.That(curve.InverseLength, Is.EqualTo(0.0166).Within(0.0001));
    }
    [Test]
    public void TestForcedSetInverseLengthNoValidPointsChangeRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 2);
        curve.ForceUpdateLenghts();
        Assert.That(curve.InverseLength, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestForcedSetInverseLengthDivisionByZero()
    {
        curve.ForceUpdateLenghts();
        Assert.That(curve.InverseLength, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestForcedSetInverseLengthDivisionByZeroRedLigth()
    {
        curve.ForceUpdateLenghts();
        Assert.That(curve.InverseLength, Is.Not.NaN);
    }
    [Test]
    public void TestSetValidPoints()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.ValidPoints, Is.EqualTo(4));
    }
    [Test]
    public void TestSetValidPointsRedLight()
    {
        curve.Set(Vector3.zero, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.ValidPoints, Is.Not.EqualTo(3));
    }
    [Test]
    public void TestSetOverflowUpValidPoints()
    {
        curve.ValidPoints = 10;
        Assert.That(curve.ValidPoints, Is.EqualTo(BezierCurve.MaxValidPoints));
    }
    [Test]
    public void TestSetOverflowUpValidPointsRedLight()
    {
        curve.ValidPoints = 10;
        Assert.That(curve.ValidPoints, Is.Not.EqualTo(10));
    }
    [Test]
    public void TestSetOverflowDownValidPoints()
    {
        curve.ValidPoints = 0;
        Assert.That(curve.ValidPoints, Is.EqualTo(BezierCurve.MinValidPoints));
    }
    [Test]
    public void TestSetOverflowDownValidPointsRedLight()
    {
        curve.ValidPoints = 0;
        Assert.That(curve.ValidPoints, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSetStartPosition()
    {
        curve.Set(Vector3.one, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.Start.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(curve.Start.y, Is.EqualTo(1).Within(0.0001));
        Assert.That(curve.Start.z, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSetStartPositionRedLight()
    {
        curve.Set(Vector3.one, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.Start.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(curve.Start.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(curve.Start.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetP1Position()
    {
        curve.Set(Vector3.one, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.P1.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.P1.y, Is.EqualTo(20).Within(0.0001));
        Assert.That(curve.P1.z, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetP1PositionRedLight()
    {
        curve.Set(Vector3.one, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.P1.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.P1.y, Is.Not.EqualTo(40).Within(0.0001));
        Assert.That(curve.P1.z, Is.Not.EqualTo(40).Within(0.0001));
    }
    [Test]
    public void TestSetP2Position()
    {
        curve.Set(Vector3.one, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.P2.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.P2.y, Is.EqualTo(40).Within(0.0001));
        Assert.That(curve.P2.z, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetP2PositionRedLight()
    {
        curve.Set(Vector3.one, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.P2.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.P2.y, Is.Not.EqualTo(20).Within(0.0001));
        Assert.That(curve.P2.z, Is.Not.EqualTo(20).Within(0.0001));
    }
    [Test]
    public void TestSetEndPosition()
    {
        curve.Set(Vector3.one, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.End.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(curve.End.y, Is.EqualTo(60).Within(0.0001));
        Assert.That(curve.End.z, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetEndPositionRedLight()
    {
        curve.Set(Vector3.one, new Vector3(0, 20), new Vector3(0, 40), new Vector3(0, 60), 4);
        Assert.That(curve.End.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.End.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.End.z, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestCopyValidPoints()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.ValidPoints, Is.EqualTo(4));
    }
    [Test]
    public void TestCopyValidPointsRedLight()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.ValidPoints, Is.Not.EqualTo(3));
    }
    [Test]
    public void TestCopyLength()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.Length, Is.EqualTo(15.9009).Within(0.0001));
    }
    [Test]
    public void TestCopyLengthRedLight()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.Length, Is.Not.EqualTo(16).Within(0.0001));
    }
    [Test]
    public void TestCopyInverseLength()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.InverseLength, Is.EqualTo(0.0628).Within(0.0001));
    }
    [Test]
    public void TestCopyInverseLengthRedLight()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.InverseLength, Is.Not.EqualTo(0.07).Within(0.0001));
    }
    [Test]
    public void TestCopyStartPosition()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.Start.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(curve.Start.y, Is.EqualTo(1).Within(0.0001));
        Assert.That(curve.Start.z, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestCopyStartPositionRedLight()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.Start.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(curve.Start.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(curve.Start.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCopyP1Position()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.P1.x, Is.EqualTo(5).Within(0.0001));
        Assert.That(curve.P1.y, Is.EqualTo(5).Within(0.0001));
        Assert.That(curve.P1.z, Is.EqualTo(2).Within(0.0001));
    }
    [Test]
    public void TestCopyP1PositionRedLight()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.P1.x, Is.Not.EqualTo(3).Within(0.0001));
        Assert.That(curve.P1.y, Is.Not.EqualTo(10).Within(0.0001));
        Assert.That(curve.P1.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCopyP2Position()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.P2.x, Is.EqualTo(3).Within(0.0001));
        Assert.That(curve.P2.y, Is.EqualTo(5).Within(0.0001));
        Assert.That(curve.P2.z, Is.EqualTo(4).Within(0.0001));
    }
    [Test]
    public void TestCopyP2PositionRedLight()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.P2.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.P2.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(curve.P2.z, Is.Not.EqualTo(10).Within(0.0001));
    }
    [Test]
    public void TestCopyEndPosition()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.End.x, Is.EqualTo(10).Within(0.0001));
        Assert.That(curve.End.y, Is.EqualTo(10).Within(0.0001));
        Assert.That(curve.End.z, Is.EqualTo(10).Within(0.0001));
    }
    [Test]
    public void TestCopyEndPositionRedLight()
    {
        BezierCurve other = new BezierCurve();
        other.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        curve.Copy(other);
        Assert.That(curve.End.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(curve.End.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(curve.End.z, Is.Not.EqualTo(2).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierStart()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res = curve.GetPoint(0);
        Assert.That(res.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(res.y, Is.EqualTo(1).Within(0.0001));
        Assert.That(res.z, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierStartRedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res = curve.GetPoint(0);
        Assert.That(res.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierEnd()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res = curve.GetPoint(1);
        Assert.That(res.x, Is.EqualTo(10).Within(0.0001));
        Assert.That(res.y, Is.EqualTo(10).Within(0.0001));
        Assert.That(res.z, Is.EqualTo(10).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierEndRedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res = curve.GetPoint(1);
        Assert.That(res.x, Is.Not.EqualTo(9).Within(0.0001));
        Assert.That(res.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res.z, Is.Not.EqualTo(11).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierMidPoint1()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res = curve.GetPoint(0.3f);
        Assert.That(res.magnitude, Is.GreaterThan(Vector3.one.magnitude));
    }
    [Test]
    public void TestCalculateBezierMidPoint1RedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res = curve.GetPoint(0.3f);
        Assert.That(res.magnitude, Is.Not.LessThan(Vector3.one.magnitude));
    }
    [Test]
    public void TestCalculateBezierMidPoint2()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res1 = curve.GetPoint(0.3f);
        Vector3 res2 = curve.GetPoint(0.6f);
        Assert.That(res2.magnitude, Is.GreaterThan(res1.magnitude));
    }
    [Test]
    public void TestCalculateBezierMidPoint2RedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res1 = curve.GetPoint(0.3f);
        Vector3 res2 = curve.GetPoint(0.6f);
        Assert.That(res2.magnitude, Is.Not.LessThan(res1.magnitude));
    }
    [Test]
    public void TestDifferenceBetweenCalculates()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res1 = curve.GetPoint(0.3f);
        Vector3 res2 = curve.CalculateQuadraticBezier(0.3f);
        Assert.That(res2.magnitude, Is.Not.EqualTo(res1.magnitude));
    }
    [Test]
    public void TestDifferenceBetweenCalculatesRedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res1 = curve.GetPoint(0.3f);
        Vector3 res2 = curve.CalculateQuadraticBezier(0.3f);
        Assert.That(res1.magnitude, Is.Not.EqualTo(res2.magnitude));
    }
    [Test]
    public void TestSimilaritiesBetweenCalculates()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res1 = curve.GetPoint(0.3f);
        Vector3 res2 = curve.CalculateCubicBezier(0.3f);
        Assert.That(res1.x, Is.EqualTo(res2.x).Within(0.0001));
        Assert.That(res1.y, Is.EqualTo(res2.y).Within(0.0001));
        Assert.That(res1.z, Is.EqualTo(res2.z).Within(0.0001));
    }
    [Test]
    public void TestSimilaritiesBetweenCalculatesRedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res1 = curve.GetPoint(0.3f);
        Assert.That(res1.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res1.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res1.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints1()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res1 = curve.GetPoint(0.3f);
        Vector3 res2 = curve.CalculateCubicBezier(0.3f);
        Assert.That(res1.x, Is.EqualTo(res2.x).Within(0.0001));
        Assert.That(res1.y, Is.EqualTo(res2.y).Within(0.0001));
        Assert.That(res1.z, Is.EqualTo(res2.z).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints1RedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 4);
        Vector3 res1 = curve.GetPoint(0.3f);
        Assert.That(res1.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res1.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res1.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints2()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 3);
        Vector3 res1 = curve.GetPoint(0.3f);
        Vector3 res2 = curve.CalculateQuadraticBezier(0.3f);
        Assert.That(res1.x, Is.EqualTo(res2.x).Within(0.0001));
        Assert.That(res1.y, Is.EqualTo(res2.y).Within(0.0001));
        Assert.That(res1.z, Is.EqualTo(res2.z).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints2RedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 3);
        Vector3 res1 = curve.GetPoint(0.3f);
        Assert.That(res1.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res1.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res1.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints3()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 2);
        Vector3 res1 = curve.GetPoint(0.3f);
        Vector3 res2 = curve.CalculateBezierFirst(0.3f);
        Assert.That(res1.x, Is.EqualTo(res2.x).Within(0.0001));
        Assert.That(res1.y, Is.EqualTo(res2.y).Within(0.0001));
        Assert.That(res1.z, Is.EqualTo(res2.z).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints3RedLight()
    {
        curve.Set(Vector3.one, new Vector3(5, 5, 2), new Vector3(3, 5, 4), new Vector3(10, 10, 10), 2);
        Vector3 res1 = curve.GetPoint(0.3f);
        Assert.That(res1.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res1.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(res1.z, Is.Not.EqualTo(0).Within(0.0001));
    }
}
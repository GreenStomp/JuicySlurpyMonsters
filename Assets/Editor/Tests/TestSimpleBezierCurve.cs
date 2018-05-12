using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Tools")]
[TestOf(typeof(SimpleBezierCurve))]
public class TestSimpleBezierCurve
{
    SimpleBezierCurve curve;
    [SetUp]
    public void SetUpSimpleCurve()
    {
        curve = new SimpleBezierCurve();
    }
    [Test]
    public void TestInitializzationValidPoints()
    {
        Assert.That(curve.ValidPoints, Is.EqualTo(SimpleBezierCurve.MinValidPoints));
    }
    [Test]
    public void TestInitializzationValidPointsRedLight()
    {
        Assert.That(curve.ValidPoints, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestInitializzationStart()
    {
        Assert.That(curve.Start, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationStartRedLight()
    {
        Assert.That(curve.Start, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializzationP1()
    {
        Assert.That(curve.P1, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationP1RedLight()
    {
        Assert.That(curve.P1, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializzationP2()
    {
        Assert.That(curve.P2, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationP2RedLight()
    {
        Assert.That(curve.P2, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializzationEnd()
    {
        Assert.That(curve.End, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationEndRedLight()
    {
        Assert.That(curve.End, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSetValidPoints()
    {
        curve.Set(0, 20, 40, 60, 4);
        Assert.That(curve.ValidPoints, Is.EqualTo(4));
    }
    [Test]
    public void TestSetValidPointsRedLight()
    {
        curve.Set(0, 20, 40, 60, 4);
        Assert.That(curve.ValidPoints, Is.Not.EqualTo(3));
    }
    [Test]
    public void TestSetOverflowUpValidPoints()
    {
        curve.ValidPoints = 10;
        Assert.That(curve.ValidPoints, Is.EqualTo(SimpleBezierCurve.MaxValidPoints));
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
        Assert.That(curve.ValidPoints, Is.EqualTo(SimpleBezierCurve.MinValidPoints));
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
        curve.Set(1, 20, 40, 60, 4);
        Assert.That(curve.Start, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSetStartPositionRedLight()
    {
        curve.Set(1, 20, 40, 60, 4);
        Assert.That(curve.Start, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetP1Position()
    {
        curve.Set(1, 20, 40, 60, 4);
        Assert.That(curve.P1, Is.EqualTo(20).Within(0.0001));
    }
    [Test]
    public void TestSetP1PositionRedLight()
    {
        curve.Set(1, 20, 40, 60, 4);
        Assert.That(curve.P1, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSetP2Position()
    {
        curve.Set(1, 20, 40, 60, 4);
        Assert.That(curve.P2, Is.EqualTo(40).Within(0.0001));
    }
    [Test]
    public void TestSetP2PositionRedLight()
    {
        curve.Set(1, 20, 40, 60, 4);
        Assert.That(curve.P2, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSetEndPosition()
    {
        curve.Set(1, 20, 40, 60, 4);
        Assert.That(curve.End, Is.EqualTo(60).Within(0.0001));
    }
    [Test]
    public void TestSetEndPositionRedLight()
    {
        curve.Set(1, 20, 40, 60, 4);
        Assert.That(curve.End, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestCopyValidPoints()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.ValidPoints, Is.EqualTo(4));
    }
    [Test]
    public void TestCopyValidPointsRedLight()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.ValidPoints, Is.Not.EqualTo(3));
    }
    [Test]
    public void TestCopyStartPosition()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.Start, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestCopyStartPositionRedLight()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.Start, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCopyP1Position()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.P1, Is.EqualTo(15).Within(0.0001));
    }
    [Test]
    public void TestCopyP1PositionRedLight()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.P1, Is.Not.EqualTo(3).Within(0.0001));
    }
    [Test]
    public void TestCopyP2Position()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.P2, Is.EqualTo(29).Within(0.0001));
    }
    [Test]
    public void TestCopyP2PositionRedLight()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.P2, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCopyEndPosition()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.End, Is.EqualTo(80).Within(0.0001));
    }
    [Test]
    public void TestCopyEndPositionRedLight()
    {
        SimpleBezierCurve other = new SimpleBezierCurve();
        other.Set(1, 15, 29, 80, 4);
        curve.Copy(other);
        Assert.That(curve.End, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierStart()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res = curve.GetValue(0);
        Assert.That(res, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierStartRedLight()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res = curve.GetValue(0);
        Assert.That(res, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierEnd()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res = curve.GetValue(1);
        Assert.That(res, Is.EqualTo(80).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierEndRedLight()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res = curve.GetValue(1);
        Assert.That(res, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCalculateBezierMidPoint1()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res = curve.GetValue(0.3f);
        Assert.That(res, Is.GreaterThan(1));
    }
    [Test]
    public void TestCalculateBezierMidPoint1RedLight()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res = curve.GetValue(0.3f);
        Assert.That(res, Is.Not.LessThan(1));
    }
    [Test]
    public void TestCalculateBezierMidPoint2()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res1 = curve.GetValue(0.3f);
        float res2 = curve.GetValue(0.6f);
        Assert.That(res2, Is.GreaterThan(res1));
    }
    [Test]
    public void TestCalculateBezierMidPoint2RedLight()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res1 = curve.GetValue(0.3f);
        float res2 = curve.GetValue(0.6f);
        Assert.That(res2, Is.Not.LessThan(res1));
    }
    [Test]
    public void TestDifferenceBetweenCalculates()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res1 = curve.GetValue(0.3f);
        float res2 = curve.CalculateBezierSecond(0.3f);
        Assert.That(res2, Is.Not.EqualTo(res1));
    }
    [Test]
    public void TestDifferenceBetweenCalculatesRedLight()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res1 = curve.GetValue(0.3f);
        float res2 = curve.CalculateBezierSecond(0.3f);
        Assert.That(res1, Is.Not.EqualTo(res2));
    }
    [Test]
    public void TestSimilaritiesBetweenCalculates()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res1 = curve.GetValue(0.3f);
        float res2 = curve.CalculateBezierThird(0.3f);
        Assert.That(res1, Is.EqualTo(res2).Within(0.0001));
    }
    [Test]
    public void TestSimilaritiesBetweenCalculatesRedLight()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res1 = curve.GetValue(0.3f);
        Assert.That(res1, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints1()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res1 = curve.GetValue(0.3f);
        float res2 = curve.CalculateBezierThird(0.3f);
        Assert.That(res1, Is.EqualTo(res2).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints1RedLight()
    {
        curve.Set(1, 15, 29, 80, 4);
        float res1 = curve.GetValue(0.3f);
        Assert.That(res1, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints2()
    {
        curve.Set(1, 15, 29, 80, 3);
        float res1 = curve.GetValue(0.3f);
        float res2 = curve.CalculateBezierSecond(0.3f);
        Assert.That(res1, Is.EqualTo(res2).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints2RedLight()
    {
        curve.Set(1, 15, 29, 80, 3);
        float res1 = curve.GetValue(0.3f);
        Assert.That(res1, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints3()
    {
        curve.Set(1, 15, 29, 80, 2);
        float res1 = curve.GetValue(0.3f);
        float res2 = curve.CalculateBezierFirst(0.3f);
        Assert.That(res1, Is.EqualTo(res2).Within(0.0001));
    }
    [Test]
    public void TestGetPointUsingValidPoints3RedLight()
    {
        curve.Set(1, 15, 29, 80, 2);
        float res1 = curve.GetValue(0.3f);
        Assert.That(res1, Is.Not.EqualTo(0).Within(0.0001));
    }
}

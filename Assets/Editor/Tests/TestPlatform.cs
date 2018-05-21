/*using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Level")]
[TestOf(typeof(Platform))]
public class TestPlatform
{
    GameObject go;
    GameObject terrain;
    GameObject p1;
    GameObject start;
    GameObject p2;
    GameObject end;
    Platform plat;
    [SetUp]
    public void SetupPlatform()
    {
        go = new GameObject();
        go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        terrain = new GameObject();
        terrain.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        start = new GameObject();
        start.transform.SetPositionAndRotation(new Vector3(0, 0, 1), Quaternion.identity);
        p1 = new GameObject();
        p1.transform.SetPositionAndRotation(new Vector3(0, 2, 3), Quaternion.identity);
        p2 = new GameObject();
        p2.transform.SetPositionAndRotation(new Vector3(5, 0, 6), Quaternion.identity);
        end = new GameObject();
        end.transform.SetPositionAndRotation(new Vector3(0, 0, 10), Quaternion.identity);
        start.transform.parent = go.transform;
        p1.transform.parent = go.transform;
        p2.transform.parent = go.transform;
        terrain.transform.parent = go.transform;
        end.transform.parent = go.transform;
        plat = go.AddComponent<Platform>();
    }
    [TearDown]
    public void TearDownPlatform()
    {
        GameObject.Destroy(go);
    }
    [Test]
    public void TestInitializationID()
    {
        Assert.That(plat.ID, Is.EqualTo(0));
    }
    [Test]
    public void TestInitializationIDRedLight()
    {
        Assert.That(plat.ID, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestInitializationSelf()
    {
        Assert.That(plat.Self.GetInstanceID(), Is.EqualTo(go.GetInstanceID()));
    }
    [Test]
    public void TestInitializationSelfRedLight()
    {
        Assert.That(plat.Self.GetInstanceID(), Is.Not.EqualTo(go.GetInstanceID() + 1));
    }
    [Test]
    public void TestInitializationNext()
    {
        Assert.That(plat.Next, Is.Null);
    }
    [Test]
    public void TestInitializationNextRedLight()
    {
        plat.SetPlatform(0, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.Reposition(plat);
        Assert.That(plat.Next, Is.Not.Null);
    }
    [Test]
    public void TestInitializationSpecial()
    {
        Assert.That(plat.Special, Is.Null);
    }
    [Test]
    public void TestInitializationSpecialRedLight()
    {
        go.AddComponent<RevertControls>();
        Assert.That(plat.Special, Is.Null);
    }
    [Test]
    public void TestInitializationForceSpecial()
    {
        Assert.That(plat.Special, Is.Null);
    }
    [Test]
    public void TestInitializationForceSpecialRedLight()
    {
        go.AddComponent<RevertControls>();
        plat.GetSpecialPlatform();
        Assert.That(plat.Special, Is.Not.Null);
    }
    [Test]
    public void TestInitializationGetSpecial()
    {
        Assert.That(plat.GetSpecialPlatform(), Is.Null);
    }
    [Test]
    public void TestInitializationGetSpecialRedLight()
    {
        go.AddComponent<RevertControls>();
        Assert.That(plat.GetSpecialPlatform(), Is.Not.Null);
    }
    [Test]
    public void TestInitializationDifficulty()
    {
        Assert.That(plat.Difficulty, Is.EqualTo(0));
    }
    [Test]
    public void TestInitializationDifficultyRedLight()
    {
        Assert.That(plat.Difficulty, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestInitializationLength()
    {
        Assert.That(plat.Length, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializationLengthRedLight()
    {
        Assert.That(plat.Length, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializationInverseLength()
    {
        Assert.That(plat.InverseLength, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializationInverseLengthRedLight()
    {
        Assert.That(plat.InverseLength, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInitializationCurveValidPoints()
    {
        Assert.That(plat.CurveValidPoints, Is.EqualTo(BezierCurve.MinValidPoints));
    }
    [Test]
    public void TestInitializationCurveValidPointsRedLight()
    {
        Assert.That(plat.CurveValidPoints, Is.Not.EqualTo(-1));
    }
    [Test]
    public void TestInitializationDistanceBetweenLanes()
    {
        Assert.That(plat.DistanceBetweenLanes, Is.EqualTo(Platform.DefaultLanesDistance).Within(0.0001));
    }
    [Test]
    public void TestInitializationDistanceBetweenLanesRedLight()
    {
        Assert.That(plat.DistanceBetweenLanes, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializationLanes()
    {
        Assert.That(plat.Lanes, Is.EqualTo(Platform.DefaultLanesNumber));
    }
    [Test]
    public void TestInitializationLanesRedLight()
    {
        Assert.That(plat.Lanes, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestInitializationEnd()
    {
        Assert.That(plat.EndLocation, Is.Null);
    }
    [Test]
    public void TestInitializationEndRedLight()
    {
        plat.SetPlatform(0, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.EndLocation, Is.Not.Null);
    }
    [Test]
    public void TestInitializationStart()
    {
        Assert.That(plat.StartLocation, Is.Null);
    }
    [Test]
    public void TestInitializationStartRedLight()
    {
        plat.SetPlatform(0, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.StartLocation, Is.Not.Null);
    }
    [Test]
    public void TestSetId()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.ID, Is.EqualTo(2));
    }
    [Test]
    public void TestSetIdRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.ID, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSetStartPosition()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.StartLocation.position.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.position.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.position.z, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSetStartPositionRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.StartLocation.position.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(plat.StartLocation.position.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(plat.StartLocation.position.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetStartRotation()
    {
        start.transform.rotation = new Quaternion(0.5547f, 0.5547f, 0.5547f, 0.2773f);
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.StartLocation.rotation.x, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.y, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.z, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.w, Is.EqualTo(0.2773).Within(0.0001));
    }
    [Test]
    public void TestSetStartRotationRedLight()
    {
        start.transform.rotation = new Quaternion(0.5547f, 0.5547f, 0.5547f, 0.2773f);
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.StartLocation.rotation.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.y, Is.Not.EqualTo(-1).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.z, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.w, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSetEnd()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.EndLocation.GetInstanceID(), Is.EqualTo(end.transform.GetInstanceID()));
    }
    [Test]
    public void TestSetEndRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.EndLocation.GetInstanceID(), Is.Not.EqualTo(end.transform.GetInstanceID() + 1));
    }
    [Test]
    public void TestSetLength()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.Length, Is.EqualTo(9).Within(0.0001));
    }
    [Test]
    public void TestSetLengthRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.Length, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetInverseLength()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.InverseLength, Is.EqualTo(0.1111).Within(0.0001));
    }
    [Test]
    public void TestSetInverseLengthRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.That(plat.InverseLength, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSetValidPoints()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Assert.That(plat.CurveValidPoints, Is.EqualTo(4));
    }
    [Test]
    public void TestSetValidPointsRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Assert.That(plat.CurveValidPoints, Is.Not.EqualTo(2));
    }
    [Test]
    public void TestSet2NullException()
    {
        Assert.Throws<System.NullReferenceException>(() => plat.SetPlatform(3));
    }
    [Test]
    public void TestSet2NullExceptionRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        Assert.DoesNotThrow(() => plat.SetPlatform(3));
    }
    [Test]
    public void TestSet2Id()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.ID, Is.EqualTo(3));
    }
    [Test]
    public void TestSet2IdRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.ID, Is.Not.EqualTo(2));
    }
    [Test]
    public void TestSet2StartPosition()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.StartLocation.position.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.position.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.position.z, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSet2StartPositionRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.StartLocation.position.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(plat.StartLocation.position.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(plat.StartLocation.position.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSet2StartRotation()
    {
        start.transform.rotation = new Quaternion(0.2f, 0.2f, 0.2f, 0.1f);
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.StartLocation.rotation.x, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.y, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.z, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.w, Is.EqualTo(0.2773).Within(0.0001));
    }
    [Test]
    public void TestSet2StartRotationRedLight()
    {
        start.transform.rotation = new Quaternion(0.2f, 0.2f, 0.2f, 0.1f);
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.StartLocation.rotation.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.y, Is.Not.EqualTo(-1).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.z, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.w, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSet2End()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.EndLocation.GetInstanceID(), Is.EqualTo(end.transform.GetInstanceID()));
    }
    [Test]
    public void TestSetEnd2RedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.EndLocation.GetInstanceID(), Is.Not.EqualTo(end.transform.GetInstanceID() + 1));
    }
    [Test]
    public void TestSet2Length()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.Length, Is.EqualTo(9).Within(0.0001));
    }
    [Test]
    public void TestSet2LengthRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.Length, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSet2InverseLength()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.InverseLength, Is.EqualTo(0.1111).Within(0.0001));
    }
    [Test]
    public void TestSet2InverseLengthRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 2);
        plat.SetPlatform(3);
        Assert.That(plat.InverseLength, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSet2ValidPoints()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.SetPlatform(3);
        Assert.That(plat.CurveValidPoints, Is.EqualTo(4));
    }
    [Test]
    public void TestSet2ValidPointsRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.SetPlatform(3);
        Assert.That(plat.CurveValidPoints, Is.Not.EqualTo(3));
    }
    [Test]
    public void TestNextNullWhenGoDisabled()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(plat);
        go.SetActive(false);
        Assert.That(plat.Next, Is.Null);
    }
    [Test]
    public void TestNextNullWhenPlatDisabledRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(plat);
        plat.enabled = false;
        Assert.That(plat.Next, Is.Null);
    }
    [Test]
    public void TestRepositionTransformStartPosition()
    {
        GameObject temp = new GameObject();
        temp.transform.SetPositionAndRotation(new Vector3(5, 5, 5), new Quaternion(0.5547f, 0.5547f, 0.5547f, 0.2773f));
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(temp.transform);
        Assert.That(plat.StartLocation.position.x, Is.EqualTo(5).Within(0.0001));
        Assert.That(plat.StartLocation.position.y, Is.EqualTo(5).Within(0.0001));
        Assert.That(plat.StartLocation.position.z, Is.EqualTo(5).Within(0.0001));
        GameObject.Destroy(temp);
    }
    [Test]
    public void TestRepositionTransformStartPositionRedLight()
    {
        GameObject temp = new GameObject();
        temp.transform.SetPositionAndRotation(new Vector3(5, 5, 5), new Quaternion(0.5547f, 0.5547f, 0.5547f, 0.2773f));
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(temp.transform);
        Assert.That(plat.StartLocation.position.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.position.y, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.position.z, Is.Not.EqualTo(1).Within(0.0001));
        GameObject.Destroy(temp);
    }
    [Test]
    public void TestRepositionTransformStartRotation()
    {
        GameObject temp = new GameObject();
        temp.transform.SetPositionAndRotation(new Vector3(5, 5, 5), new Quaternion(0.2773f, 0.5547f, 0.5547f, 0.5547f));
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(temp.transform);
        Assert.That(plat.StartLocation.rotation.x, Is.EqualTo(0.2773).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.y, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.z, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.w, Is.EqualTo(0.5547).Within(0.0001));
        GameObject.Destroy(temp);
    }
    [Test]
    public void TestRepositionTransformStartRotationRedLight()
    {
        GameObject temp = new GameObject();
        temp.transform.SetPositionAndRotation(new Vector3(5, 5, 5), new Quaternion(0.2773f, 0.5547f, 0.5547f, 0.5547f));
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(temp.transform);
        Assert.That(plat.StartLocation.rotation.x, Is.Not.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.y, Is.Not.EqualTo(0.2773).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.z, Is.Not.EqualTo(0.2773).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.w, Is.Not.EqualTo(0.2773).Within(0.0001));
        GameObject.Destroy(temp);
    }
    [Test]
    public void TestRepositionPlatformStartPosition()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(plat);
        Assert.That(plat.StartLocation.position.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.position.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(plat.StartLocation.position.z, Is.EqualTo(10).Within(0.0001));
    }
    [Test]
    public void TestRepositionPlatformStartPositionRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(plat);
        Assert.That(plat.StartLocation.position.x, Is.Not.EqualTo(5).Within(0.0001));
        Assert.That(plat.StartLocation.position.y, Is.Not.EqualTo(5).Within(0.0001));
        Assert.That(plat.StartLocation.position.z, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestRepositionPlatformStartRotation()
    {
        end.transform.rotation = new Quaternion(0.2773f, 0.5547f, 0.5547f, 0.5547f);
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(plat);
        Assert.That(plat.StartLocation.rotation.x, Is.EqualTo(0.2773).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.y, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.z, Is.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.w, Is.EqualTo(0.5547).Within(0.0001));
    }
    [Test]
    public void TestRepositionPlatformStartRotationRedLight()
    {
        end.transform.rotation = new Quaternion(0.2773f, 0.5547f, 0.5547f, 0.5547f);
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        plat.Reposition(plat);
        Assert.That(plat.StartLocation.rotation.x, Is.Not.EqualTo(0.5547).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.y, Is.Not.EqualTo(0.2773).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.z, Is.Not.EqualTo(0.2773).Within(0.0001));
        Assert.That(plat.StartLocation.rotation.w, Is.Not.EqualTo(0.2773).Within(0.0001));
    }
    [Test]
    public void TestCurveLength()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        BezierCurve curve = new BezierCurve();
        curve.Set(start.transform.position, p1.transform.position, p2.transform.position, end.transform.position, 4);
        Assert.That(plat.Length, Is.EqualTo(curve.Length).Within(0.0001));
    }
    [Test]
    public void TestCurveLengthRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        BezierCurve curve = new BezierCurve();
        curve.Set(start.transform.position, p1.transform.position, p2.transform.position, end.transform.position, 4);
        Assert.That(plat.Length, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCalculateCurve()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        BezierCurve curve = new BezierCurve();
        curve.Set(start.transform.position, p1.transform.position, p2.transform.position, end.transform.position, 4);
        Vector3 v1 = plat.CalculateBezierCurve(0.5f);
        Vector3 v2 = curve.GetPoint(0.5f);
        Assert.That(v1.x, Is.EqualTo(v2.x).Within(0.0001));
        Assert.That(v1.y, Is.EqualTo(v2.y).Within(0.0001));
        Assert.That(v1.z, Is.EqualTo(v2.z).Within(0.0001));
    }
    [Test]
    public void TestCalculateCurveRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        BezierCurve curve = new BezierCurve();
        curve.Set(start.transform.position, p1.transform.position, p2.transform.position, end.transform.position, 4);
        Vector3 v1 = plat.CalculateBezierCurve(0.5f);
        Vector3 v2 = curve.GetPoint(0.6f);
        Assert.That(v1.x, Is.Not.EqualTo(v2.x).Within(0.0001));
        Assert.That(v1.y, Is.Not.EqualTo(v2.y).Within(0.0001));
        Assert.That(v1.z, Is.Not.EqualTo(v2.z).Within(0.0001));
    }
    [Test]
    public void TestCalculateCurveStart()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Vector3 v1 = plat.CalculateBezierCurve(0.0f);
        Assert.That(v1.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(v1.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(v1.z, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestCalculateCurveStartRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Vector3 v1 = plat.CalculateBezierCurve(0.0f);
        Assert.That(v1.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(v1.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(v1.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCalculateCurveEnd()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Vector3 v1 = plat.CalculateBezierCurve(1f);
        Assert.That(v1.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(v1.y, Is.EqualTo(0).Within(0.0001));
        Assert.That(v1.z, Is.EqualTo(10).Within(0.0001));
    }
    [Test]
    public void TestCalculateCurveEndRedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Vector3 v1 = plat.CalculateBezierCurve(1f);
        Assert.That(v1.x, Is.Not.EqualTo(10).Within(0.0001));
        Assert.That(v1.y, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(v1.z, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestCalculateCurveM1()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Vector3 v1 = plat.CalculateBezierCurve(0.5f);
        Vector3 v2 = plat.CalculateBezierCurve(0.2f);
        Assert.That(v1.magnitude, Is.GreaterThan(v2.magnitude));
    }
    [Test]
    public void TestCalculateCurveM1RedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Vector3 v1 = plat.CalculateBezierCurve(0.5f);
        Vector3 v2 = plat.CalculateBezierCurve(0.2f);
        Assert.That(v1.magnitude, Is.Not.LessThan(v2.magnitude));
    }
    [Test]
    public void TestCalculateCurveM2()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Vector3 v1 = plat.CalculateBezierCurve(0.8f);
        Vector3 v2 = plat.CalculateBezierCurve(1f);
        Assert.That(v1.magnitude, Is.LessThan(v2.magnitude));
    }
    [Test]
    public void TestCalculateCurveM2RedLight()
    {
        plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
        Vector3 v1 = plat.CalculateBezierCurve(0.8f);
        Vector3 v2 = plat.CalculateBezierCurve(1f);
        Assert.That(v1.magnitude, Is.Not.GreaterThan(v2.magnitude));
    }
    //[Test]
    //public void TestCalculateCurveMoveDifference()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    BezierCurve curve = new BezierCurve();
    //    curve.Set(start.transform.position, p1.transform.position, p2.transform.position, end.transform.position, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(0.5f);
    //    Vector3 v2 = curve.GetPoint(0.5f);
    //    Assert.That(v1.x, Is.Not.EqualTo(v2.x).Within(0.0001));
    //    Assert.That(v1.y, Is.Not.EqualTo(v2.y).Within(0.0001));
    //    Assert.That(v1.z, Is.Not.EqualTo(v2.z).Within(0.0001));
    //}
    //[Test]
    //public void TestCalculateCurveMoveDifferenceRedLight()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    BezierCurve curve = new BezierCurve();
    //    curve.Set(start.transform.position, p1.transform.position, p2.transform.position, end.transform.position, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(0.5f);
    //    Vector3 v2 = curve.GetPoint(0.5f);
    //    Assert.That(v1.magnitude, Is.Not.EqualTo(v2.magnitude).Within(0.0001));
    //}
    //[Test]
    //public void TestCalculateCurveMoveStart()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(0.0f);
    //    Assert.That(v1.x, Is.EqualTo(0).Within(0.0001));
    //    Assert.That(v1.y, Is.EqualTo(0).Within(0.0001));
    //    Assert.That(v1.z, Is.EqualTo(1).Within(0.0001));
    //}
    //[Test]
    //public void TestCalculateCurveMoveStartRedLight()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(0.0f);
    //    Assert.That(v1.x, Is.Not.EqualTo(10).Within(0.0001));
    //    Assert.That(v1.y, Is.Not.EqualTo(1).Within(0.0001));
    //    Assert.That(v1.z, Is.Not.EqualTo(0).Within(0.0001));
    //}
    //[Test]
    //public void TestCalculateCurveMoveEnd()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(1f);
    //    Assert.That(v1.x, Is.EqualTo(0).Within(0.0001));
    //    Assert.That(v1.y, Is.EqualTo(0).Within(0.0001));
    //    Assert.That(v1.z, Is.EqualTo(10).Within(0.0001));
    //}
    //[Test]
    //public void TestCalculateCurveMoveEndRedLight()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(1f);
    //    Assert.That(v1.x, Is.Not.EqualTo(10).Within(0.0001));
    //    Assert.That(v1.y, Is.Not.EqualTo(1).Within(0.0001));
    //    Assert.That(v1.z, Is.Not.EqualTo(0).Within(0.0001));
    //}
    //[Test]
    //public void TestCalculateCurveMoveM1()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(0.5f);
    //    Assert.That(v1.magnitude, Is.GreaterThan(1));
    //}
    //[Test]
    //public void TestCalculateCurveMoveM1RedLight()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(0.5f);
    //    Assert.That(v1.magnitude, Is.Not.LessThan(1));
    //}
    //[Test]
    //public void TestCalculateCurveMoveM2()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(0.8f);
    //    Assert.That(v1.magnitude, Is.LessThan(10));
    //}
    //[Test]
    //public void TestCalculateCurveMoveM2RedLight()
    //{
    //    plat.SetPlatform(2, start.transform, p1.transform, p2.transform, end.transform, terrain.transform, 4);
    //    Vector3 v1 = plat.CalculateBezierCurveMove(0.8f);
    //    Assert.That(v1.magnitude, Is.Not.GreaterThan(10));
    //}
}*/
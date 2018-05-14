using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Tools")]
[TestOf(typeof(PlatDeathPlane))]
public class TestPlatDeathPlane
{
    GameObject go;
    PlatDeathPlane comp;
    [SetUp]
    public void SetupGoPlatDeathPlane()
    {
        go = new GameObject();
        comp = go.AddComponent<PlatDeathPlane>();
    }
    [TearDown]
    public void TearDownGoPlatDeathPlane()
    {
        GameObject.Destroy(go);
    }
	[Test]
	public void TestInitializationDeathPoint()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        Assert.That(comp.DeathSidePoint.x, Is.EqualTo(0).Within(0.001));
        Assert.That(comp.DeathSidePoint.y, Is.EqualTo(0).Within(0.001));
        Assert.That(comp.DeathSidePoint.z, Is.EqualTo(-1).Within(0.001));
    }
    [Test]
    public void TestInitializationDeathPointRedLight()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        Assert.That(comp.DeathSidePoint.x, Is.Not.EqualTo(0.7071f).Within(0.001));
        Assert.That(comp.DeathSidePoint.y, Is.Not.EqualTo(-0.7071f).Within(0.001));
        Assert.That(comp.DeathSidePoint.z, Is.Not.EqualTo(0.7071f).Within(0.001));
    }
    [Test]
    public void TestInitializationPlane()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        Assert.That(comp.DeathPlane.normal.x, Is.EqualTo(0).Within(0.001));
        Assert.That(comp.DeathPlane.normal.y, Is.EqualTo(0).Within(0.001));
        Assert.That(comp.DeathPlane.normal.z, Is.EqualTo(1).Within(0.001));
    }
    [Test]
    public void TestInitializationPlaneRedLight()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        Assert.That(comp.DeathPlane.normal.x, Is.Not.EqualTo(-0.7071f).Within(0.001));
        Assert.That(comp.DeathPlane.normal.y, Is.Not.EqualTo(0.7071f).Within(0.001));
        Assert.That(comp.DeathPlane.normal.z, Is.Not.EqualTo(0.7071f).Within(0.001));
    }
    [Test]
    public void TestUpdatedDeathPoint()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        comp.ForceUpdatePlane();
        Assert.That(comp.DeathSidePoint.x, Is.EqualTo(-0.7071f).Within(0.001));
        Assert.That(comp.DeathSidePoint.y, Is.EqualTo(0.7071f).Within(0.001));
        Assert.That(comp.DeathSidePoint.z, Is.EqualTo(0).Within(0.001));
    }
    [Test]
    public void TestUpdatedDeathPointRedLight()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        comp.ForceUpdatePlane();
        Assert.That(comp.DeathSidePoint.x, Is.Not.EqualTo(0.7071f).Within(0.001));
        Assert.That(comp.DeathSidePoint.y, Is.Not.EqualTo(-0.7071f).Within(0.001));
        Assert.That(comp.DeathSidePoint.z, Is.Not.EqualTo(0.7071f).Within(0.001));
    }
    [Test]
    public void TestUpdatedPlane()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        comp.ForceUpdatePlane();
        Assert.That(comp.DeathPlane.normal.x, Is.EqualTo(0.7071f).Within(0.001));
        Assert.That(comp.DeathPlane.normal.y, Is.EqualTo(-0.7071f).Within(0.001));
        Assert.That(comp.DeathPlane.normal.z, Is.EqualTo(0).Within(0.001));
    }
    [Test]
    public void TestUpdatedPlaneRedLight()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        comp.ForceUpdatePlane();
        Assert.That(comp.DeathPlane.normal.x, Is.Not.EqualTo(-0.7071f).Within(0.001));
        Assert.That(comp.DeathPlane.normal.y, Is.Not.EqualTo(0.7071f).Within(0.001));
        Assert.That(comp.DeathPlane.normal.z, Is.Not.EqualTo(0.7071f).Within(0.001));
    }
    [Test]
    public void TestDeathPointIsBehindPlane()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        go.transform.position = Vector3.one;
        comp.ForceUpdatePlane();
        Assert.That(comp.DeathPlane.SameSide(comp.DeathSidePoint,new Vector3(-1.7071f, 1.7071f, 0)));
    }
    [Test]
    public void TestDeathPointIsBehindPlaneRedLight()
    {
        go.transform.forward = new Vector3(0.7071f, -0.7071f, 0);
        go.transform.position = Vector3.one;
        comp.ForceUpdatePlane();
        Assert.That(!comp.DeathPlane.SameSide(comp.DeathSidePoint, new Vector3(2.7071f, -2.7071f, 0)));
    }
}
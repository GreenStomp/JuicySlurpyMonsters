using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Pools")]
[TestOf(typeof(PoolBasic<BezierCurve>))]
public class TestPoolBasic
{
    [TearDown]
    public void TearDownClearPool()
    {
        PoolBasic<BezierCurve>.Clear();
    }
    [Test]
    public void TestGetNonNullStored()
    {
        PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        Assert.That(PoolBasic<BezierCurve>.Get(), Is.Not.Null);
    }
    [Test]
    public void TestGetNonNullStoredRedLight()
    {
        PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        Assert.That(PoolBasic<BezierCurve>.Get(), !Is.Null);
    }
    [Test]
    public void TestStoredCount()
    {
        PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        Assert.That(PoolBasic<BezierCurve>.ElementsStored, Is.EqualTo(1));
    }
    [Test]
    public void TestStoredCountRedLight()
    {
        PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        Assert.That(PoolBasic<BezierCurve>.ElementsStored, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestGetNonNullInstanciated()
    {
        Assert.That(PoolBasic<BezierCurve>.Get(), Is.Not.Null);
    }
    [Test]
    public void TestGetNonNullInstanciatedRedLight()
    {
        Assert.That(PoolBasic<BezierCurve>.Get(), !Is.Null);
    }
    [Test]
    public void TestGetValidInstanciated()
    {
        Assert.That(PoolBasic<BezierCurve>.Get().ValidPoints, Is.EqualTo(BezierCurve.MinValidPoints));
    }
    [Test]
    public void TestGetValidInstanciatedRedLight()
    {
        Assert.That(PoolBasic<BezierCurve>.Get().ValidPoints, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestDifferenceStoredAndGetted()
    {
        PoolBasic<BezierCurve>.Recycle(new BezierCurve() { ValidPoints = 3 });
        Assert.That(PoolBasic<BezierCurve>.Get().ValidPoints, Is.EqualTo(3));
    }
    [Test]
    public void TestDifferenceStoredAndGettedRedLight()
    {
        PoolBasic<BezierCurve>.Recycle(new BezierCurve() { ValidPoints = 3 });
        Assert.That(PoolBasic<BezierCurve>.Get().ValidPoints, Is.Not.EqualTo(BezierCurve.MinValidPoints));
    }
    [Test]
    public void TestRecycleCountIncrease()
    {
        PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        Assert.That(PoolBasic<BezierCurve>.ElementsStored, Is.EqualTo(1));
    }
    [Test]
    public void TestRecycleCountIncreaseRedLight()
    {
        PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        Assert.That(PoolBasic<BezierCurve>.ElementsStored, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestClearCount()
    {
        for (int i = 0; i < 5; i++)
        {
            PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        }
        PoolBasic<BezierCurve>.Clear();
        Assert.That(PoolBasic<BezierCurve>.ElementsStored, Is.EqualTo(0));
    }
    [Test]
    public void TestClearCountRedLight()
    {
        for (int i = 0; i < 5; i++)
        {
            PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        }
        PoolBasic<BezierCurve>.Clear();
        Assert.That(PoolBasic<BezierCurve>.ElementsStored, Is.Not.EqualTo(5));
    }
    [Test]
    public void TestClearCount2()
    {
        for (int i = 0; i < 5; i++)
        {
            PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        }
        PoolBasic<BezierCurve>.Clear((o) => { });
        Assert.That(PoolBasic<BezierCurve>.ElementsStored, Is.EqualTo(0));
    }
    [Test]
    public void TestClearCount2RedLight()
    {
        for (int i = 0; i < 5; i++)
        {
            PoolBasic<BezierCurve>.Recycle(new BezierCurve());
        }
        PoolBasic<BezierCurve>.Clear((o) => { });
        Assert.That(PoolBasic<BezierCurve>.ElementsStored, Is.Not.EqualTo(5));
    }
    [Test]
    public void TestClearOnDestroy()
    {
        BezierCurve temp = new BezierCurve();
        PoolBasic<BezierCurve>.Recycle(temp);
        PoolBasic<BezierCurve>.Clear((o) => { if (o.ValidPoints == 2) o.Set(new Vector3(100, 100, 100), Vector3.one, 2); });
        Assert.That(temp.Start.x, Is.EqualTo(100));
        Assert.That(temp.Start.y, Is.EqualTo(100));
        Assert.That(temp.Start.z, Is.EqualTo(100));
    }
    [Test]
    public void TestClearOnDestroyRedLight()
    {
        BezierCurve temp = new BezierCurve();
        PoolBasic<BezierCurve>.Recycle(temp);
        PoolBasic<BezierCurve>.Clear((o) => { if (o.ValidPoints == 2) o.Set(new Vector3(100, 100, 100), Vector3.one, 2); });
        Assert.That(temp.Start.x, Is.Not.EqualTo(1));
        Assert.That(temp.Start.y, Is.Not.EqualTo(1));
        Assert.That(temp.Start.z, Is.Not.EqualTo(1));
    }
}
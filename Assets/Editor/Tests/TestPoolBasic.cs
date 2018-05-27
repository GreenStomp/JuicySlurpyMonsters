using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Pools")]
[TestOf(typeof(PoolBasic<V3BezierCurve>))]
public class TestPoolBasic
{
    [TearDown]
    public void TearDownClearPool()
    {
        PoolBasic<V3BezierCurve>.Clear();
    }
    [Test]
    public void TestGetNonNullStored()
    {
        PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        Assert.That(PoolBasic<V3BezierCurve>.Get(), Is.Not.Null);
    }
    [Test]
    public void TestGetNonNullStoredRedLight()
    {
        PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        Assert.That(PoolBasic<V3BezierCurve>.Get(), !Is.Null);
    }
    [Test]
    public void TestStoredCount()
    {
        PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        Assert.That(PoolBasic<V3BezierCurve>.ElementsStored, Is.EqualTo(1));
    }
    [Test]
    public void TestStoredCountRedLight()
    {
        PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        Assert.That(PoolBasic<V3BezierCurve>.ElementsStored, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestGetNonNullInstanciated()
    {
        Assert.That(PoolBasic<V3BezierCurve>.Get(), Is.Not.Null);
    }
    [Test]
    public void TestGetNonNullInstanciatedRedLight()
    {
        Assert.That(PoolBasic<V3BezierCurve>.Get(), !Is.Null);
    }
    [Test]
    public void TestGetValidInstanciated()
    {
        Assert.That(PoolBasic<V3BezierCurve>.Get().ValidPoints, Is.EqualTo(V3BezierCurve.MinValidPoints));
    }
    [Test]
    public void TestGetValidInstanciatedRedLight()
    {
        Assert.That(PoolBasic<V3BezierCurve>.Get().ValidPoints, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestDifferenceStoredAndGetted()
    {
        PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve() { ValidPoints = 3 });
        Assert.That(PoolBasic<V3BezierCurve>.Get().ValidPoints, Is.EqualTo(3));
    }
    [Test]
    public void TestDifferenceStoredAndGettedRedLight()
    {
        PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve() { ValidPoints = 3 });
        Assert.That(PoolBasic<V3BezierCurve>.Get().ValidPoints, Is.Not.EqualTo(V3BezierCurve.MinValidPoints));
    }
    [Test]
    public void TestRecycleCountIncrease()
    {
        PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        Assert.That(PoolBasic<V3BezierCurve>.ElementsStored, Is.EqualTo(1));
    }
    [Test]
    public void TestRecycleCountIncreaseRedLight()
    {
        PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        Assert.That(PoolBasic<V3BezierCurve>.ElementsStored, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestClearCount()
    {
        for (int i = 0; i < 5; i++)
        {
            PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        }
        PoolBasic<V3BezierCurve>.Clear();
        Assert.That(PoolBasic<V3BezierCurve>.ElementsStored, Is.EqualTo(0));
    }
    [Test]
    public void TestClearCountRedLight()
    {
        for (int i = 0; i < 5; i++)
        {
            PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        }
        PoolBasic<V3BezierCurve>.Clear();
        Assert.That(PoolBasic<V3BezierCurve>.ElementsStored, Is.Not.EqualTo(5));
    }
    [Test]
    public void TestClearCount2()
    {
        for (int i = 0; i < 5; i++)
        {
            PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        }
        PoolBasic<V3BezierCurve>.Clear((o) => { });
        Assert.That(PoolBasic<V3BezierCurve>.ElementsStored, Is.EqualTo(0));
    }
    [Test]
    public void TestClearCount2RedLight()
    {
        for (int i = 0; i < 5; i++)
        {
            PoolBasic<V3BezierCurve>.Recycle(new V3BezierCurve());
        }
        PoolBasic<V3BezierCurve>.Clear((o) => { });
        Assert.That(PoolBasic<V3BezierCurve>.ElementsStored, Is.Not.EqualTo(5));
    }
    [Test]
    public void TestClearOnDestroy()
    {
        V3BezierCurve temp = new V3BezierCurve();
        PoolBasic<V3BezierCurve>.Recycle(temp);
        PoolBasic<V3BezierCurve>.Clear((o) => { if (o.ValidPoints == 2) o.Set(new Vector3(100, 100, 100), Vector3.one); });
        Assert.That(temp.Start.x, Is.EqualTo(100));
        Assert.That(temp.Start.y, Is.EqualTo(100));
        Assert.That(temp.Start.z, Is.EqualTo(100));
    }
    [Test]
    public void TestClearOnDestroyRedLight()
    {
        V3BezierCurve temp = new V3BezierCurve();
        PoolBasic<V3BezierCurve>.Recycle(temp);
        PoolBasic<V3BezierCurve>.Clear((o) => { if (o.ValidPoints == 2) o.Set(new Vector3(100, 100, 100), Vector3.one); });
        Assert.That(temp.Start.x, Is.Not.EqualTo(1));
        Assert.That(temp.Start.y, Is.Not.EqualTo(1));
        Assert.That(temp.Start.z, Is.Not.EqualTo(1));
    }
}
using UnityEngine;
using System;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Pools")]
[TestOf(typeof(Pool<BezierCurve>))]
public class TestPool
{
    Pool<BezierCurve> pool;
    BezierCurve original;
    Func<BezierCurve, BezierCurve> allocator;
    [SetUp]
    public void SetupPoolAllocatorAndOriginal()
    {
        original = new BezierCurve();
        original.Set(Vector3.one, new Vector3(5, 5, 5), new Vector3(8, 6, 7), new Vector3(10, 10, 10), 4);
        allocator = (c) => { BezierCurve newInstance = new BezierCurve(); newInstance.Copy(original); return newInstance; };
        pool = new Pool<BezierCurve>(allocator, original, 10, (c) => { c.ValidPoints = 3; });
    }
    [Test]
    public void TestInitializzationPreallocation()
    {
        Assert.That(pool.ElementsStored, Is.EqualTo(10));
    }
    [Test]
    public void TestInitializzationPreallocationRedLight()
    {
        Assert.That(pool.ElementsStored, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestInitializzationOnPreallocation()
    {
        Assert.That(pool.Get(original).ValidPoints, Is.EqualTo(3));
    }
    [Test]
    public void TestInitializzationOnPreallocationRedLight()
    {
        Assert.That(pool.Get(original).ValidPoints, Is.Not.EqualTo(4));
    }
    [Test]
    public void TestGetNonNullStored()
    {
        Assert.That(pool.Get(original), Is.Not.Null);
    }
    [Test]
    public void TestGetNonNullStoredRedLight()
    {
        Assert.That(pool.Get(original), !Is.Null);
    }
    [Test]
    public void TestStoredCount()
    {
        pool.Get(original);
        Assert.That(pool.ElementsStored, Is.EqualTo(9));
    }
    [Test]
    public void TestStoredCountRedLight()
    {
        pool.Get(original);
        Assert.That(pool.ElementsStored, Is.Not.EqualTo(10));
    }
    [Test]
    public void TestGetNonNullInstanciated()
    {
        for (int i = 0; i < 10; i++)
        {
            pool.Get(original);
        }
        Assert.That(pool.Get(original), Is.Not.Null);
    }
    [Test]
    public void TestGetNonNullInstanciatedRedLight()
    {
        for (int i = 0; i < 10; i++)
        {
            pool.Get(original);
        }
        Assert.That(pool.Get(original), !Is.Null);
    }
    [Test]
    public void TestGetValidCopyInstanciated()
    {
        for (int i = 0; i < 10; i++)
        {
            pool.Get(original);
        }
        Assert.That(pool.Get(original).ValidPoints, Is.EqualTo(4));
    }
    [Test]
    public void TestGetValidCopyInstanciatedRedLight()
    {
        for (int i = 0; i < 10; i++)
        {
            pool.Get(original);
        }
        Assert.That(pool.Get(original).ValidPoints, Is.Not.EqualTo(3));
    }
    [Test]
    public void TestDifferenceStoredAndInstanciated()
    {
        BezierCurve current = null;
        for (int i = 0; i < 10; i++)
        {
            current = pool.Get(original);
        }
        Assert.That(pool.Get(original).ValidPoints, Is.Not.EqualTo(current.ValidPoints));
    }
    [Test]
    public void TestDifferenceStoredAndInstanciatedRedLight()
    {
        BezierCurve current = null;
        for (int i = 0; i < 10; i++)
        {
            current = pool.Get(original);
        }
        Assert.That(pool.Get(original).ValidPoints, Is.EqualTo(4));
    }
    [Test]
    public void TestRecycleCountIncrease()
    {
        pool.Recycle(new BezierCurve());
        Assert.That(pool.ElementsStored, Is.EqualTo(11));
    }
    [Test]
    public void TestRecycleCountIncreaseRedLight()
    {
        pool.Recycle(new BezierCurve());
        Assert.That(pool.ElementsStored, Is.Not.EqualTo(10));
    }
    [Test]
    public void TestDifferenceRecycledAndGetted()
    {
        for (int i = 0; i < 10; i++)
        {
            pool.Get(original);
        }
        pool.Recycle(new BezierCurve());
        Assert.That(pool.Get(original).ValidPoints, Is.EqualTo(BezierCurve.MinValidPoints));
    }
    [Test]
    public void TestDifferenceRecycledAndGettedRedLight()
    {
        for (int i = 0; i < 10; i++)
        {
            pool.Get(original);
        }
        pool.Recycle(new BezierCurve());
        Assert.That(pool.Get(original).ValidPoints, Is.Not.EqualTo(4));
    }
    [Test]
    public void TestClearCount()
    {
        pool.Clear((o) => { });
        Assert.That(pool.ElementsStored, Is.EqualTo(0));
    }
    [Test]
    public void TestClearCountRedLight()
    {
        pool.Clear((o) => { });
        Assert.That(pool.ElementsStored, Is.Not.EqualTo(10));
    }
    [Test]
    public void TestClearOnDestroy()
    {
        BezierCurve temp = new BezierCurve();
        pool.Recycle(temp);
        pool.Clear((o) => { if (o.ValidPoints == 2) o.Set(new Vector3(100, 100, 100), Vector3.one, 2); });
        Assert.That(temp.Start.x, Is.EqualTo(100));
        Assert.That(temp.Start.y, Is.EqualTo(100));
        Assert.That(temp.Start.z, Is.EqualTo(100));
    }
    [Test]
    public void TestClearOnDestroyRedLight()
    {
        BezierCurve temp = new BezierCurve();
        pool.Recycle(temp);
        pool.Clear((o) => { if (o.ValidPoints == 2) o.Set(new Vector3(100, 100, 100), Vector3.one, 2); });
        Assert.That(temp.Start.x, Is.Not.EqualTo(1));
        Assert.That(temp.Start.y, Is.Not.EqualTo(1));
        Assert.That(temp.Start.z, Is.Not.EqualTo(1));
    }
}
using UnityEngine;
using System;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
[Category("Pools")]
[TestOf(typeof(PoolManager))]
public class TestPoolManager
{
    TestObj original;
    TestObj2 original2;
    [SetUp]
    public void SetupIPoolables()
    {
        original = new GameObject().AddComponent<TestObj>();
        original2 = new GameObject().AddComponent<TestObj2>();
    }
    [TearDown]
    public void TearDownDestroysAllPools()
    {
        PoolManager.DestroyAll();
    }
    [TearDown]
    public void TearDownDestroyOriginals()
    {
        if (original)
        {
            original.Prefab = null;
            GameObject.Destroy(original.Self);
        }
        if (original2)
        {
            original2.Prefab = null;
            GameObject.Destroy(original2.Self);
        }
    }
    [Test]
    public void TestInitializzationCount()
    {
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.EqualTo(0));
    }
    [Test]
    public void TestInitializzationCountRedLight()
    {
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.Not.EqualTo(1));
    }
    [Test]
    public void TestNotExceptionInitialize()
    {
        PoolManager.InitializePool(original);
        Assert.DoesNotThrow(() => PoolManager.InitializePool(original));
    }
    [Test]
    public void TestNotExceptionInitializeRedLight()
    {
        PoolManager.InitializePool(original2);
        Assert.DoesNotThrow(() => PoolManager.InitializePool(original2));
    }
    [Test]
    public void TestGetNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => PoolManager.Get(original));
    }
    [Test]
    public void TestGetNullReferenceExceptionRedLight()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Get(original));
    }
    [Test]
    public void TestGetKeyNotFoundException()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Get(original));
    }
    [Test]
    public void TestGetKeyNotFoundExceptionRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.DoesNotThrow(() => PoolManager.Get(original));
    }
    [Test]
    public void TestGet2NullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => PoolManager.Get(original, (o) => { }));
    }
    [Test]
    public void TestGet2NullReferenceExceptionRedLight()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Get(original, (o) => { }));
    }
    [Test]
    public void TestGet2KeyNotFoundException()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Get(original, (o) => { }));
    }
    [Test]
    public void TestGet2KeyNotFoundExceptionRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.DoesNotThrow(() => PoolManager.Get(original, (o) => { }));
    }
    [Test]
    public void TestRecycleNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => PoolManager.Recycle(original));
    }
    [Test]
    public void TestRecycleNullReferenceExceptionRedLight()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Recycle(original));
    }
    [Test]
    public void TestRecycleKeyNotFoundException()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Recycle(original));
    }
    [Test]
    public void TestRecycleKeyNotFoundExceptionRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.DoesNotThrow(() => PoolManager.Recycle(original));
    }
    [Test]
    public void TestRecycle2NullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => PoolManager.Recycle(original, (o) => { }));
    }
    [Test]
    public void TestRecycle2NullReferenceExceptionRedLight()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Recycle(original, (o) => { }));
    }
    [Test]
    public void TestRecycle2KeyNotFoundException()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Recycle(original, (o) => { }));
    }
    [Test]
    public void TestRecycle2KeyNotFoundExceptionRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.DoesNotThrow(() => PoolManager.Recycle(original, (o) => { }));
    }
    [Test]
    public void TestClearNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => PoolManager.Clear(original));
    }
    [Test]
    public void TestClearNullReferenceExceptionRedLight()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Clear(original));
    }
    [Test]
    public void TestClearKeyNotFoundException()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Clear(original));
    }
    [Test]
    public void TestClearKeyNotFoundExceptionRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.DoesNotThrow(() => PoolManager.Clear(original));
    }
    [Test]
    public void TestDestroyNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => PoolManager.Destroy(original));
    }
    [Test]
    public void TestDestroyNullReferenceExceptionRedLight()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Destroy(original));
    }
    [Test]
    public void TestDestroyKeyNotFoundException()
    {
        original.Prefab = original;
        Assert.Throws<KeyNotFoundException>(() => PoolManager.Destroy(original));
    }
    [Test]
    public void TestDestroyKeyNotFoundExceptionRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.DoesNotThrow(() => PoolManager.Destroy(original));
    }
    [Test]
    public void TestNoExceptionGetOverallCount()
    {
        Assert.DoesNotThrow(() => PoolManager.GetTotalElementsStoredCount());
    }
    [Test]
    public void TestNoExceptionClearAll()
    {
        Assert.DoesNotThrow(() => PoolManager.ClearAll());
    }
    [Test]
    public void TestNoExceptionDestroyAll()
    {
        Assert.DoesNotThrow(() => PoolManager.DestroyAll());
    }
    [Test]
    public void TestNoExceptionInitialize()
    {
        PoolManager.InitializePool(original);
        Assert.DoesNotThrow(() => PoolManager.InitializePool(original));
    }
    [Test]
    public void TestInitializePrefabSet()
    {
        PoolManager.InitializePool(original);
        Assert.That(original.Prefab, Is.EqualTo(original));
    }
    [Test]
    public void TestInitializePrefabSetRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.That(original.Prefab, Is.Not.Null);
    }
    [Test]
    public void TestInitializeWithPreallocPrefabSet()
    {
        PoolManager.InitializePool(original, 10);
        Assert.That(original.Prefab, Is.EqualTo(original));
    }
    [Test]
    public void TestInitializeWithPreallocPrefabSetRedLight()
    {
        PoolManager.InitializePool(original, 10);
        Assert.That(original.Prefab, Is.Not.Null);
    }
    [Test]
    public void TestInitializeWithPreallocCount()
    {
        PoolManager.InitializePool(original, 10);
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.EqualTo(10));
    }
    [Test]
    public void TestInitializeWithPreallocCountRedLight()
    {
        PoolManager.InitializePool(original, 10);
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.Not.EqualTo(0));
    }
    [Test]
    public void TestOverallCount()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 15);
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.EqualTo(25));
    }
    [Test]
    public void TestOverallCountRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 15);
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.Not.EqualTo(15));
    }
    [Test]
    public void TestCount()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 15);
        Assert.That(PoolManager.GetElementsStoredCount(original2), Is.EqualTo(15));
    }
    [Test]
    public void TestCountRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 15);
        Assert.That(PoolManager.GetElementsStoredCount(original2), Is.Not.EqualTo(25));
    }
    [Test]
    public void TestCountChange()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 15);
        PoolManager.Get(original);
        PoolManager.Get(original2);
        PoolManager.Get(original2);
        PoolManager.Get(original2);
        Assert.That(PoolManager.GetElementsStoredCount(original2), Is.EqualTo(12));
    }
    [Test]
    public void TestCountChangeRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 15);
        PoolManager.Get(original);
        PoolManager.Get(original2);
        PoolManager.Get(original2);
        PoolManager.Get(original2);
        Assert.That(PoolManager.GetElementsStoredCount(original2), Is.Not.EqualTo(15));
    }
    [Test]
    public void TestOverallCountChange()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 15);
        PoolManager.Get(original);
        PoolManager.Get(original2);
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.EqualTo(23));
    }
    [Test]
    public void TestOverallCountChangeRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 15);
        PoolManager.Get(original);
        PoolManager.Get(original2);
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.Not.EqualTo(25));
    }
    [Test]
    public void TestGetNonNull()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original), Is.Not.Null);
    }
    [Test]
    public void TestGetNonNullRedLight()
    {
        PoolManager.InitializePool(original2);
        Assert.That(PoolManager.Get(original2), Is.Not.Null);
    }
    [Test]
    public void TestGetCountChange()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.Get(original);
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.EqualTo(9));
    }
    [Test]
    public void TestGetCountChangeRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.Get(original);
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.Not.EqualTo(10));
    }
    [Test]
    public void TestGet2CountChange()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.Get(original, (o) => { });
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.EqualTo(9));
    }
    [Test]
    public void TestGet2CountChangeRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.Get(original, (o) => { });
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.Not.EqualTo(10));
    }
    [Test]
    public void TestGetPrefabCheck()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original).Prefab, Is.EqualTo(original));
    }
    [Test]
    public void TestGetPrefabCheckRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original).Prefab, Is.Not.Null);
    }
    [Test]
    public void TestGetActiveCheck()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original).gameObject.activeSelf, Is.True);
    }
    [Test]
    public void TestGetActiveCheckRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original).gameObject.activeSelf, Is.Not.False);
    }
    [Test]
    public void TestGetSelfCheck()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original).Self, Is.Not.EqualTo(original.Self));
    }
    [Test]
    public void TestGetSelfCheckRedLight()
    {
        PoolManager.InitializePool(original2);
        Assert.That(PoolManager.Get(original2).Self, Is.Not.EqualTo(original.Self));
    }
    [Test]
    public void TestGet2NonNull()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original, (o) => { }), Is.Not.Null);
    }
    [Test]
    public void TestGet2NonNullRedLight()
    {
        PoolManager.InitializePool(original2);
        Assert.That(PoolManager.Get(original2, (o) => { }), Is.Not.Null);
    }
    [Test]
    public void TestGet2PrefabCheck()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original, (o) => { }).Prefab, Is.EqualTo(original));
    }
    [Test]
    public void TestGet2PrefabCheckRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original, (o) => { }).Prefab, Is.Not.Null);
    }
    [Test]
    public void TestGet2ActiveCheck()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original, (o) => { }).gameObject.activeSelf, Is.True);
    }
    [Test]
    public void TestGet2ActiveCheckRedLight()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original, (o) => { }).gameObject.activeSelf, Is.Not.False);
    }
    [Test]
    public void TestGet2SelfCheck()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original, (o) => { }).Self, Is.Not.EqualTo(original.Self));
    }
    [Test]
    public void TestGet2SelfCheckRedLight()
    {
        PoolManager.InitializePool(original2);
        Assert.That(PoolManager.Get(original2, (o) => { }).Self, Is.Not.EqualTo(original.Self));
    }
    [Test]
    public void TestOnGet()
    {
        PoolManager.InitializePool(original);
        Assert.That(PoolManager.Get(original, (o) => { o.Prefab = null; }).Prefab, Is.Null);
    }
    [Test]
    public void TestOnGetRedLight()
    {
        PoolManager.InitializePool(original2);
        Assert.That(PoolManager.Get(original2, (o) => { o.Prefab = null; }).Prefab, Is.Null);
    }
    [Test]
    public void TestRecycleCount()
    {
        PoolManager.InitializePool(original);
        PoolManager.Recycle(PoolManager.Get(original));
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.EqualTo(1));
    }
    [Test]
    public void TestRecycleCountRedLight()
    {
        PoolManager.InitializePool(original);
        PoolManager.Recycle(PoolManager.Get(original));
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.Not.EqualTo(0));
    }
    [Test]
    public void TestRecycleActiveCheck()
    {
        PoolManager.InitializePool(original);
        TestObj temp = PoolManager.Get(original);
        PoolManager.Recycle(temp);
        Assert.That(temp.gameObject.activeSelf, Is.False);
    }
    [Test]
    public void TestRecycleActiveCheckRedLight()
    {
        PoolManager.InitializePool(original);
        TestObj temp = PoolManager.Get(original);
        PoolManager.Recycle(temp);
        Assert.That(temp.gameObject.activeSelf, Is.Not.True);
    }
    [Test]
    public void TestRecycle2Count()
    {
        PoolManager.InitializePool(original);
        PoolManager.Recycle(PoolManager.Get(original), (o) => { });
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.EqualTo(1));
    }
    [Test]
    public void TestRecycle2CountRedLight()
    {
        PoolManager.InitializePool(original);
        PoolManager.Recycle(PoolManager.Get(original), (o) => { });
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.Not.EqualTo(0));
    }
    [Test]
    public void TestRecycle2ActiveCheck()
    {
        PoolManager.InitializePool(original);
        TestObj temp = PoolManager.Get(original, (o) => { });
        PoolManager.Recycle(temp);
        Assert.That(temp.gameObject.activeSelf, Is.False);
    }
    [Test]
    public void TestRecycle2ActiveCheckRedLight()
    {
        PoolManager.InitializePool(original);
        TestObj temp = PoolManager.Get(original, (o) => { });
        PoolManager.Recycle(temp);
        Assert.That(temp.gameObject.activeSelf, Is.Not.True);
    }
    [Test]
    public void TestOnRecycle()
    {
        PoolManager.InitializePool(original2);
        TestObj2 temp = PoolManager.Get(original2);
        temp.V = 5;
        PoolManager.Recycle(temp, (o) => { o.V = 4; });
        Assert.That(temp.V, Is.EqualTo(4));
    }
    [Test]
    public void TestOnRecycleRedLight()
    {
        PoolManager.InitializePool(original2);
        TestObj2 temp = PoolManager.Get(original2);
        temp.V = 5;
        PoolManager.Recycle(temp, (o) => { o.V = 4; });
        Assert.That(temp.V, Is.Not.EqualTo(5));
    }
    [Test]
    public void TestPrefabSettedToNullException()
    {
        PoolManager.InitializePool(original, 10);
        Assert.Throws<NullReferenceException>(() => PoolManager.Recycle(PoolManager.Get(original), (o) => o.Prefab = null));
    }
    [Test]
    public void TestPrefabNotSettedNoException()
    {
        PoolManager.InitializePool(original, 10);
        Assert.DoesNotThrow(() => PoolManager.Recycle(PoolManager.Get(original), (o) => o.Prefab = o.Prefab));
    }
    [Test]
    public void TestClearCount()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.Clear(original);
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.EqualTo(0));
    }
    [Test]
    public void TestClearCountRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.Clear(original);
        Assert.That(PoolManager.GetElementsStoredCount(original), Is.Not.EqualTo(10));
    }
    [Test]
    public void TestClearAllCount()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 20);
        PoolManager.ClearAll();
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.EqualTo(0));
    }
    [Test]
    public void TestClearAllCountRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 20);
        PoolManager.ClearAll();
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.Not.EqualTo(30));
    }
    [UnityTest]
    public IEnumerator TestClearObjsDestroyed()
    {
        PoolManager.InitializePool(original, 10);
        TestObj temp = PoolManager.Get(original);
        PoolManager.Recycle(temp);
        PoolManager.Clear(original);

        yield return null;

        Assert.That(!temp);
    }
    [UnityTest]
    public IEnumerator TestClearObjsDestroyedRedLight()
    {
        PoolManager.InitializePool(original2, 10);
        TestObj2 temp = PoolManager.Get(original2);
        PoolManager.Recycle(temp);
        PoolManager.Clear(original2);

        yield return null;

        Assert.That(!temp);
    }
    [UnityTest]
    public IEnumerator TestClearAllObjsDestroyed()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 20);
        TestObj temp = PoolManager.Get(original);
        PoolManager.Recycle(temp);
        PoolManager.ClearAll();

        yield return null;

        Assert.That(!temp);
    }
    [UnityTest]
    public IEnumerator TestClearAllObjsDestroyedRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 20);
        TestObj2 temp = PoolManager.Get(original2);
        PoolManager.Recycle(temp);
        PoolManager.ClearAll();

        yield return null;

        Assert.That(!temp);
    }
    [Test]
    public void TestDestroyCountException()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.Destroy(original);
        Assert.Throws<KeyNotFoundException>(()=>PoolManager.GetElementsStoredCount(original));
    }
    [Test]
    public void TestDestroyCountExceptionRedLight()
    {
        PoolManager.InitializePool(original2, 10);
        PoolManager.Destroy(original2);
        Assert.Throws<KeyNotFoundException>(() => PoolManager.GetElementsStoredCount(original2));
    }
    [Test]
    public void TestDestroyAllCount()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 20);
        PoolManager.DestroyAll();
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.EqualTo(0));
    }
    [Test]
    public void TestDestroyAllCountRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 20);
        PoolManager.DestroyAll();
        Assert.That(PoolManager.GetTotalElementsStoredCount(), Is.Not.EqualTo(30));
    }
    [UnityTest]
    public IEnumerator TestDestroyObjsDestroyed()
    {
        PoolManager.InitializePool(original, 10);
        TestObj temp = PoolManager.Get(original);
        PoolManager.Recycle(temp);
        PoolManager.Destroy(original);

        yield return null;

        Assert.That(!temp);
    }
    [UnityTest]
    public IEnumerator TestDestroyObjsDestroyedRedLight()
    {
        PoolManager.InitializePool(original2, 10);
        TestObj2 temp = PoolManager.Get(original2);
        PoolManager.Recycle(temp);
        PoolManager.Destroy(original2);

        yield return null;

        Assert.That(!temp);
    }
    [UnityTest]
    public IEnumerator TestDestroyAllObjsDestroyed()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 20);
        TestObj temp = PoolManager.Get(original);
        PoolManager.Recycle(temp);
        PoolManager.DestroyAll();

        yield return null;

        Assert.That(!temp);
    }
    [UnityTest]
    public IEnumerator TestDestroyAllObjsDestroyedRedLight()
    {
        PoolManager.InitializePool(original, 10);
        PoolManager.InitializePool(original2, 20);
        TestObj2 temp = PoolManager.Get(original2);
        PoolManager.Recycle(temp);
        PoolManager.DestroyAll();

        yield return null;

        Assert.That(!temp);
    }

    class TestObj : MonoBehaviour, IPoolable
    {
        public IPoolable Prefab { get; set; }
        public GameObject Self { get { return this.gameObject; } }
    }
    class TestObj2 : MonoBehaviour, IPoolable
    {
        public IPoolable Prefab { get; set; }
        public GameObject Self { get { return this.gameObject; } }
        public int V;
    }
}
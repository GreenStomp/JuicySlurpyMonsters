using System;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Input")]
[TestOf(typeof(MobileInput))]
public class TestMobileInput
{
    GameObject obj;
    MobileInput mobile;
    [SetUp]
    public void SetupGameobjectSwipe()
    {
        obj = new GameObject();
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        mobile = obj.AddComponent<MobileInput>();
    }
    [TearDown]
    public void TearDownGameobjectSwipe()
    {
        GameObject.Destroy(obj);
        mobile = null;
        obj = null;
    }

    [Test]
    public void TestInitializzationInvertedControls()
    {
        Assert.That(mobile.InvertedControls, Is.False);
    }
    [Test]
    public void TestInitializzationSwipeLeft()
    {
        Assert.That(mobile.SwipeLeft, Is.False);
    }
    [Test]
    public void TestInitializzationTap()
    {
        Assert.That(mobile.Tap, Is.False);
    }
    [Test]
    public void TestInitializzationSwipeRight()
    {
        Assert.That(mobile.SwipeRight, Is.False);
    }
    [Test]
    public void TestInitializzationIsDragging()
    {
        Assert.That(mobile.IsDraging, Is.False);
    }
    [Test]
    public void TestInitializzationSwipeDown()
    {
        Assert.That(mobile.SwipeUp, Is.False);
    }
    [Test]
    public void TestInitializzationSwipeUp()
    {
        Assert.That(mobile.SwipeDown, Is.False);
    }
    [Test]
    public void TestInitializzationStartTouch()
    {
        Assert.That(mobile.StartTouch.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationSwipeDelta()
    {
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationInvertedControlsRedLight()
    {
        Assert.That(mobile.InvertedControls, Is.Not.True);
    }
    [Test]
    public void TestInitializzationSwipeLeftRedLight()
    {
        Assert.That(mobile.SwipeLeft, Is.Not.True);
    }
    [Test]
    public void TestInitializzationTapRedLight()
    {
        Assert.That(mobile.Tap, Is.Not.True);
    }
    [Test]
    public void TestInitializzationSwipeRightRedLight()
    {
        Assert.That(mobile.SwipeRight, Is.Not.True);
    }
    [Test]
    public void TestInitializzationIsDraggingRedLight()
    {
        Assert.That(mobile.IsDraging, Is.Not.True);
    }
    [Test]
    public void TestInitializzationSwipeDownRedLight()
    {
        Assert.That(mobile.SwipeUp, Is.Not.True);
    }
    [Test]
    public void TestInitializzationSwipeUpRedLight()
    {
        Assert.That(mobile.SwipeDown, Is.Not.True);
    }
    [Test]
    public void TestInitializzationDoubleTap()
    {
        Assert.That(mobile.DoubleTap, Is.Not.True);
    }
    [Test]
    public void TestInitializzationDoubleTapRedLight()
    {
        Assert.That(mobile.DoubleTap, Is.Not.True);
    }
    [Test]
    public void TestInitializzationStartTouchRedLight()
    {
        Assert.That(mobile.StartTouch.x, Is.Not.EqualTo(1));
        Assert.That(mobile.StartTouch.y, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestInitializzationSwipeDeltaRedLight()
    {
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(1));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestInvertedControlsSetting()
    {
        mobile.InvertedControls = true;
        Assert.That(mobile.InvertedControls, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSettingRedLight()
    {
        mobile.InvertedControls = false;
        Assert.That(mobile.InvertedControls, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestInitializzationSingleton()
    {
        Assert.That(MobileInput.Instance, Is.EqualTo(mobile));
    }
    [Test]
    public void TestInitializzationSingletonRedLightNull()
    {
        Assert.That(MobileInput.Instance, Is.Not.Null);
    }
    [Test]
    public void TestInitializzationSingletonRedLight()
    {
        GameObject o = new GameObject();
        Assert.That(MobileInput.Instance, Is.Not.EqualTo(o.AddComponent<MobileInput>()));
    }
    [Test]
    public void TestSingletonDestroyCurrentSwipe()
    {
        GameObject.DestroyImmediate(mobile);
        Assert.That(MobileInput.Instance, Is.Null);
    }
    [Test]
    public void TestSingletonDestroyNewSwipeInstance()
    {
        GameObject o = new GameObject();
        MobileInput temp = o.AddComponent<MobileInput>();
        Assert.That(temp, Is.Not.EqualTo(MobileInput.Instance));
    }
    [Test]
    public void TestSingletonDestroyCurrentSwipeAndCreateNew()
    {
        GameObject.DestroyImmediate(mobile);
        mobile = obj.AddComponent<MobileInput>();
        Assert.That(MobileInput.Instance, Is.EqualTo(mobile));
    }
    [Test]
    public void TestSingletonDestroyCurrentSwipeRedLight()
    {
        GameObject.DestroyImmediate(mobile);
        Assert.That(MobileInput.Instance, !Is.Not.Null);
    }
    [Test]
    public void TestSingletonDestroyNewSwipeInstanceRedLight()
    {
        GameObject o = new GameObject();
        MobileInput temp = o.AddComponent<MobileInput>();
        Assert.That(temp, !Is.EqualTo(MobileInput.Instance));
    }
    [Test]
    public void TestSingletonDestroyCurrentSwipeAndCreateNewRedLight()
    {
        GameObject.DestroyImmediate(mobile);
        mobile = obj.AddComponent<MobileInput>();
        Assert.That(MobileInput.Instance, !Is.Not.EqualTo(mobile));
    }

#if UNITY_STANDALONE
    [Test]
    public void TestInvertedControlsTap()
    {
        mobile.InvertedControls = true;
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(mobile.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsTapRedLight()
    {
        mobile.InvertedControls = true;
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(mobile.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsDoubleTap()
    {
        mobile.InvertedControls = true;
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(mobile.DoubleTap, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsDoubleTapRedLight()
    {
        mobile.InvertedControls = true;
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(mobile.DoubleTap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeDelta()
    {
        mobile.InvertedControls = true;
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.zero);
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.one);
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(-1).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(-1).Within(0.0001));
    }
    [Test]
    public void TestInvertedControlsSwipeDeltaRedLight()
    {
        mobile.InvertedControls = true;
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.zero);
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.one);
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInvertedControlsSwipeLeft()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeLeftRedLight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeRight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeRightRedLight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeUp()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeUpRedLight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeDown()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeDownRedLight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateTap()
    {
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        mobile.PreUpdate();
        Assert.That(mobile.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateTapRedLight()
    {
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        mobile.PreUpdate();
        Assert.That(mobile.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestSettingDoubleTap()
    {
        mobile.UpdatePCTouchStatus(true, false, false, true, Vector2.zero);
        Assert.That(mobile.DoubleTap, Is.EqualTo(true));
    }
    [Test]
    public void TestSettingDoubleTapRedLight()
    {
        mobile.UpdatePCTouchStatus(true, false, false, true, Vector2.zero);
        Assert.That(mobile.DoubleTap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateDoubleTap()
    {
        mobile.UpdatePCTouchStatus(true, false, false, true, Vector2.zero);
        mobile.PreUpdate();
        Assert.That(mobile.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateDoubleTapRedLight()
    {
        mobile.UpdatePCTouchStatus(true, false, false, true, Vector2.zero);
        mobile.PreUpdate();
        Assert.That(mobile.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeDelta()
    {
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.PreUpdate();
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestPreUpdateSwipeDeltaRedLight()
    {
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.PreUpdate();
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestPreUpdateSwipeLeft()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeLeft, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeLeftRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeLeft, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeRight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeRight, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeRightRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeRight, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeUp()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeUp, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeUpRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeUp, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeDown()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeDown, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeDownRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeDown, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestTapDown()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        Assert.That(mobile.Tap, Is.EqualTo(true));
    }
    [Test]
    public void TestTapDownRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        Assert.That(mobile.Tap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestIsDraggingDown()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        Assert.That(mobile.IsDraging, Is.EqualTo(true));
    }
    [Test]
    public void TestIsDraggingDownRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        Assert.That(mobile.IsDraging, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestStartTouchDown()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(mobile.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(mobile.StartTouch.x, Is.Not.EqualTo(0));
        Assert.That(mobile.StartTouch.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestTapDownUp()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, true, false, false, Vector2.zero);
        Assert.That(mobile.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestTapDownUpRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, true, false, false, Vector2.zero);
        Assert.That(mobile.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestIsDraggingDownUp()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, true, false, false, Vector2.zero);
        Assert.That(mobile.IsDraging, Is.EqualTo(false));
    }
    [Test]
    public void TestIsDraggingDownUpRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, true, false, false, Vector2.zero);
        Assert.That(mobile.IsDraging, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestStartTouchDownUp()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, true, false, false, Vector2.one);
        Assert.That(mobile.StartTouch.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownUpRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, true, false, false, Vector2.one);
        Assert.That(mobile.StartTouch.x, Is.Not.EqualTo(1));
        Assert.That(mobile.StartTouch.y, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestStartTouchDownPRessed()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        Assert.That(mobile.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownPressedRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        Assert.That(mobile.StartTouch.x, Is.Not.EqualTo(0));
        Assert.That(mobile.StartTouch.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSwipeDeltaDownPressed()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(-1).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(-1).Within(0.0001));
    }
    [Test]
    public void TestSwipeDeltaDownPressedRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(0));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSwipeDeltaDownPressedUp()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, true, false, false, -5 * Vector2.one);
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSwipeDeltaDownPressedUpRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, true, false, false, -5 * Vector2.one);
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(-2).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(-2).Within(0.0001));
    }
    [Test]
    public void TestSwipeLeftCheck()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.True);
    }
    [Test]
    public void TestSwipeLeftCheckRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.Not.False);
    }
    [Test]
    public void TestSwipeLeftCheckRedLightOthers()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.False);
    }
    [Test]
    public void TestSwipeRightCheck()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.True);
    }
    [Test]
    public void TestSwipeRightCheckRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.Not.False);
    }
    [Test]
    public void TestSwipeRightCheckRedLightOthers()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.False);
    }
    [Test]
    public void TestSwipeRightCheckRedLightOtherAxis()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.False);
        Assert.That(mobile.SwipeDown, Is.False);
    }
    [Test]
    public void TestSwipeUpCheck()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.True);
    }
    [Test]
    public void TestSwipeUpCheckRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.Not.False);
    }
    [Test]
    public void TestSwipeUpCheckRedLightOthers()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.False);
    }
    [Test]
    public void TestSwipeDownCheck()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.True);
    }
    [Test]
    public void TestSwipeDownCheckRedLight()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.Not.False);
    }
    [Test]
    public void TestSwipeDownCheckRedLightOthers()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.False);
    }
    [Test]
    public void TestSwipeDownCheckRedLightOtherAxis()
    {
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.False);
        Assert.That(mobile.SwipeLeft, Is.False);
    }
#else
    Touch touch;
    [SetUp]
    public void SetUpTouch()
    {
        touch = new Touch();
    }
    [Test]
    public void TestInvertedControlsTap()
    {
        mobile.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsTapRedLight()
    {
        mobile.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsDoubleTap()
    {
        mobile.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.DoubleTap, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsDoubleTapRedLight()
    {
        mobile.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.DoubleTap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeDelta()
    {
        mobile.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(-1).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(-1).Within(0.0001));
    }
    [Test]
    public void TestInvertedControlsSwipeDeltaRedLight()
    {
        mobile.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSettingDoubleTap()
    {
        touch.tapCount = 3;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.DoubleTap, Is.True);
    }
    [Test]
    public void TestSettingDoubleTapRedLight()
    {
        touch.tapCount = 3;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.DoubleTap, Is.Not.False);
    }
    [Test]
    public void TestInvertedControlsSwipeLeft()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeLeftRedLight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeRight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeRightRedLight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeUp()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeUpRedLight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeDown()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeDownRedLight()
    {
        mobile.InvertedControls = true;
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateTap()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.PreUpdate();
        Assert.That(mobile.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateTapRedLight()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.PreUpdate();
        Assert.That(mobile.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateDoubleTap()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        touch.tapCount = 2;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.PreUpdate();
        Assert.That(mobile.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateDoubleTapRedLight()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        touch.tapCount = 2;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.PreUpdate();
        Assert.That(mobile.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeDelta()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.PreUpdate();
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestPreUpdateSwipeDeltaRedLight()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.PreUpdate();
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestPreUpdateSwipeLeft()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeLeft, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeLeftRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeLeft, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeRight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeRight, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeRightRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeRight, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeUp()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeUp, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeUpRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeUp, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeDown()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeDown, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeDownRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        Assert.That(mobile.SwipeDown, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestTapDown()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.Tap, Is.EqualTo(true));
    }
    [Test]
    public void TestTapDownRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.Tap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestIsDraggingDown()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.IsDraging, Is.EqualTo(true));
    }
    [Test]
    public void TestIsDraggingDownRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.IsDraging, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestStartTouchDown()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.StartTouch.x, Is.Not.EqualTo(0));
        Assert.That(mobile.StartTouch.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestTapDownUp()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Ended;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestTapDownUpRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Ended;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestIsDraggingDownUp()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Ended;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.IsDraging, Is.EqualTo(false));
    }
    [Test]
    public void TestIsDraggingDownUpRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Ended;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.IsDraging, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestStartTouchDownUp()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Ended;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.StartTouch.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownUpRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Ended;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.StartTouch.x, Is.Not.EqualTo(1));
        Assert.That(mobile.StartTouch.y, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestStartTouchDownPRessed()
    {
        mobile.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownPressedRedLight()
    {
        mobile.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.StartTouch.x, Is.Not.EqualTo(0));
        Assert.That(mobile.StartTouch.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSwipeDeltaDownPressed()
    {
        mobile.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(-1).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(-1).Within(0.0001));
    }
    [Test]
    public void TestSwipeDeltaDownPressedRedLight()
    {
        mobile.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(0));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSwipeDeltaDownPressedUp()
    {
        mobile.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = new Vector2(-5, -5);
        touch.phase = TouchPhase.Ended;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSwipeDeltaDownPressedUpRedLight()
    {
        mobile.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = new Vector2(-5, -5);
        touch.phase = TouchPhase.Ended;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(-2).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(-2).Within(0.0001));
    }
    [Test]
    public void TestSwipeLeftCheck()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.True);
    }
    [Test]
    public void TestSwipeLeftCheckRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.Not.False);
    }
    [Test]
    public void TestSwipeLeftCheckRedLightOthers()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.False);
    }
    [Test]
    public void TestSwipeRightCheck()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.True);
    }
    [Test]
    public void TestSwipeRightCheckRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.Not.False);
    }
    [Test]
    public void TestSwipeRightCheckRedLightOthers()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeLeft, Is.False);
    }
    [Test]
    public void TestSwipeRightCheckRedLightOtherAxis()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.False);
        Assert.That(mobile.SwipeDown, Is.False);
    }
    [Test]
    public void TestSwipeUpCheck()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.True);
    }
    [Test]
    public void TestSwipeUpCheckRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.Not.False);
    }
    [Test]
    public void TestSwipeUpCheckRedLightOthers()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.False);
    }
    [Test]
    public void TestSwipeDownCheck()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.True);
    }
    [Test]
    public void TestSwipeDownCheckRedLight()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeDown, Is.Not.False);
    }
    [Test]
    public void TestSwipeDownCheckRedLightOthers()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeUp, Is.False);
    }
    [Test]
    public void TestSwipeDownCheckRedLightOtherAxis()
    {
        mobile.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        mobile.SwipeStatusCheck();
        Assert.That(mobile.SwipeRight, Is.False);
        Assert.That(mobile.SwipeLeft, Is.False);
    }
    [Test]
    public void TestCurrentFingerIdSet()
    {
        mobile.PreUpdate();
        touch.fingerId = 2;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.CurrentFingerId, Is.EqualTo(2));
    }
    [Test]
    public void TestCurrentFingerIdSetRedLight()
    {
        mobile.PreUpdate();
        touch.fingerId = 2;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(mobile.CurrentFingerId, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestFingerSwitchIsDraggingReset()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.IsDraging, Is.False);
    }
    [Test]
    public void TestFingerSwitchIsDraggingResetRedLight()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.IsDraging, Is.Not.True);
    }
    [Test]
    public void TestFingerSwitchTapReset()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.Tap, Is.False);
    }
    [Test]
    public void TestFingerSwitchTapResetRedLight()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.Tap, Is.Not.True);
    }
    [Test]
    public void TestFingerSwitchDoubleTapReset()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.tapCount = 1;
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.tapCount = 2;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.DoubleTap, Is.False);
    }
    [Test]
    public void TestFingerSwitchDoubleTapResetRedLight()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.tapCount = 1;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.tapCount = 2;
        touch.phase = TouchPhase.Began;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.DoubleTap, Is.Not.True);
    }
    [Test]
    public void TestFingerSwitchSwipeDeltaReset()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.position = Vector2.one;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestFingerSwitchSwipeDeltaResetRedLight()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.position = Vector2.one;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.SwipeDelta.x, Is.Not.EqualTo(-10).Within(0.0001));
        Assert.That(mobile.SwipeDelta.y, Is.Not.EqualTo(-200).Within(0.0001));
    }
    [Test]
    public void TestFingerSwitchStartTouchReset()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.position = Vector2.one;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestFingerSwitchStartTouchResetRedLight()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.position = Vector2.one;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.StartTouch.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(mobile.StartTouch.y, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestFingerSwitchSwipeReset()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.SwipeLeft, Is.False);
    }
    [Test]
    public void TestFingerSwitchSwipeResetRedLight()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.SwipeLeft, Is.Not.True);
    }
    [Test]
    public void TestCurrentFingerIdSwitch()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.CurrentFingerId, Is.EqualTo(2));
    }
    [Test]
    public void TestCurrentFingerIdSwitchRedLight()
    {
        mobile.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        mobile.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        mobile.SwipeStatusCheck();
        mobile.PreUpdate();
        mobile.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(mobile.CurrentFingerId, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestUpdateInputNullException()
    {
        Assert.Throws<NullReferenceException>(() => mobile.UpdateMobileTouchStatus(null));
    }
#endif

    [UnityTest]
    public IEnumerator TestSingletonDestroyNewSwipe()
    {
        GameObject o = new GameObject();
        MobileInput temp = o.AddComponent<MobileInput>();

        yield return null;

        Assert.That(!temp);
    }

    [UnityTest]
    public IEnumerator TestSingletonDestroyNewSwipeRedLight()
    {
        GameObject o = new GameObject();
        MobileInput temp = o.AddComponent<MobileInput>();

        yield return null;

        Assert.That(!(temp != null));
    }
}
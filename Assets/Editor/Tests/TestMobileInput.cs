using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
[Category("Input")]
public class TestSwipe
{
    GameObject obj;
    MobileInput swipe;
    [SetUp]
    public void SetupGameobjectSwipe()
    {
        obj = new GameObject();
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        swipe = new MobileInput();
    }
    [TearDown]
    public void TearDownGameobjectSwipe()
    {
        GameObject.Destroy(obj);
        swipe = null;
        obj = null;
    }

    [Test]
    public void TestInitializzationInvertedControls()
    {
        Assert.That(swipe.InvertedControls, Is.False);
    }
    [Test]
    public void TestInitializzationSwipeLeft()
    {
        Assert.That(swipe.SwipeLeft, Is.False);
    }
    [Test]
    public void TestInitializzationTap()
    {
        Assert.That(swipe.Tap, Is.False);
    }
    [Test]
    public void TestInitializzationSwipeRight()
    {
        Assert.That(swipe.SwipeRight, Is.False);
    }
    [Test]
    public void TestInitializzationIsDragging()
    {
        Assert.That(swipe.IsDraging, Is.False);
    }
    [Test]
    public void TestInitializzationSwipeDown()
    {
        Assert.That(swipe.SwipeUp, Is.False);
    }
    [Test]
    public void TestInitializzationSwipeUp()
    {
        Assert.That(swipe.SwipeDown, Is.False);
    }
    [Test]
    public void TestInitializzationStartTouch()
    {
        Assert.That(swipe.StartTouch.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationSwipeDelta()
    {
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestInitializzationInvertedControlsRedLight()
    {
        Assert.That(swipe.InvertedControls, Is.Not.True);
    }
    [Test]
    public void TestInitializzationSwipeLeftRedLight()
    {
        Assert.That(swipe.SwipeLeft, Is.Not.True);
    }
    [Test]
    public void TestInitializzationTapRedLight()
    {
        Assert.That(swipe.Tap, Is.Not.True);
    }
    [Test]
    public void TestInitializzationSwipeRightRedLight()
    {
        Assert.That(swipe.SwipeRight, Is.Not.True);
    }
    [Test]
    public void TestInitializzationIsDraggingRedLight()
    {
        Assert.That(swipe.IsDraging, Is.Not.True);
    }
    [Test]
    public void TestInitializzationSwipeDownRedLight()
    {
        Assert.That(swipe.SwipeUp, Is.Not.True);
    }
    [Test]
    public void TestInitializzationSwipeUpRedLight()
    {
        Assert.That(swipe.SwipeDown, Is.Not.True);
    }
    [Test]
    public void TestInitializzationDoubleTap()
    {
        Assert.That(swipe.DoubleTap, Is.Not.True);
    }
    [Test]
    public void TestInitializzationDoubleTapRedLight()
    {
        Assert.That(swipe.DoubleTap, Is.Not.True);
    }
    [Test]
    public void TestInitializzationStartTouchRedLight()
    {
        Assert.That(swipe.StartTouch.x, Is.Not.EqualTo(1));
        Assert.That(swipe.StartTouch.y, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestInitializzationSwipeDeltaRedLight()
    {
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(1));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestInvertedControlsSetting()
    {
        swipe.InvertedControls = true;
        Assert.That(swipe.InvertedControls, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSettingRedLight()
    {
        swipe.InvertedControls = false;
        Assert.That(swipe.InvertedControls, Is.Not.EqualTo(true));
    }

#if UNITY_STANDALONE
    [Test]
    public void TestInvertedControlsTap()
    {
        swipe.InvertedControls = true;
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(swipe.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsTapRedLight()
    {
        swipe.InvertedControls = true;
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(swipe.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsDoubleTap()
    {
        swipe.InvertedControls = true;
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(swipe.DoubleTap, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsDoubleTapRedLight()
    {
        swipe.InvertedControls = true;
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(swipe.DoubleTap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeDelta()
    {
        swipe.InvertedControls = true;
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.zero);
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.one);
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(-1).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(-1).Within(0.0001));
    }
    [Test]
    public void TestInvertedControlsSwipeDeltaRedLight()
    {
        swipe.InvertedControls = true;
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.zero);
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.one);
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestInvertedControlsSwipeLeft()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeLeftRedLight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeRight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeRightRedLight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeUp()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeUpRedLight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeDown()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeDownRedLight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateTap()
    {
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        swipe.PreUpdate();
        Assert.That(swipe.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateTapRedLight()
    {
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        swipe.PreUpdate();
        Assert.That(swipe.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestSettingDoubleTap()
    {
        swipe.UpdatePCTouchStatus(true, false, false, true, Vector2.zero);
        Assert.That(swipe.DoubleTap, Is.EqualTo(true));
    }
    [Test]
    public void TestSettingDoubleTapRedLight()
    {
        swipe.UpdatePCTouchStatus(true, false, false, true, Vector2.zero);
        Assert.That(swipe.DoubleTap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateDoubleTap()
    {
        swipe.UpdatePCTouchStatus(true, false, false, true, Vector2.zero);
        swipe.PreUpdate();
        Assert.That(swipe.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateDoubleTapRedLight()
    {
        swipe.UpdatePCTouchStatus(true, false, false, true, Vector2.zero);
        swipe.PreUpdate();
        Assert.That(swipe.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeDelta()
    {
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.PreUpdate();
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestPreUpdateSwipeDeltaRedLight()
    {
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.PreUpdate();
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestPreUpdateSwipeLeft()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeLeft, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeLeftRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeLeft, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeRight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeRight, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeRightRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeRight, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeUp()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeUp, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeUpRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeUp, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeDown()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeDown, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeDownRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeDown, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestTapDown()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        Assert.That(swipe.Tap, Is.EqualTo(true));
    }
    [Test]
    public void TestTapDownRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        Assert.That(swipe.Tap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestIsDraggingDown()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        Assert.That(swipe.IsDraging, Is.EqualTo(true));
    }
    [Test]
    public void TestIsDraggingDownRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        Assert.That(swipe.IsDraging, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestStartTouchDown()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(swipe.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        Assert.That(swipe.StartTouch.x, Is.Not.EqualTo(0));
        Assert.That(swipe.StartTouch.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestTapDownUp()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, true, false, false, Vector2.zero);
        Assert.That(swipe.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestTapDownUpRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, true, false, false, Vector2.zero);
        Assert.That(swipe.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestIsDraggingDownUp()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, true, false, false, Vector2.zero);
        Assert.That(swipe.IsDraging, Is.EqualTo(false));
    }
    [Test]
    public void TestIsDraggingDownUpRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, true, false, false, Vector2.zero);
        Assert.That(swipe.IsDraging, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestStartTouchDownUp()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, true, false, false, Vector2.one);
        Assert.That(swipe.StartTouch.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownUpRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, false, false, Vector2.one);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, true, false, false, Vector2.one);
        Assert.That(swipe.StartTouch.x, Is.Not.EqualTo(1));
        Assert.That(swipe.StartTouch.y, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestStartTouchDownPRessed()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        Assert.That(swipe.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownPressedRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        Assert.That(swipe.StartTouch.x, Is.Not.EqualTo(0));
        Assert.That(swipe.StartTouch.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSwipeDeltaDownPressed()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(-1).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(-1).Within(0.0001));
    }
    [Test]
    public void TestSwipeDeltaDownPressedRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(0));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSwipeDeltaDownPressedUp()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, true, false, false, -5 * Vector2.one);
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSwipeDeltaDownPressedUpRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, Vector2.one);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, true, false, false, -5 * Vector2.one);
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(-2).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(-2).Within(0.0001));
    }
    [Test]
    public void TestSwipeLeftCheck()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.True);
    }
    [Test]
    public void TestSwipeLeftCheckRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.Not.False);
    }
    [Test]
    public void TestSwipeLeftCheckRedLightOthers()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.False);
    }
    [Test]
    public void TestSwipeRightCheck()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.True);
    }
    [Test]
    public void TestSwipeRightCheckRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.Not.False);
    }
    [Test]
    public void TestSwipeRightCheckRedLightOthers()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.False);
    }
    [Test]
    public void TestSwipeRightCheckRedLightOtherAxis()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(-200, 10));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.False);
        Assert.That(swipe.SwipeDown, Is.False);
    }
    [Test]
    public void TestSwipeUpCheck()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.True);
    }
    [Test]
    public void TestSwipeUpCheckRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.Not.False);
    }
    [Test]
    public void TestSwipeUpCheckRedLightOthers()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, -200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.False);
    }
    [Test]
    public void TestSwipeDownCheck()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.True);
    }
    [Test]
    public void TestSwipeDownCheckRedLight()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.Not.False);
    }
    [Test]
    public void TestSwipeDownCheckRedLightOthers()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.False);
    }
    [Test]
    public void TestSwipeDownCheckRedLightOtherAxis()
    {
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(true, false, true, false, new Vector2(10, 200));
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdatePCTouchStatus(false, false, true, false, Vector2.zero);
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.False);
        Assert.That(swipe.SwipeLeft, Is.False);
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
        swipe.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsTapRedLight()
    {
        swipe.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsDoubleTap()
    {
        swipe.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.DoubleTap, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsDoubleTapRedLight()
    {
        swipe.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.DoubleTap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeDelta()
    {
        swipe.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(-1).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(-1).Within(0.0001));
    }
    [Test]
    public void TestInvertedControlsSwipeDeltaRedLight()
    {
        swipe.InvertedControls = true;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestSettingDoubleTap()
    {
        touch.tapCount = 3;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.DoubleTap, Is.True);
    }
    [Test]
    public void TestSettingDoubleTapRedLight()
    {
        touch.tapCount = 3;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.DoubleTap, Is.Not.False);
    }
    [Test]
    public void TestInvertedControlsSwipeLeft()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeLeftRedLight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeRight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeRightRedLight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeUp()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeUpRedLight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestInvertedControlsSwipeDown()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.EqualTo(true));
    }
    [Test]
    public void TestInvertedControlsSwipeDownRedLight()
    {
        swipe.InvertedControls = true;
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateTap()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.PreUpdate();
        Assert.That(swipe.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateTapRedLight()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.PreUpdate();
        Assert.That(swipe.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateDoubleTap()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        touch.tapCount = 2;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.PreUpdate();
        Assert.That(swipe.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateDoubleTapRedLight()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        touch.tapCount = 2;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.PreUpdate();
        Assert.That(swipe.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeDelta()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.PreUpdate();
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestPreUpdateSwipeDeltaRedLight()
    {
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.PreUpdate();
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(1).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestPreUpdateSwipeLeft()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeLeft, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeLeftRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeLeft, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeRight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeRight, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeRightRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeRight, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeUp()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeUp, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeUpRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeUp, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestPreUpdateSwipeDown()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeDown, Is.EqualTo(false));
    }
    [Test]
    public void TestPreUpdateSwipeDownRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        Assert.That(swipe.SwipeDown, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestTapDown()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.Tap, Is.EqualTo(true));
    }
    [Test]
    public void TestTapDownRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.Tap, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestIsDraggingDown()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.IsDraging, Is.EqualTo(true));
    }
    [Test]
    public void TestIsDraggingDownRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.IsDraging, Is.Not.EqualTo(false));
    }
    [Test]
    public void TestStartTouchDown()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.StartTouch.x, Is.Not.EqualTo(0));
        Assert.That(swipe.StartTouch.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestTapDownUp()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Ended;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.Tap, Is.EqualTo(false));
    }
    [Test]
    public void TestTapDownUpRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Ended;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.Tap, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestIsDraggingDownUp()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Ended;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.IsDraging, Is.EqualTo(false));
    }
    [Test]
    public void TestIsDraggingDownUpRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Ended;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.IsDraging, Is.Not.EqualTo(true));
    }
    [Test]
    public void TestStartTouchDownUp()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Ended;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.StartTouch.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownUpRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = Vector2.one;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Ended;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.StartTouch.x, Is.Not.EqualTo(1));
        Assert.That(swipe.StartTouch.y, Is.Not.EqualTo(1));
    }
    [Test]
    public void TestStartTouchDownPRessed()
    {
        swipe.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestStartTouchDownPressedRedLight()
    {
        swipe.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.StartTouch.x, Is.Not.EqualTo(0));
        Assert.That(swipe.StartTouch.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSwipeDeltaDownPressed()
    {
        swipe.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(-1).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(-1).Within(0.0001));
    }
    [Test]
    public void TestSwipeDeltaDownPressedRedLight()
    {
        swipe.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(0));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestSwipeDeltaDownPressedUp()
    {
        swipe.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = new Vector2(-5, -5);
        touch.phase = TouchPhase.Ended;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestSwipeDeltaDownPressedUpRedLight()
    {
        swipe.PreUpdate();
        touch.position = Vector2.one;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.phase = TouchPhase.Moved;
        touch.position = Vector2.zero;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = new Vector2(-5, -5);
        touch.phase = TouchPhase.Ended;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(-2).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(-2).Within(0.0001));
    }
    [Test]
    public void TestSwipeLeftCheck()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.True);
    }
    [Test]
    public void TestSwipeLeftCheckRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.Not.False);
    }
    [Test]
    public void TestSwipeLeftCheckRedLightOthers()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.False);
    }
    [Test]
    public void TestSwipeRightCheck()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.True);
    }
    [Test]
    public void TestSwipeRightCheckRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.Not.False);
    }
    [Test]
    public void TestSwipeRightCheckRedLightOthers()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeLeft, Is.False);
    }
    [Test]
    public void TestSwipeRightCheckRedLightOtherAxis()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(-200, 10);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.False);
        Assert.That(swipe.SwipeDown, Is.False);
    }
    [Test]
    public void TestSwipeUpCheck()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.True);
    }
    [Test]
    public void TestSwipeUpCheckRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.Not.False);
    }
    [Test]
    public void TestSwipeUpCheckRedLightOthers()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, -200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.False);
    }
    [Test]
    public void TestSwipeDownCheck()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.True);
    }
    [Test]
    public void TestSwipeDownCheckRedLight()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeDown, Is.Not.False);
    }
    [Test]
    public void TestSwipeDownCheckRedLightOthers()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeUp, Is.False);
    }
    [Test]
    public void TestSwipeDownCheckRedLightOtherAxis()
    {
        swipe.PreUpdate();
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        swipe.SwipeStatusCheck();
        Assert.That(swipe.SwipeRight, Is.False);
        Assert.That(swipe.SwipeLeft, Is.False);
    }
    [Test]
    public void TestCurrentFingerIdSet()
    {
        swipe.PreUpdate();
        touch.fingerId = 2;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.CurrentFingerId, Is.EqualTo(2));
    }
    [Test]
    public void TestCurrentFingerIdSetRedLight()
    {
        swipe.PreUpdate();
        touch.fingerId = 2;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch });
        Assert.That(swipe.CurrentFingerId, Is.Not.EqualTo(0));
    }
    [Test]
    public void TestFingerSwitchIsDraggingReset()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.IsDraging, Is.False);
    }
    [Test]
    public void TestFingerSwitchIsDraggingResetRedLight()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.IsDraging, Is.Not.True);
    }
    [Test]
    public void TestFingerSwitchTapReset()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.Tap, Is.False);
    }
    [Test]
    public void TestFingerSwitchTapResetRedLight()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.Tap, Is.Not.True);
    }
    [Test]
    public void TestFingerSwitchDoubleTapReset()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.tapCount = 1;
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.tapCount = 2;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.DoubleTap, Is.False);
    }
    [Test]
    public void TestFingerSwitchDoubleTapResetRedLight()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.phase = TouchPhase.Stationary;
        secondTouch.tapCount = 1;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.tapCount = 2;
        touch.phase = TouchPhase.Began;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.DoubleTap, Is.Not.True);
    }
    [Test]
    public void TestFingerSwitchSwipeDeltaReset()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.position = Vector2.one;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.SwipeDelta.x, Is.EqualTo(0).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestFingerSwitchSwipeDeltaResetRedLight()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.position = Vector2.one;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.SwipeDelta.x, Is.Not.EqualTo(-10).Within(0.0001));
        Assert.That(swipe.SwipeDelta.y, Is.Not.EqualTo(-200).Within(0.0001));
    }
    [Test]
    public void TestFingerSwitchStartTouchReset()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.position = Vector2.one;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.StartTouch.x, Is.EqualTo(1).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.EqualTo(1).Within(0.0001));
    }
    [Test]
    public void TestFingerSwitchStartTouchResetRedLight()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.position = Vector2.one;
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.StartTouch.x, Is.Not.EqualTo(0).Within(0.0001));
        Assert.That(swipe.StartTouch.y, Is.Not.EqualTo(0).Within(0.0001));
    }
    [Test]
    public void TestFingerSwitchSwipeReset()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.SwipeLeft, Is.False);
    }
    [Test]
    public void TestFingerSwitchSwipeResetRedLight()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.SwipeLeft, Is.Not.True);
    }
    [Test]
    public void TestCurrentFingerIdSwitch()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.CurrentFingerId, Is.EqualTo(2));
    }
    [Test]
    public void TestCurrentFingerIdSwitchRedLight()
    {
        swipe.PreUpdate();
        Touch secondTouch = new Touch();
        secondTouch.fingerId = 2;
        touch.fingerId = 0;
        touch.phase = TouchPhase.Began;
        touch.position = new Vector2(10, 200);
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        touch.position = Vector2.zero;
        touch.phase = TouchPhase.Moved;
        swipe.UpdateMobileTouchStatus(new Touch[] { touch, secondTouch });
        swipe.SwipeStatusCheck();
        swipe.PreUpdate();
        swipe.UpdateMobileTouchStatus(new Touch[] { secondTouch });
        Assert.That(swipe.CurrentFingerId, Is.Not.EqualTo(0));
    }

#endif
}
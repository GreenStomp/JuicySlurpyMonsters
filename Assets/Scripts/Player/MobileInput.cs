using UnityEngine;
using System;
public sealed class MobileInput : MonoBehaviour
{
    public static MobileInput Instance { get; private set; }

    public const int DeadZoneSqr = 125 * 125;

    /// <summary>
    /// Indicates if most input controls shall be returned inverted
    /// </summary>
    public bool InvertedControls { get; set; }
    /// <summary>
    /// Tells if a touch is continuous
    /// </summary>
    public bool IsDraging { get; private set; }
    /// <summary>
    /// Initial touching point
    /// </summary>
    public Vector2 StartTouch { get; private set; }
    /// <summary>
    /// Difference between current and initial touch positions. Affected by InvertedControls (-SwipeDelta)
    /// </summary>
    public Vector2 SwipeDelta { get { return InvertedControls ? -swipeDelta : swipeDelta; } }
    /// <summary>
    /// Indicates the first frame in which a tap occurred. Affected by InvertedControls (DoubleTap)
    /// </summary>
    public bool Tap { get { return InvertedControls ? doubleTap : tap; } }
    /// <summary>
    /// Indicates if there have been 2 or more consecutive taps. Affected by InvertedControls (Tap)
    /// </summary>
    public bool DoubleTap { get { return InvertedControls ? tap : doubleTap; } }
    /// <summary>
    /// Indicates if a valid swipe has been performed. Affected by InvertedControls (SwipeRight)
    /// </summary>
    public bool SwipeLeft { get { return InvertedControls ? swipeRight : swipeLeft; } }
    /// <summary>
    /// Indicates if a valid swipe has been performed. Affected by InvertedControls (SwipeLeft)
    /// </summary>
    public bool SwipeRight { get { return InvertedControls ? swipeLeft : swipeRight; } }
    /// <summary>
    /// Indicates if a valid swipe has been performed. Affected by InvertedControls (SwipeDown)
    /// </summary>
    public bool SwipeUp { get { return InvertedControls ? swipeDown : swipeUp; } }
    /// <summary>
    /// Indicates if a valid swipe has been performed. Affected by InvertedControls (SwipeUp)
    /// </summary>
    public bool SwipeDown { get { return InvertedControls ? swipeUp : swipeDown; } }
    /// <summary>
    /// Current touch unique id
    /// </summary>
    public int CurrentFingerId { get { return currentFingerId; } }

    private Vector2 swipeDelta;
    private bool swipeLeft, swipeRight, swipeUp, swipeDown, tap, doubleTap;
    private int currentFingerId;

    void Awake()
    {
        //Singleton
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    /// <summary>
    /// Initializes several fields
    /// </summary>
    public void PreUpdate()
    {
        tap = doubleTap = swipeLeft = swipeRight = swipeDown = swipeUp = false;
        this.swipeDelta = Vector2.zero;
    }
    /// <summary>
    /// Given the SwipeDelta checks if a valid swipe has been performed
    /// </summary>
    public void SwipeStatusCheck()
    {
        if (swipeDelta.sqrMagnitude > DeadZoneSqr)
        {
            //direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right?
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //up or down?
                if (y > 0)
                    swipeUp = true;
                else
                    swipeDown = true;
            }
        }
    }
#if UNITY_STANDALONE
    /// <summary>
    /// Updates most fields given input data
    /// </summary>
    /// <param name="buttonDown">indicates if this is the first frame the button has been pressed</param>
    /// <param name="buttonUp">indicates if this is the first frame the button has been released</param>
    /// <param name="buttonPressed">indicates if the button is pressed</param>
    /// <param name="doubleTap">indicates if a double tap has been performed</param>
    /// <param name="cursorPosition">cursor current position</param>
    public void UpdatePCTouchStatus(bool buttonDown, bool buttonUp, bool buttonPressed, bool doubleTap, Vector2 cursorPosition)
    {
        this.doubleTap = doubleTap;
        if (buttonDown)
        {
            tap = true;
            IsDraging = true;
            StartTouch = cursorPosition;
        }
        //releasing
        else if (buttonUp)
        {
            StartTouch = Vector2.zero;
            IsDraging = false;
        }
        //calculate the distance
        if (IsDraging && buttonPressed)
        {
            swipeDelta = cursorPosition - StartTouch;
        }
    }
#else
    /// <summary>
    /// Updates most fields given input data
    /// </summary>
    /// <param name="touchesList">list of input touches</param>
    public void UpdateMobileTouchStatus(Touch[] touchesList)
    {
        //Se non ci sono tocchi non fare niente
        int touches = touchesList.Length;
        if (touches == 0)
            return;

        //Inizializzo Touch toUse e verifico se corrisponde al tocco preso in considerazione nel frame precedente
        Touch toUse = touchesList[0];
        bool done = toUse.fingerId == currentFingerId;

        //Se l'id non corrisponde cerco l'id giusto fra tutti i vari touch
        if (!done)
        {
            for (int i = 1; i < touches; i++)
            {
                toUse = touchesList[i];
                if (toUse.fingerId == currentFingerId)
                {
                    done = true;
                    break;
                }
            }
        }

        //Se non ho trovato una corrispondenza significa che il touch del frame precedente non c'è più, per cui prendo un nuovo touch ,mi salvo il nuovo id e resetto i vari valori
        if (!done)
        {
            toUse = touchesList[0];
            currentFingerId = toUse.fingerId;
            IsDraging = false;
            StartTouch = toUse.position;
        }

        //tapping
        this.doubleTap = toUse.tapCount > 1;
        if (toUse.phase == TouchPhase.Began)
        {
            tap = true;
            IsDraging = true;
            StartTouch = toUse.position;
        }
        //releasing
        else if (toUse.phase == TouchPhase.Ended || toUse.phase == TouchPhase.Canceled)
        {
            IsDraging = false;
            StartTouch = Vector2.zero;
        }
        //calculate the distance
        if (IsDraging)
        {
            swipeDelta = toUse.position - StartTouch;
        }
    }
    private void UpdateMobileTouchStatus()
    {
        //Se non ci sono tocchi non fare niente
        int touches = Input.touchCount;
        if (touches == 0)
            return;

        //Inizializzo Touch toUse e verifico se corrisponde al tocco preso in considerazione nel frame precedente
        Touch toUse = Input.GetTouch(0);
        bool done = toUse.fingerId == currentFingerId;

        //Se l'id non corrisponde cerco l'id giusto fra tutti i vari touch
        if (!done)
        {
            for (int i = 1; i < touches; i++)
            {
                toUse = Input.GetTouch(i);
                if (toUse.fingerId == currentFingerId)
                {
                    done = true;
                    break;
                }
            }
        }

        //Se non ho trovato una corrispondenza significa che il touch del frame precedente non c'è più, per cui prendo un nuovo touch e mi salvo il nuovo id
        if (!done)
        {
            toUse = Input.GetTouch(0);
            currentFingerId = toUse.fingerId;
            IsDraging = false;
            StartTouch = toUse.position;
        }

        //tapping
        this.doubleTap = toUse.tapCount > 1;
        if (toUse.phase == TouchPhase.Began)
        {
            tap = true;
            IsDraging = true;
            StartTouch = toUse.position;
        }
        //releasing
        else if (toUse.phase == TouchPhase.Ended || toUse.phase == TouchPhase.Canceled)
        {
            IsDraging = false;
            StartTouch = Vector2.zero;
        }
        //calculate the distance
        if (IsDraging)
        {
            swipeDelta = toUse.position - StartTouch;
        }
    }
#endif
    void Update()
    {
        //Initialize values
        PreUpdate();

        //clicking
#if UNITY_STANDALONE
        UpdatePCTouchStatus(Input.GetMouseButtonDown(0), Input.GetMouseButtonUp(0), Input.GetMouseButton(0), Input.GetMouseButtonDown(1), Input.mousePosition);
#else
        UpdateMobileTouchStatus();
#endif

        //did we cross the deadzone?
        SwipeStatusCheck();
    }
    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
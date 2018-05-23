using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{

}

public class PowerUp : MonoBehaviour
{
    [HideInInspector]
    public string ToolTip = "PowerUp";
    public bool DisplayIcon = true;
    public Texture2D Icon;
    public Vector2 IconPosition = Vector3.zero;
    public int Uses = 5;

    [SerializeField]
    private GameObject Shield;
    [SerializeField]
    private GameObject Attractor;

    [HideInInspector]
    public bool IsActive = false;
    [HideInInspector]
    public bool IsPassive = false;
    public float PassiveLifetime = 20f;
    [HideInInspector]
    public float DieAt = 0f;

    public const float Offset = 10.0f;

    public int Width;
    public int Height;

    public virtual void Init()
    {
        if (IsPassive)
        {
            Destroy(this, PassiveLifetime);
            DieAt = Time.time + PassiveLifetime;
        }
        else
        {
            IsActive = true;
        }
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void OnGUI()
    {
        if (!DisplayIcon) return;
        if (!Icon) return;
        if (!GetComponent<Health>()) return;

        Color c = Color.white;

        if (!IsActive || IsPassive) c.a = 0.5f;
        GUI.color = c;

        string s = "";
        if (Uses > 1) s = Uses.ToString();

        GUIContent content = new GUIContent(ToolTip, Icon);

        Rect rect = new Rect(IconPosition.x, IconPosition.y, Width, Height);

        GUI.Box(rect, content);
        s = "";
        if (Uses > 1) s = Uses.ToString();

        if (IsPassive)
        {
            float t = DieAt - Time.time;
            s = (Mathf.Round((DieAt - Time.time) * Offset) / Offset).ToString();
            if (t > 3) s = (Mathf.Floor(DieAt - Time.time)).ToString();
        }

        if (s != "")
        {
            GUI.color = Color.white;
            GUI.Label(rect, s);
        }
    }

    private void Update()
    {
        bool ironHideActive = GetComponent<IronHide>();
        bool magnetCoinActive = GetComponent<MagnetCoin>();

        Shield.SetActive(ironHideActive);
        Attractor.SetActive(magnetCoinActive);
    }
}

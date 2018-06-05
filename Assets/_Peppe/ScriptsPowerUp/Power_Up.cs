using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Power_Up : ScriptableObject
{
    public string Name = "PowerUp";
    public AudioClip Audio;
    public GameObject ParticleEffect;
    public bool Passive;
    public bool Active;
    public Texture ImageIcon;
    public float DieAt=0f;
    public float PassiveLifeTime=20f;
    public  float Offset = 10f;
    public Vector2 IconPosition = Vector3.zero;
    public float Width;
    public float Height;
    public GameObject ToInstance;

    public virtual void Init()
    {
        if (Passive)
        {
            //Destroy(this, PassiveLifeTime);
            DieAt = (Time.time + PassiveLifeTime);
        }
        else
        {
            Active = true;
        }
    }

    public abstract void DoPowerUp(Vector3 position);




}

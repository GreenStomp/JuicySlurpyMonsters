using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronHide : PowerUp, IPowerUp
{
    public string DisplayName = "Shield";

    // Use this for initialization
    void Start()
    {
        if (!GetComponent<Health>())
        {
            SphereCollider col = GetComponent<SphereCollider>();

            if (!col)
            {
                col = gameObject.AddComponent<SphereCollider>();
            }
            col.radius = 0.5f;
            col.isTrigger = true;
            IsActive = false;
        }
    }

    public override void Init()
    {
        ToolTip = DisplayName;
        IsPassive = true;
        IconPosition = Vector2.right * 220;

        if (GetComponent<Health>())
        {
            Destroy(this, PassiveLifetime);
            DieAt = Time.time + PassiveLifetime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Health>())
        {
            //Do animation Effects here
            return;
        }

        if (IsPassive)
        {
            DoPowerUp();
        }
        else if (IsActive)
        {
            if (Input.GetButtonDown("Jump"))
            {
                DoPowerUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.GetComponent<Health>() && !go.GetComponent(typeof(IPowerUp)))
        {
            if (go.GetComponent<IronHide>())
            {
                //Destroy(go.GetComponent<IronHide>());
                go.GetComponent<IronHide>().gameObject.SetActive(false);
            }

            //Create the new powerUp.
            PowerUp po = go.AddComponent<IronHide>();
            po.Icon = Icon;
            po.Width = Width;
            po.Height = Height;
            //Shield = po.Shield;

            Debug.Log(po);
            po.Init();
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    void DoPowerUp()
    {
        // do the actual powerup stuff here.
        //Shield.gameObject.SetActive(true);
       
    }
}

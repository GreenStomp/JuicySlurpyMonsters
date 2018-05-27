using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedSpring : PowerUp
{
    public string DisplayName = "Pedestrian Spring";

	// Use this for initialization
	void Start ()
    {
		if(!GetComponent<Health>())
        {
            SphereCollider col = GetComponent<SphereCollider>();

            if(!col)
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

        if (GetComponent<Health>())
        {
            //Make this the current active powerup
            PowerUp[] pups = (PowerUp[])GetComponents<PowerUp>();
            foreach (PowerUp pup in pups)
            {
                pup.IsActive = false;
            }
            IsActive = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (!GetComponent<Health>())
        {
            //Do Animation Effects Here
            return;
        }

        if(IsPassive)
        {
            DoPowerUp();
        }
        else if(IsActive)
        {
            DoPowerUp();
            //if(Input.GetButtonDown("Jump"))
            //{
            //    DoPowerUp();
            //}
        }
	}

    //Handle the trigger enter stuff and update the player.
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.GetComponent<Health>())
        {
            if (go.GetComponent<PedSpring>())
            {
                Destroy(go.GetComponent<PedSpring>());
            }

            
            //Create the new powerUp
            PowerUp po = go.AddComponent<PedSpring>();
            po.Icon = Icon;
            po.Width = Width;
            po.Height = Height;

            //If it is the only PowerUp then it should be active.
            PowerUp[] pups = (PowerUp[])go.GetComponents<PowerUp>();
            po.Init();
            Destroy(gameObject);
        
        }
    }

    void DoPowerUp()
    {
        //do the actual powerUp stuff here.
        Debug.Log("DoPedSprings!!!");    
    }
}

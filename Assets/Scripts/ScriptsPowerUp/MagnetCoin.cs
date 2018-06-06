using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCoin : PowerUp, IPowerUp
{
    public string DisplayName = "Magnet Coin";

    public float MagnetRange = 45f;
    public float MinDistance = 1f;
    private Collider[] coins;

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
        if (this.isActiveAndEnabled)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.GetComponent<Health>() && !go.GetComponent(typeof(IPowerUp)))
        {
            if (go.GetComponent<MagnetCoin>())
            {
                Destroy(go.GetComponent<MagnetCoin>());
            }

            //Create the new powerUp.
            PowerUp po = go.AddComponent<MagnetCoin>();
            po.Icon = Icon;
            po.Width = Width;
            po.Height = Height;

            Debug.Log(po);
            po.Init();

            //Pool.Recycle()
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void DoPowerUp()
    {
        // do the actual powerup stuff here.

        Vector3 here = transform.position;

        //Find the colliders inside range and in layer 14.
        coins = Physics.OverlapSphere(here, MagnetRange, 1 << 9);

        foreach (var coin in coins)
        {
            Transform tfCoin = coin.transform;
            float dist = Vector3.Distance(tfCoin.position, here);

            //Closer the min distance grab the object.
            if (dist < MinDistance)
            {
                //Add the points to the score.
                //Destroy(tfCoin.gameObject);
                tfCoin.gameObject.SetActive(false);
            }
            else
            {
                //Inside range attract it.
                if (dist <= MagnetRange)
                {
                    float vel = MagnetRange / dist; //Velocity inversely proportional to distance.
                    //move coin to the player.
                    tfCoin.position = Vector3.MoveTowards(tfCoin.position, here, vel);
                }
            }

        }
    }
}

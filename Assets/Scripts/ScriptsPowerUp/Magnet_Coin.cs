using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/MagnetCoin")]
public class Magnet_Coin : Power_Up
{
    public float MagnetRange;
    public float MinDistance;
    private Collider[] coins;

    public override void Init()
    {
        base.Init();
    }

    public override void DoPowerUp(Vector3 here)
    {
        //Vector3 here = transform.position;
        //if (ObjToInstance == null)
        //    ObjToInstance = GameObject.Instantiate(ParticleEffect.gameObject, here, Quaternion.identity);
        //ObjToInstance.transform.parent = pos.transform;


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

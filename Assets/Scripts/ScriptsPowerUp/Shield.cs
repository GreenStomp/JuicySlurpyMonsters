using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/Shield")]
public class Shield : Power_Up
{
    private PlayerLife playerLife;

    public override void Init()
    {
        base.Init();    
    }

    public override void DoPowerUp(Vector3 position)
    {
        ToInstance.transform.parent.GetComponent<PlayerLife>().Immune = true;
    }

    public override void EndPowerUp()
    {
        ToInstance.transform.parent.GetComponent<PlayerLife>().Immune = false;
    }
}

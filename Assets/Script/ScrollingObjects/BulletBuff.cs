using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBuff : ScrollingObject
{
    public float buffDuration;

    public override void OnHitPlayer(FPSController player)
    {
        base.OnHitPlayer(player);
    }
}

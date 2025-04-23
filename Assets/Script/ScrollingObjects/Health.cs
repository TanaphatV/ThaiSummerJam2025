using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : ScrollingObject
{
    public override void OnHitPlayer(FPSController player)
    {
        base.OnHitPlayer(player);
        player.Health += 1;
    }
}

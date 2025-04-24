using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ScrollingObject
{
    public override void OnHitPlayer(FPSController player)
    {
        base.OnHitPlayer(player);
        player.SetHealth(player.Health - 1);
    }
}

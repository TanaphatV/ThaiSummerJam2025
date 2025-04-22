using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ScrollingObject
{
    public int scoreReward;

    public override void OnHitPlayer(FPSController player)
    {
        ScoreCounter.instance.AddScore(scoreReward);
        Destroy(gameObject);
    }

}

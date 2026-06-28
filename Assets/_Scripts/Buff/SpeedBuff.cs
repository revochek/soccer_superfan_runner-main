using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SpeedBuff : IBuff
{
    private readonly float _speedBonus;

    public SpeedBuff(float speedBonus)
    {
        _speedBonus = speedBonus;
    }

    public HeroStats ApplyBuff(HeroStats stats)
    {
        return GetDataWithSpeedModification(stats, _speedBonus);
    }

    private HeroStats GetDataWithSpeedModification(HeroStats stats, float speedBonus)
    {
        var newData = stats;
        newData.MovementSpeed = Mathf.Max(newData.MovementSpeed + speedBonus, 0);
        return newData;
    }
}

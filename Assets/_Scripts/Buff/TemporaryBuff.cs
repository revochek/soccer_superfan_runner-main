using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class TemporaryBuff : IBuff
{
    private IBuffable _owner;
    private IBuff _coreBuff;
    private float _lifeTime;

    private Timer _timer;

    public TemporaryBuff(IBuffable owner, IBuff buff, float lifeTime)
    {
        _owner = owner;
        _coreBuff = buff;
        _lifeTime = lifeTime;

        _timer = Timer.CreateInstance;
    }

    public HeroStats ApplyBuff(HeroStats stats)
    {
        var newStats = _coreBuff.ApplyBuff(stats);

        _timer.StartTimer(_lifeTime);
        _timer.Completed += () =>
        {
            _owner.RemoveBuff(this);
        };

        return newStats;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    HeroStats ApplyBuff(HeroStats stats);
}

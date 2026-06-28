using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    bool CheckActivationPossibility(int countSpell);
    void Activate();
}

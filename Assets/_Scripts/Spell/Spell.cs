using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public abstract class Spell : MonoBehaviour, ISpell
{
    private float _lifeTime=3;
    private Timer _timer;

    //private int _countSpell;
    protected bool IsActivate;

    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    public event UnityAction SpellCompleted;

    public virtual void Initialize(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _persistentData = persistentData;
        _dataProvider = dataProvider;
    }

    public bool CheckActivationPossibility(int countSpell) => !IsActivate && countSpell >= 1;

    public virtual void Activate()
    {
        IsActivate = true;

        UpdateSpell();

        _timer = Timer.CreateInstance;
        _timer.StartTimer(_lifeTime);

        _timer.Completed += () =>
        {
            IsActivate = false;
            UpdateSpell();
            SpellCompleted?.Invoke();
        };
    }
    protected abstract void UpdateSpell();

}

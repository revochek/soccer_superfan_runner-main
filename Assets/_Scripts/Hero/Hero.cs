using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(HeroStats))]
[RequireComponent(typeof(HeroMovement))]
public class Hero : MonoBehaviour, IBuffable
{    
    [SerializeField] private ParticleSystem _respawnPoofParticle;

    private HeroMovement _movement;
    private HeroStats _currentHeroStats;
    private List<IBuff> _buffs = new List<IBuff>();
    private Vector2 _startPoint;
    //private TableIconCurrentBuffs

    private void Start()
    {
        _startPoint = transform.position;
        _movement = GetComponent<HeroMovement>();
        _currentHeroStats = GetComponent<HeroStats>();    
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < _buffs.Count; i++)
            {
                Debug.Log(_buffs[i]);
            }
        }
    }
    public void ReturnToStartPoint()
    {
        transform.position = _startPoint;
        SoundLoader.Instance.PlaySound(SoundLoader.Instance.Hit);
        StartCoroutine(_movement.StopAndMoveAfterDelay(1.5f));
        _respawnPoofParticle.Play();
    }

    public void AddBuff(IBuff buff)
    {
        _buffs.Add(buff);

        ApplyBuffs();
    }

    public void RemoveBuff(IBuff buff)
    {
        _buffs.Remove(buff);

        ApplyBuffs();
    }

    private void ApplyBuffs()
    {
        _currentHeroStats.ResetToBase();

        foreach (var buff in _buffs)
        {
            _currentHeroStats = buff.ApplyBuff(_currentHeroStats);
        }
    }

}


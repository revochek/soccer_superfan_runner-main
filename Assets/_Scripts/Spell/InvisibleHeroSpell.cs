using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibleHeroSpell : Spell
{
    [SerializeField] private GameObject _modelHero;
    private SpriteRenderer _spriteHero;
    //[SerializeField] private Hero _hero;
    [SerializeField] private GameObject _heroGameObject;

    public override void Initialize(IPersistentData persistentData, IDataProvider dataProvider)
    { 
        base.Initialize(persistentData, dataProvider);
    }

    private void Start()
    {
        _spriteHero = _modelHero.GetComponentInChildren<SpriteRenderer>();
    }

    protected override void UpdateSpell() 
    {
        ChangeHeroLayer(IsActivate); 
        ChangeModelHeroTransparent(IsActivate); 
    }

    public override void Activate()
    {
        base.Activate();
    }

    private void ChangeHeroLayer(bool isSpellAcitave)
    {
        if (isSpellAcitave)
        {
            _heroGameObject.layer = LayerMask.NameToLayer("Ghost");
        }
        else
        {
            _heroGameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

        private void ChangeModelHeroTransparent(bool isTransparent)
    {
        if (isTransparent)
            _spriteHero.color = new Color(_spriteHero.color.r, _spriteHero.color.g, _spriteHero.color.b, 0.5f);
        else
            _spriteHero.color = new Color(_spriteHero.color.r, _spriteHero.color.g, _spriteHero.color.b, 1);
    }


}
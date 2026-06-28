using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class HeroModelLoader : MonoBehaviour
{
    private HeroSkins _currentSelectedSkin;

    [SerializeField] GameObject _smile;
    [SerializeField] GameObject _ball;
    [SerializeField] GameObject _heartEyes;
    [SerializeField] GameObject _king;
    [SerializeField] GameObject _shaggy;


    public void Initialize(HeroSkins heroSkin)
    {
        _currentSelectedSkin = heroSkin;
        InstantiateModel();
    }

    private void InstantiateModel()
    {
        Instantiate(GetLoadedModel(_currentSelectedSkin), transform.position, Quaternion.identity, gameObject.transform);
    }

    private GameObject GetLoadedModel(HeroSkins skinType)
    {
        switch (skinType)
        {
            case HeroSkins.Smile:
                return _smile;
            case HeroSkins.Ball:
                return _ball;
            case HeroSkins.HeartEyes:
                return _heartEyes;
            case HeroSkins.King:
                return _king;
            case HeroSkins.Shaggy:
                return _shaggy;

            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}

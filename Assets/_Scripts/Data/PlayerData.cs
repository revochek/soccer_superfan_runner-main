using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int Level;
    public int Coins;
    public int Stars;
    public int SpellsCount;
    public bool IsAudioOn;

    private HeroSkins _selectedHeroSkin;
    private HashSet<HeroSkins> _openHeroSkins;

    public DateTime LastRewardTime;
    public TimeSpan RewardInterval = TimeSpan.FromHours(1);

    public PlayerData()
    {
        Level = 0;
        Coins = 0;
        Stars = 0;
        SpellsCount = 2;
        IsAudioOn = false;

        _selectedHeroSkin = HeroSkins.Smile;
        _openHeroSkins = new HashSet<HeroSkins>() { _selectedHeroSkin };

        LastRewardTime = DateTime.MinValue;
    }

    public HeroSkins SelectedHeroSkin
    {
        get => _selectedHeroSkin;
        set
        {
            _selectedHeroSkin = value;
        }
    }

    public IEnumerable<HeroSkins> OpenHeroSkins => _openHeroSkins;

    public void OpenHeroSkin(HeroSkins skin)
    {
        if (_openHeroSkins.Contains(skin))
            throw new ArgumentException(nameof(skin));

        _openHeroSkins.Add(skin);
    }
}

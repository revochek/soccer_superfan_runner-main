using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{ 
    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private SoundButton _soundButton;

    public void Initialize(IPersistentData persistentData, IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
        _persistentData = persistentData;
    }

    private void OnEnable()
    {
        _soundButton.Click += OnSoundButtonClicked;
    }

    private void OnDisable()
    {
        _soundButton.Click -= OnSoundButtonClicked;
    }

    private void Start()
    {
        _soundButton.ChangeSoundIcon(_persistentData.PlayerData.IsAudioOn);
        SetAudioState(_persistentData.PlayerData.IsAudioOn);
    }

    private void OnSoundButtonClicked()
    {
        _persistentData.PlayerData.IsAudioOn = !_persistentData.PlayerData.IsAudioOn;
        _dataProvider.Save();
        _soundButton.ChangeSoundIcon(_persistentData.PlayerData.IsAudioOn);

        SetAudioState(_persistentData.PlayerData.IsAudioOn);
    }

    private void SetAudioState(bool isAudioOn)
    {
        if (isAudioOn)
        {
            _audioMixer.SetFloat("MasterVolume", 0f);
        }
        else
        {
            _audioMixer.SetFloat("MasterVolume", -80f); 
        }
    }
}

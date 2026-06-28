using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNumberUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumberText;
    private IPersistentData _persistentData;

    public void Initialize(IPersistentData persistentData)
    {
        _persistentData = persistentData;
    }

    private void Start()
    {
        _levelNumberText.text = "LEVEL " + (_persistentData.PlayerData.Level + 1);
    }
}

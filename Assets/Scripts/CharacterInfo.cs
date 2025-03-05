using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class CharacterInfo
{
    private Enums.RpsState _state = Enums.RpsState.WrongAnswer;
    private UserData _scores;

    public Enums.RpsState State
    {
        get => _state;
        set => _state = value;
    }

    public UserData Scores
    {
        get => _scores;
        set => _scores = value;
    }
    
}
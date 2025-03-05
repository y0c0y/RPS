using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class CharacterInfo
{
    private Enums.RpsState state = Enums.RpsState.WrongAnswer;
    private List<Enums.Score> scores = new();

    public Enums.RpsState State
    {
        get => state;
        set => state = value;
    }

    public List<Enums.Score> Scores
    {
        get => scores;
        set => scores = value;
    }
    
    
}

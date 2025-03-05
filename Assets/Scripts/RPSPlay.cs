using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class RpsPlay
{
    
    public CharacterInfo Player;
    public CharacterInfo Npc;

    public string ScoreString(Enums.Score score)
    {
        // Debug.Log($"player : {player.State}");
        // Debug.Log($"npc : {npc.State}");
        
        switch (score)
        {
            case Enums.Score.Win:
                return TextData.WinString;
            case Enums.Score.Loss:
                return TextData.LoseString;
            case Enums.Score.Draw:
                return TextData.DrawString;
            default:
                return TextData.WrongString;
        }
    }
    
    public void ScoreSave(Enums.Score score)
    {
        switch (score)
        {
            case Enums.Score.Win:
                Player.Scores.Add(Enums.Score.Win);
                Npc.Scores.Add(Enums.Score.Loss);
                break;
            case Enums.Score.Loss:
                Player.Scores.Add(Enums.Score.Loss);
                Npc.Scores.Add(Enums.Score.Win);
                break;
            case Enums.Score.Draw:
                Player.Scores.Add(Enums.Score.Draw);
                Npc.Scores.Add(Enums.Score.Draw);
                break;
            case Enums.Score.WrongAnswer:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(score), score, null);
        }
    }
    
    public Enums.Score CheckResult()
    {
        if(Player.State == Npc.State) return Enums.Score.Draw;
        switch (Player.State)
        {
            case Enums.RpsState.Rock:
                return Npc.State == Enums.RpsState.Scissors ? Enums.Score.Win : Enums.Score.Loss;
            case Enums.RpsState.Paper:
                return Npc.State == Enums.RpsState.Rock ? Enums.Score.Win : Enums.Score.Loss;
            case Enums.RpsState.Scissors :
                return Npc.State == Enums.RpsState.Paper ? Enums.Score.Win : Enums.Score.Loss;
            case Enums.RpsState.WrongAnswer:
            default:
                return Enums.Score.WrongAnswer;
        }

    }
    

}

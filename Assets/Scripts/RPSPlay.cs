using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

[Serializable]
public partial class RPSPlay : MonoBehaviour
{
    
    public CharacterInfo player;
    public CharacterInfo npc;

    public string ScoreString(Enums.Score score)
    {
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
                player.Scores.Add(Enums.Score.Win);
                npc.Scores.Add(Enums.Score.Loss);
                break;
            case Enums.Score.Loss:
                player.Scores.Add(Enums.Score.Loss);
                npc.Scores.Add(Enums.Score.Win);
                break;
            case Enums.Score.Draw:
                player.Scores.Add(Enums.Score.Draw);
                npc.Scores.Add(Enums.Score.Draw);
                break;
            case Enums.Score.WrongAnswer:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(score), score, null);
        }
    }
    
    public Enums.Score CheckResult()
    {
        if(player == npc) return Enums.Score.Draw;
        switch (player.State)
        {
            case Enums.RpsState.Rock:
                return npc.State == Enums.RpsState.Paper ? Enums.Score.Loss : Enums.Score.Win;
            case Enums.RpsState.Paper:
                return npc.State == Enums.RpsState.Scissors ? Enums.Score.Win : Enums.Score.Loss;
            case Enums.RpsState.Scissors :
                return npc.State == Enums.RpsState.Paper ? Enums.Score.Win : Enums.Score.Loss;
            case Enums.RpsState.WrongAnswer:
            default:
                return Enums.Score.WrongAnswer;
        }

    }
    
    public string Play()
    {
        var random = new Random();
        var randomValue = random.Next(0, 2);
        
        npc.State = (Enums.RpsState)randomValue;

        var result = CheckResult();
        
        ScoreSave(result);
        
        return ScoreString(result);
    }
}

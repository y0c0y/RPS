using System;

public class RpsPlay
{
    public CharacterInfo Player;
    public CharacterInfo Npc;

    public static string ScoreString(Enums.Score score)
    {
        return score switch
        {
            Enums.Score.Win => TextData.WinString,
            Enums.Score.Loss => TextData.LoseString,
            Enums.Score.Draw => TextData.DrawString,
            _ => TextData.WrongString
        };
    }
    
    public void ScoreUpdate(Enums.Score score)
    {
        switch (score)
        {
            case Enums.Score.Win:
                Player.Scores[(int)Enums.Score.Win]++;
                // Npc.Scores[(int)Enums.Score.Loss]++;
                break;
            case Enums.Score.Loss:
                Player.Scores[(int)Enums.Score.Loss]++;
                // Npc.Scores[(int)Enums.Score.Win]++;
                break;
            case Enums.Score.Draw:
                Player.Scores[(int)Enums.Score.Draw]++;
                // Npc.Scores[(int)Enums.Score.Draw]++;
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

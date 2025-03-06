using System;

[Serializable]
public class CharacterInfo : UserData
{
    private Enums.RpsState _state = Enums.RpsState.WrongAnswer;
    private UserData _scores = new();

    public Enums.RpsState State
    {
        get => _state;
        set => _state = value;
    }

    public UserData Scores
    {
        get => _scores;
        set{
            if (value != null)
            {
                Array.Copy(value.userScores, _scores.userScores, _scores.userScores.Length);
            }
        }
    }
    
}
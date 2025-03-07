using System;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    
    public DataManager dataManager;
    public RpsPlay RpsPlay;
    
    public delegate void StateChangedHandler (CharacterInfo who, Enums.RpsState state);
    public event StateChangedHandler OnStateChanged;
    public event Action OnGameStart;
    public event Action<string> OnGameResult;
    public event Action<UserData> OnScoreUpdated;
    
    public event Action<bool> OnButtonChanged;
    public event Action OnGameEnd;
    
    
    public void Play()
    {   
        StartCoroutine(Stop());
    }

    public void Quit()
    {
        dataManager.JsonSave(RpsPlay.Player.Scores);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    private string OneTime()
    {
        var random = new Random();
        var randomValue = (Enums.RpsState)random.Next(0, 3);

        // var randomValue = (Enums.RpsState)2;
        
        OnStateChanged?.Invoke(RpsPlay.Npc,randomValue);
        
        var result = RpsPlay.CheckResult();
        
        RpsPlay.ScoreUpdate(result);
        
        return RpsPlay.ScoreString(result);
    }

    private IEnumerator Count()
    {
        yield return new WaitForSeconds(0.5f);
        for (var i = 0; i < 3; i++)
        {
            OnGameResult?.Invoke(TextData.NpcString[i]);
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator Stop()
    {
        
        OnGameStart?.Invoke();
        
        yield return Count(); //선택 시간
        
        OnButtonChanged?.Invoke(false); // 버튼 삭제
        var tmp = OneTime(); // 한 게임 과정
        
        OnGameResult?.Invoke(tmp); // 결과 출력
        
        yield return new WaitForSeconds(2.0f); //결과 확인 시간
        
        OnGameEnd?.Invoke();
        
        OnScoreUpdated?.Invoke(RpsPlay.Player.Scores);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Start()
    {
        RpsPlay = new RpsPlay
        {
            Npc = new CharacterInfo(),
            Player = new CharacterInfo
            {
                Scores = dataManager.JsonLoad()
            }
        };

        OnScoreUpdated?.Invoke(RpsPlay.Player.Scores);
        OnGameEnd?.Invoke();
    }
}
using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public DataManager dataManager;
    public UIManager uiManager;
    public RpsPlay RpsPlay;

    private string OneTime()
    {
        var random = new Random();
        var randomValue = (Enums.RpsState)random.Next(0, 2);

        // var randomValue = Enums.RpsState.Rock;
        
        RpsPlay.Npc.State = randomValue;
        uiManager.npcText.text = uiManager.FindHand(randomValue);

        var result = RpsPlay.CheckResult();
        
        RpsPlay.ScoreUpdate(result);
        
        return RpsPlay.ScoreString(result);
    }

    IEnumerator Count(int count)
    {
        for (var i = count; i > 0; i--)
        {
            uiManager.resultText.text = i.ToString();
            yield return new WaitForSeconds(i * 1.0f);
        }
        yield return new WaitForSeconds(1.0f);
    }
    
    IEnumerator Stop()
    {
        uiManager.SetDuringGameCanvas(); //게임 화면 셋팅
        
        yield return Count(3); //선택 시간
        
        uiManager.playerButtonCanvasOnOff(false); // 버튼 삭제

        uiManager.resultText.text = TextData.NpcString;
        
        yield return new WaitForSeconds(1.0f); //결과 확인 시간
        
        var tmp = OneTime(); // 한 게임 과정
        
        uiManager.SetResultText(tmp); // 결과 출력
        
        yield return new WaitForSeconds(2.0f); //결과 확인 시간
        
        uiManager.SetStartCanvas(); // 다시 로비 화면
        
        uiManager.UpdateRecordText(RpsPlay.Player.Scores);
    }

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
    
    void Start()
    {
        RpsPlay = new RpsPlay
        {
            Npc = new CharacterInfo(),
            Player = new CharacterInfo()
        };
        
        RpsPlay.Player.Scores = dataManager.JsonLoad();

        
        Debug.Log(string.Join(", ", RpsPlay.Player.Scores.userScores));
        uiManager.UpdateRecordText(RpsPlay.Player.Scores);
        uiManager.SetStartCanvas();
       
        // Debug.Log(rpsPlay.player.State);
    }
}

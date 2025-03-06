using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

        uiManager.SetNpcChoice(randomValue);
        
        var result = RpsPlay.CheckResult();
        
        RpsPlay.ScoreUpdate(result);
        
        return RpsPlay.ScoreString(result);
    }

    IEnumerator Count(int count)
    {
        yield return new WaitForSeconds(0.5f);
        for (var i = 0; i < 3; i++)
        {
            uiManager.resultText.text = TextData.NpcString[i];
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.5f);
    }
    
    IEnumerator Stop()
    {
        uiManager.SetDuringGameCanvas(); //게임 화면 셋팅
        
        yield return Count(3); //선택 시간
        
        uiManager.playerButtonCanvasOnOff(false); // 버튼 삭제
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
        
        // uiManager.npcImage = GetComponent<RawImage>();
        // uiManager.playerImage = GetComponent<RawImage>();
        
        RpsPlay.Player.Scores = dataManager.JsonLoad();
        
        // Debug.Log(string.Join(", ", RpsPlay.Player.Scores.userScores));
        uiManager.UpdateRecordText(RpsPlay.Player.Scores);
        uiManager.SetStartCanvas();
       
        // Debug.Log(rpsPlay.player.State);
    }
}

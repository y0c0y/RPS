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
        
        // UserData tmp = RpsPlay.ScoreUpdate(result, RpsPlay.Player.Scores);
        // dataManager.JsonSave(tmp);
        
        return RpsPlay.ScoreString(result);
    }
    
    IEnumerator Stop()
    {

        uiManager.SetDuringGameCanvas();
        for (var i = 3; i >= 0; i--)
        {
            uiManager.npcText.text = i.ToString();
            yield return new WaitForSeconds(i * 1.0f);
        }
        
        uiManager.playerButtonCanvasOnOff(false);
        yield return new WaitForSeconds(1.0f);
        var tmp = OneTime();
        uiManager.SetResultText(tmp);
        yield return new WaitForSeconds(1.0f);
        uiManager.SetStartCanvas();
    }

    public void Play()
    {
        // RpsPlay.Player.Scores = dataManager.JsonLoad();
        StartCoroutine(Stop());
    }
    
    void Start()
    {
        RpsPlay = new RpsPlay
        {
            Npc = new CharacterInfo(),
            Player = new CharacterInfo()
        };

        uiManager.SetStartCanvas();
       
        // Debug.Log(rpsPlay.player.State);
        
        
    }
}

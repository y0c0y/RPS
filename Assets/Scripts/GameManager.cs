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
        
        RpsPlay.ScoreSave(result);
        
        return RpsPlay.ScoreString(result);
    }
    
    IEnumerator Stop()
    {
        
        uiManager.lobbyCanvasOnOff(false);
        uiManager.PlayTextCanvasOnOff(true);
        uiManager.playerButtonCanvasOnOff(true);
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
        uiManager.playerButtonCanvasOnOff(false);
        uiManager.PlayTextCanvasOnOff(false);
        uiManager.lobbyCanvasOnOff(true);
    }

    public void Play()
    {
        
        StartCoroutine(Stop());
    }
    
    void Start()
    {
        RpsPlay = new RpsPlay();
        RpsPlay.Npc = new CharacterInfo();
        RpsPlay.Player = new CharacterInfo();
      

       uiManager.playerButtonCanvasOnOff(false);
       uiManager.PlayTextCanvasOnOff(false);
       uiManager.lobbyCanvasOnOff(true);
        // Debug.Log(rpsPlay.player.State);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

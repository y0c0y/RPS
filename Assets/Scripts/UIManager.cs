using System;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameManager gameManager;
    
    public Text npcText;
    public Text playerText;
    public Text resultText;
    
    public Canvas playerButtonCanvas;
    public Canvas lobbyCanvas;
    // public Canvas resultCanvas;
    public Canvas playTextCanvas;
    public string FindHand(Enums.RpsState state)
    {
        switch (state)
        {
            case Enums.RpsState.Rock:
                return TextData.Rock;
            case Enums.RpsState.Paper:
                return TextData.Paper;
            case Enums.RpsState.Scissors:
                return TextData.Scissors;
            default:
                return TextData.WrongString;
        }
    }

    public void OnClickPlayButton()
    {
        Debug.Log("OnClickPlayButton");
        gameManager.Play();
    }

    public void OnClickQuitButton()
    {
        Debug.Log("OnClickQuitButton");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
    public void OnClickRock()
    {
        playerText.text = TextData.Rock;
        gameManager.RpsPlay.Player.State = Enums.RpsState.Rock;
        // Debug.Log(gameManager.rspPlay.player.State);
    }

    public void OnClickPaper()
    {
        playerText.text = TextData.Paper;
        gameManager.RpsPlay.Player.State = Enums.RpsState.Paper;
        // Debug.Log(gameManager.rspPlay.player.State);
    }

    public void OnClickScissors()
    {
        playerText.text = TextData.Scissors;
        gameManager.RpsPlay.Player.State = Enums.RpsState.Scissors;
        // Debug.Log(gameManager.rspPlay.player.State);
    }

    public void SetResultText(string text)
    {
        resultText.text = text;
    }

    public void playerButtonCanvasOnOff(bool isOn)
    {
        playerButtonCanvas.enabled = isOn;
        Debug.Log(playerButtonCanvas.enabled);

        
    }
    public void lobbyCanvasOnOff(bool isOn)
    {
        // Debug.Log(isOn);
        lobbyCanvas.enabled = isOn;
    }
    public void PlayTextCanvasOnOff(bool isOn)
    {
        // Debug.Log(isOn);
        npcText.text = "";
        playerText.text = "";
        resultText.text = "";
        playTextCanvas.enabled = isOn;
        
    }
}

using System;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameManager gameManager;
    
    public Text npcText;
    public Text playerText;
    public Text resultText;
    public Text recordText;
    
    public Canvas playerButtonCanvas;
    public Canvas lobbyCanvas;
    // public Canvas resultCanvas;
    public Canvas playTextCanvas;
    
    public Texture rockTexture;
    public Texture paperTexture;
    public Texture scissorsTexture;
    
    public Transform npcTransform;
    public Transform playerTransform;

    public RawImage npcImage;
    public RawImage playerImage;


    public void ResetImage()
    {
        npcImage.texture = null;
        playerImage.texture = null;

        npcImage.enabled = false;
        playerImage.enabled = false;
    }
    
    
    public void SetImage(RawImage image, Enums.RpsState state)
    {
        if (!image) { image = GetComponent<RawImage>(); }
        if(!image.enabled) { image.enabled = true; }

        switch (state)
        {
            case Enums.RpsState.Rock:
                image.texture = rockTexture;
                break;
            case Enums.RpsState.Paper:
                image.texture = paperTexture;
                break;
            case Enums.RpsState.Scissors:
                image.texture = scissorsTexture;
                break;
        }
        
        // Debug.Log(image.texture);
    }

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
        // Debug.Log("OnClickPlayButton");

       
        gameManager.Play();
    }

    public void UpdateRecordText(UserData userData)
    {
        var tmpScore = userData.userScores;
        recordText.text = $"Win : {tmpScore[0]} Loss : {tmpScore[1]}, Draw : {tmpScore[2]}";

    }

    public void OnClickQuitButton()
    {
        // Debug.Log("OnClickQuitButton");
        gameManager.Quit();
        
    }
    
    public void OnClickRock()
    {
        SetPlayerChoice(Enums.RpsState.Rock);
    }

    public void OnClickPaper()
    {
        SetPlayerChoice(Enums.RpsState.Paper);
    }

    public void OnClickScissors()
    {
        SetPlayerChoice(Enums.RpsState.Scissors);
    }
    
    public void SetNpcChoice(Enums.RpsState state)
    {
        SetImage(npcImage,state);
        gameManager.RpsPlay.Npc.State = state;
    }


    private void SetPlayerChoice(Enums.RpsState state)
    {
        SetImage(playerImage,state);
        gameManager.RpsPlay.Player.State = state;
    }

    public void SetResultText(string text)
    {
        resultText.text = text;
    }

    public void playerButtonCanvasOnOff(bool isOn)
    {
        playerButtonCanvas.enabled = isOn;
        // Debug.Log(playerButtonCanvas.enabled);

        
    }
    public void lobbyCanvasOnOff(bool isOn)
    {
        // Debug.Log(isOn);
        lobbyCanvas.enabled = isOn;
    }
    public void PlayTextCanvasOnOff(bool isOn)
    {
        resultText.text = "";
        playTextCanvas.enabled = isOn;
    }

    public void SetStartCanvas()
    {
        if(playerImage && npcImage) ResetImage();
        
        playerButtonCanvasOnOff(false);
        PlayTextCanvasOnOff(false);
        lobbyCanvasOnOff(true);
    }
    
    public void SetDuringGameCanvas()
    {
        lobbyCanvasOnOff(false);
        PlayTextCanvasOnOff(true);
        playerButtonCanvasOnOff(true);
    }
}

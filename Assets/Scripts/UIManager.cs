using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameManager gameManager;
    
    public Text resultText;
    public Text recordText;
    
    public Canvas playerButtonCanvas;
    public Canvas lobbyCanvas;
    public Canvas playTextCanvas;
    
    public Texture rockTexture;
    public Texture paperTexture;
    public Texture scissorsTexture;
    
    public RawImage npcImage;
    public RawImage playerImage;


    private void ResetImage()
    {
        npcImage.texture = null;
        playerImage.texture = null;

        npcImage.enabled = false;
        playerImage.enabled = false;
    }


    private void SetImage(RawImage image, Enums.RpsState state)
    {
        if (!image) { image = GetComponent<RawImage>(); }
        if(!image.enabled) { image.enabled = true; }

        image.texture = state switch
        {
            Enums.RpsState.Rock => rockTexture,
            Enums.RpsState.Paper => paperTexture,
            Enums.RpsState.Scissors => scissorsTexture,
            _ => image.texture
        };
    }

    public void OnClickPlayButton()
    {
        gameManager.Play();
    }

    public void UpdateRecordText(UserData userData)
    {
        var tmpScore = userData.userScores;
        recordText.text = $"Win : {tmpScore[0]} Loss : {tmpScore[1]}, Draw : {tmpScore[2]}";

    }

    public void OnClickQuitButton()
    {
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
    }

    private void lobbyCanvasOnOff(bool isOn)
    {
        lobbyCanvas.enabled = isOn;
    }

    private void PlayTextCanvasOnOff(bool isOn)
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

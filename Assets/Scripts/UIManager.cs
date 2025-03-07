using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager Instance { get; set; }
    
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


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void OnClickPlayButton()
    {
        GameManager.Instance.Play();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += Choice;
        GameManager.Instance.OnScoreUpdated += UpdateRecordText;
        GameManager.Instance.OnGameStart += SetDuringGameCanvas; 
        GameManager.Instance.OnGameResult += SetResultText;
        GameManager.Instance.OnGameEnd += SetStartCanvas;
        GameManager.Instance.OnButtonChanged += playerButtonCanvasOnOff;
        GameManager.Instance.OnButtonChanged += lobbyCanvasOnOff;
            
        SetStartCanvas();
    }

    private void UpdateRecordText(UserData userData)
    {
        var tmpScore = userData.userScores;
        recordText.text = $"Win : {tmpScore[0]} Loss : {tmpScore[1]}, Draw : {tmpScore[2]}";

    }

    public void OnClickQuitButton()
    {
         GameManager.Instance.Quit();
    }
    
    public void OnClickRock()
    {
        Choice( GameManager.Instance.RpsPlay.Player,Enums.RpsState.Rock);
    }

    public void OnClickPaper()
    {
        Choice( GameManager.Instance.RpsPlay.Player,Enums.RpsState.Paper);
    }

    public void OnClickScissors()
    {
        Choice( GameManager.Instance.RpsPlay.Player,Enums.RpsState.Scissors);
    }

    private void Choice(CharacterInfo who, Enums.RpsState state)
    {
        RawImage image = who ==  GameManager.Instance.RpsPlay.Player ? playerImage : npcImage;
        SetImage(image,state);
        who.State = state;
    }

    private void SetResultText(string text)
    {
        resultText.text = text;
    }

    private void playerButtonCanvasOnOff(bool isOn)
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

    private void SetStartCanvas()
    {
        if(playerImage && npcImage) ResetImage();
        
        playerButtonCanvasOnOff(false);
        PlayTextCanvasOnOff(false);
        lobbyCanvasOnOff(true);
    }

    private void SetDuringGameCanvas()
    {
        lobbyCanvasOnOff(false);
        PlayTextCanvasOnOff(true);
        playerButtonCanvasOnOff(true);
    }
    
    private void ResetImage()
    {
        npcImage.texture = null;
        playerImage.texture = null;

        npcImage.enabled = false;
        playerImage.enabled = false;
    }


    private void SetImage(RawImage image, Enums.RpsState state)
    {
        // if (!image) { image = GetComponent<RawImage>(); }
        if(!image.enabled) { image.enabled = true; }

        image.texture = state switch
        {
            Enums.RpsState.Rock => rockTexture,
            Enums.RpsState.Paper => paperTexture,
            Enums.RpsState.Scissors => scissorsTexture,
            _ => image.texture
        };
    }

}
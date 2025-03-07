using UnityEngine;
using System.Collections;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
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

    private IEnumerator Count()
    {
        yield return new WaitForSeconds(0.5f);
        for (var i = 0; i < 3; i++)
        {
            uiManager.resultText.text = TextData.NpcString[i];
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator Stop()
    {
        uiManager.SetDuringGameCanvas(); //게임 화면 셋팅
        
        yield return Count(); //선택 시간
        
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

        uiManager.UpdateRecordText(RpsPlay.Player.Scores);
        uiManager.SetStartCanvas();
    }
}

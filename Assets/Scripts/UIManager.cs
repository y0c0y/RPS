using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text npcText;
    public Text playerText;
    public Text scoreText;

    // public void setUIComponent()
    // {
    //     npcText = GetComponent<Text>();
    //     playerText = GetComponent<Text>();
    //     scoreText = GetComponent<Text>();
    // }
    public void OnClickRock()
    {
        Debug.Log("click rock");
        
        playerText.text = TextData.Rock;
    }

    public void OnClickPaper()
    {
        Debug.Log("click paper");
        playerText.text = TextData.Paper;
    }

    public void OnClickScissors()
    {
        Debug.Log("click scissors");
        playerText.text = TextData.Scissors;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text NPCText;
    public Text PlayerText;
    public Text ScoreText;

    public void OnClickRock()
    {
        Debug.Log("click rock");
    }

    public void OnClickPaper()
    {
        Debug.Log("click paper");
    }

    public void OnClickScissors()
    {
        Debug.Log("click scissors");
    }
}

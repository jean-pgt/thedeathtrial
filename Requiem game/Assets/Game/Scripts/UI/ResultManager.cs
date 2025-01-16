using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NewEmptyCSharpScript : MonoBehaviour
{
    public GameObject resultPanel;
    public Text resultText;

    public FightingController[] fightingController;
    public OpponentAI[] opponentAI;

    void Update()
    {
        foreach (FightingController fightingController in fightingController)
        {
            if (fightingController.gameObject.activeSelf && fightingController.currenthealth <= 0)
            {
                SetResult("You Lose!");
                return;
            }
        }

        foreach (OpponentAI opponentAI in opponentAI)
        {
            if (opponentAI.gameObject.activeSelf && opponentAI.currenthealth <= 0)
            {
                SetResult("You Win!");
                return;
            }
        }
    }
    void SetResult(string result)
    {
        resultText.text = result;
        resultPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

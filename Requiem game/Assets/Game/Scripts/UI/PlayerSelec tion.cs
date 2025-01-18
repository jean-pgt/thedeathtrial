using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSelection : MonoBehaviour
{
    public GameObject playerCharacters;
    private GameObject[] allCharacters;
    private int currentIndex = 0;

    private void Start()
    {
        allCharacters = new GameObject[playerCharacters.transform.childCount];

        for (int i = 0; i < playerCharacters.transform.childCount; i++)
        {
            allCharacters[i] = playerCharacters.transform.GetChild(i).gameObject;
            allCharacters[i].SetActive(false);
        }

        if (PlayerPrefs.HasKey("SelectedCharacterIndex"))
        {
            currentIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");
        }

        ShowcurrentCharacter();
    }
    void ShowcurrentCharacter()
    {
        foreach (GameObject character in allCharacters)
        {
            character.SetActive(false);
        }

        allCharacters[currentIndex].SetActive(true);
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % allCharacters.Length;
        ShowcurrentCharacter();
    }

    public void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + allCharacters.Length) % allCharacters.Length;
        ShowcurrentCharacter();
    }

    public void OnYesButtonClick(string sceneName)
    {
        PlayerPrefs.SetInt("SelectedCharacterindex" , currentIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName);
    }

}

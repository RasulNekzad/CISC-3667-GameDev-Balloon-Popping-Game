using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInput;

    public void PlayGame()
    {
        string s = playerNameInput.text;
        PersistentData.Instance.setName(s);
        SceneManager.LoadScene("Level 1");
    }

    public void GoToInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GoToHighScores()
    {
        SceneManager.LoadScene("High Scores");
    }
}

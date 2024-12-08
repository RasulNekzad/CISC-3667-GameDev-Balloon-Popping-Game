using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int numBalloons;
    private int currentSceneIndex;

    void Start()
    {
        numBalloons = FindObjectsOfType<BalloonMovement>().Length;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    public void BalloonPopped()
    {
        numBalloons--;
        if (numBalloons <= 0) {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(currentSceneIndex + 1);
        } else {
            Debug.Log("All levels completed.");
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene(currentSceneIndex);
        PersistentData.Instance.setScore(0);
    }
}

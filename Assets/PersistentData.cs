using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setName(string n)
    {
        if (string.IsNullOrEmpty(n))
        {
            n = "Anonymous";
        }
        
        PlayerPrefs.SetString("playerName", n);
        PlayerPrefs.Save();
    }

    public string getName()
    {
        return PlayerPrefs.GetString("playerName", "");
    }

    public void setScore(int s)
    {
        PlayerPrefs.SetInt("playerScore", s);
        PlayerPrefs.Save();
    }

    public int getScore()
    {
        return PlayerPrefs.GetInt("playerScore", 0);
    }
}

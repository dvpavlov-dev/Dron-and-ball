using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int CurrentLevelID;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }
    }

    public void ChangeLevel()
    {
        CurrentLevelID++;
        SceneManager.LoadScene($"Level {CurrentLevelID}");
    }

    public void ChangeLevel(int levelID)
    {
        CurrentLevelID = levelID;
        SceneManager.LoadScene($"Level {CurrentLevelID}");
    }
    
    public bool IsLevelsEnd()
    {
        return SceneManager.sceneCountInBuildSettings - 3 < CurrentLevelID;
    }

    public void BackToMenu()
    {
        CurrentLevelID = 0;
        SceneManager.LoadScene("StartScene");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }
}

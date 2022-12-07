using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Win()
    {
        if (!LevelManager.Instance.IsLevelsEnd())
        {
            LevelManager.Instance.ChangeLevel();
        }
        else
        {
            LevelManager.Instance.EndGame();
        }
    }
}

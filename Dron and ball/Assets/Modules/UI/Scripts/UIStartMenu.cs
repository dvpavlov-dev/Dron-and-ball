public class UIStartMenu : UIController
{
    #region Start menu
    public void StartGame()
    {
        LevelManager.Instance.ChangeLevel();
    }

    public void OpenLevels()
    {
        ChangeTab("UI Levels menu [17:13]");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion

    #region Levels menu

    public void StartLevel(int idLevel)
    {
        LevelManager.Instance.ChangeLevel(idLevel);
    }

    public void BackToStartMenu()
    {
        ChangeTab("UI Start menu [4:4]");
    }

    #endregion
}

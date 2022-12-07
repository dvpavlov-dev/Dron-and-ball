public class UIMainMenu : UIController
{
    #region Gameplay

    public void OpenPauseMenu()
    {
        ChangeTab("UI Pause menu [29:13]");
    }

    public void OpenTutorial()
    {
        ChangeTab("UI Tutorial EN [31:28]");
    }

    #endregion

    #region Pause menu

    public void Continue()
    {
        ChangeTab("UI Gameplay [28:3]");
    }

    public void Restart()
    {
        LevelManager.Instance.ChangeLevel(LevelManager.Instance.CurrentLevelID);
    }

    public void BackToMenu()
    {
        LevelManager.Instance.BackToMenu();
    }

    #endregion

    #region Tutorial

    public void BackToGameplay()
    {
        ChangeTab("UI Gameplay [28:3]");
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndScene : UIController
{
    public void BackToMenu()
    {
        LevelManager.Instance.BackToMenu();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickBtn : MonoBehaviour {

    public void OnClickGo() {
        GameManager.Instance.LaunchTrain();
    }

    public void OnClickMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void OnClickRestartLevel()
    {
        GameManager.Instance.ReloadCurrentScene();
    }

    public void OnClickNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }
}

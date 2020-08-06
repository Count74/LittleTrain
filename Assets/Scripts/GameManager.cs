using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    string levelName = "level1";
    string mainMenuName = "main_menu";
    string creditsScene = "main_menu";
    int levelNumber = 0;
    int levelProgress;
    int amountOfLevels = 24;
    bool freeCamera = true;


    bool lockCamera = false;
    TrainController train;
    bool isBroken;
    bool isComplete;
    UIManager ui;


    internal void SetUI(UIManager uIManager)
    {
        ui = uIManager;
    }

    internal int GetLevelNumber()
    {
        return levelNumber;
    }

    internal void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        levelName = mainMenuName;
        levelNumber = 0;
        freeCamera = true;
        isBroken = false;
    }

    internal void LoadNextLevel()
    {
        levelNumber++;
        if (levelNumber > amountOfLevels)
        {
            SceneManager.LoadScene(creditsScene);
        }
        else
        {
            levelName = "level" + levelNumber;
            SceneManager.LoadScene(levelName);
            freeCamera = true;
            isBroken = false;
        }
    }

    private void Awake()
    {
       levelProgress = PlayerPrefs.GetInt("LevelProgress");
       SetLevel();
    }

    private void SetLevel()
    {
        train = GameObject.FindGameObjectWithTag("Train").GetComponent<TrainController>();
        if (GameObject.FindGameObjectWithTag("UI") != null )
            ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        isBroken = false;
        freeCamera = true;
        isComplete = false;
    }

    public void LevelComplete() {
        if (isBroken || isComplete)
            return;
        isComplete = true;
        freeCamera = true;
        StartCoroutine(WinMenu());

        int nextLevel = levelNumber + 1;
        if (nextLevel < amountOfLevels && nextLevel > levelProgress)
        {
            levelProgress = nextLevel;
            PlayerPrefs.SetInt("LevelProgress", levelProgress);
            PlayerPrefs.Save();
        }
     }

    IEnumerator WinMenu()
    {
        yield return new WaitForSeconds(1.0f);
        ui.StartWinMenu();
    }

    public void TrainIsBroken()
    {
        if (isBroken)
            return;

        isBroken = true;
        StartCoroutine(LostTrain());
    }

    IEnumerator LostTrain()
    {
        yield return new WaitForSeconds(1.0f);
        train.Stop();
        ui.restartBtn.gameObject.SetActive(true);
        ui.restartBtn.enabled = true;
        freeCamera = true;
    }

    public bool IsCameraFree()
    {
        return freeCamera;
    }

    public void SetLockCamera(bool isLock)
    {
        lockCamera = isLock;
    }

    public bool IsCameraLock()
    {
        return lockCamera;
    }

    internal void CameraToTrain()
    {
        freeCamera = false;
    }

    public void LaunchTrain() {
        SetLevel();

        train.StartSmoke();
        train.StartEngine();
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(levelName);
        freeCamera = true;
        isBroken = false;

        //AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        //if (ao == null)
        //{
        //    Debug.LogError("[GameManager] Unable to load level " + levelName);
        //    return;
        //}

    }

    public TrainController getTrain()
    {
        return train;
    }

    internal void LoadScene(int number)
    {
        levelName = "level" + number;
        levelNumber = number;
        SceneManager.LoadScene(levelName);
        freeCamera = true;
        isBroken = false;
    }

    public GameObject getLoco()
    {
        return train.getLoco();
    }
}

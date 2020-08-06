using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Button restartBtn;
    public Text LevelText;
    public Canvas regularMenu;
    public Canvas winMenu;

	// Use this for initialization
	void Start () {
        GameManager.Instance.SetUI(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void StartWinMenu()
    {
        regularMenu.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(true);
    }
}

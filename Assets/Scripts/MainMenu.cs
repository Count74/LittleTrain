using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public int amountOfLevels = 24;
    public int levelProgress = 2;
    public int btnInRow = 6;
    

    public int xStart = -300;
    public int yStart = -42;
    public int xOffset = 120;
    public int yOffset = 70;

    public GameObject button;
    public GameObject lockedButton;

	// Use this for initialization
	void Start () {
        levelProgress = PlayerPrefs.GetInt("LevelProgress");
        Debug.Log(levelProgress);
        if (levelProgress < 1)
            levelProgress = 1;
        DrawMenu();
	}

    private void DrawMenu()
    {
        int levelNum = 1;
        int row = 0;
        Vector3 startDrawPos = new Vector3(xStart, yStart, 0) + this.transform.position;  // привязываем позицию к конвасу 
        Vector3 currentDrawPos;
        while(levelNum <= amountOfLevels)
        {
            for (int i = 0; i < btnInRow; i++)
            {
                if (levelNum  > amountOfLevels)
                    break;

                currentDrawPos = new Vector3(startDrawPos.x + xOffset * i, startDrawPos.y - yOffset * row, 0);
                if (levelNum <= levelProgress)
                {
                    GameObject objBtn = Instantiate(button, currentDrawPos, Quaternion.identity, this.transform);
                    objBtn.transform.position = currentDrawPos;
                    Text textObj = objBtn.gameObject.GetComponentInChildren<Text>();
                    textObj.text = levelNum.ToString();
                    // отслеживаем нажатие
                    objBtn.gameObject.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(textObj));
                }
                else
                {
                    //кнопки под замком
                    GameObject objBtn = Instantiate(lockedButton, currentDrawPos, Quaternion.identity, this.transform);
                    objBtn.transform.position = currentDrawPos;
                }

                levelNum++;                    
            }
            row++;
        }
    }

    void ButtonClicked(Text buttonText)
    {
        //Output this to console when the Button3 is clicked
        Debug.Log("Load level  = " + buttonText.text);
        GameManager.Instance.LoadScene(Convert.ToInt32(buttonText.text));
    }
}

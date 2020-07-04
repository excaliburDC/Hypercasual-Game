using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelSelectButton;
    public Transform buttonParent;
    public List<LevelData> levelList;

    private void Start()
    {
        Delete();
        FillList();
    }

    private void FillList()
    {
        foreach (var level in levelList)
        {

            GameObject newbutton = Instantiate(levelSelectButton);
            LevelButton button = newbutton.GetComponent<LevelButton>();

            button.levelText.text = level.levelText;

            if (PlayerPrefs.GetInt("Level" + button.levelText.text) == 1)
            {
                level.unLock = 1;
                level.isInteractible = true;
            }

            

            button.levelUnlockint = level.unLock;
            button.GetComponent<Button>().interactable = level.isInteractible;
            button.levelLockedImg.enabled = (level.unLock != 1) ? true : false;
            // button.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Level" + button.LevelText.text));
            //button.GetComponent<Button>().onClick.AddListener(() => StarCor("Level" + button.LevelText.text));



            newbutton.transform.SetParent(buttonParent);
        }
       // SaveLevelData();
    }

    private void SaveLevelData()
    {
        GameObject[] allbuttons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject buttons in allbuttons)
        {
            LevelButton button = buttons.GetComponent<LevelButton>();
            PlayerPrefs.SetInt("Level" + button.levelText.text, button.levelUnlockint);
        }
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
    }
}

[System.Serializable]
public class LevelData
{

    public string levelText;
    public int unLock;
    public bool isInteractible;

    public Button.ButtonClickedEvent OnClick;

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MenuManager : SingletonManager<MenuManager>
{
    //public event Action<int> OnUpdateLivesText;

    public Menus m_startMenu;
    public TextMeshProUGUI livesText;

    public static bool isGameOver;

    [SerializeField] private Menus m_pauseMenu;
    [SerializeField] private Menus m_Hud;
    [SerializeField] private Menus m_GameOverMenu;

    [SerializeField]
    private Component[] menus = new Component[0];


    private Menus previousMenu;
    public Menus PreviousMenu { get { return previousMenu; } }

    private Menus currentMenu;
    public Menus CurrentMenu { get { return currentMenu; } }

    private void OnEnable()
    {

        //the bool parameter decides whether to include inactive gameobjects or not
        menus = GetComponentsInChildren<Menus>(true);
    }

    private void Start()
    {
        if (m_startMenu)
        {
            SwitchMenus(m_startMenu);
        }
       
            

    }


    private void Update()
    {
        //for testing purpose only
        if(currentMenu==m_Hud && Input.GetKeyDown(KeyCode.Escape))
        {
           
            PauseMenu();
        }

        if (currentMenu == m_Hud)
        {
            Test.Instance.OnLiveLost += UpdateLivesText;
        }
    }

    public void UpdateLivesText(int livesLeft)
    {
        livesText.text = "Lives : " + livesLeft.ToString();

        if(livesLeft<0)
        {
            
            GameOver();
        }
    }

    public void SwitchMenus(Menus menu)
    {
        if (menu)
        {
            if (currentMenu)
            {
                currentMenu.CloseMenu();
                previousMenu = currentMenu;
            }
            currentMenu = menu;
           // currentMenu.gameObject.SetActive(true);
            currentMenu.ActivateMenu();



        }
    }

    

    public void SwitchToPreviousMenu()
    {
        if (previousMenu)
        {
            SwitchMenus(previousMenu);
        }
    }

    public void SwitchToHUD()
    {
        if(m_Hud)
        {
            SwitchMenus(m_Hud);
            Time.timeScale = 1f;
        
        }
    }

    public void PauseMenu()
    {
        if(m_pauseMenu)
        {
            Time.timeScale = 0f;
            AudioManager.Instance.Pause("MenuSound");
            SwitchMenus(m_pauseMenu);
        }
       
    }

    public void GameOver()
    {
        isGameOver = true;
        SwitchMenus(m_GameOverMenu);
        Time.timeScale = 0f;
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.Play("ButtonSound");
    }

    

    public void QuitGame()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(WaitToLoadScene(sceneIndex));

    }

    private IEnumerator WaitToLoadScene(int index)
    {
        yield return null;
    }
}

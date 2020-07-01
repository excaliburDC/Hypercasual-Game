using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Menus m_startMenu;

    //for testing purpose
    [SerializeField] public Menus m_pauseMenu;

    [SerializeField]
    private Component[] menus = new Component[0];


    public delegate void SwitchDelegate();
    public event SwitchDelegate onSwitchedMenu;

    private Menus previousMenu;
    public Menus PreviousMenu { get { return previousMenu; } }

    private Menus currentMenu;
    public Menus CurrentMenu { get { return currentMenu; } }

    private void Awake()
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchMenus(m_pauseMenu);
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

            if (onSwitchedMenu != null)
            {
                Debug.Log("Event Fired");
                // onSwitchedMenu += new SwitchDelegate();
            }
        }
    }

    public void SwitchToPreviousMenu()
    {
        if (previousMenu)
        {
            SwitchMenus(previousMenu);
        }
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

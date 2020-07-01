using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menus : MonoBehaviour
{
    public Selectable m_startSelectable;

    public delegate void ActivateMenuDelegate();
    public delegate void CloseMenuDelegate();

    public event ActivateMenuDelegate onActivateMenu;
    public event CloseMenuDelegate onCloseMenu;

   // private Animator anim;

    private void Awake()
    {
       // anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (m_startSelectable)
        {
            EventSystem.current.SetSelectedGameObject(m_startSelectable.gameObject);
        }
    }

    public virtual void ActivateMenu()
    {
        if (onActivateMenu != null)
        {
            //fire activate menu event
        }
        gameObject.SetActive(true);
       // HandleAnimator("Show");
    }

    public virtual void CloseMenu()
    {
        if (onCloseMenu != null)
        {
            //fire close menu event
        }
        gameObject.SetActive(false);
       // HandleAnimator("Hide");
    }

    //private void HandleAnimator(string animTrigger)
    //{
    //    if (anim)
    //    {
    //        anim.SetTrigger(animTrigger);
    //    }
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : SingletonManager<Test>
{
    public event Action<int> OnLiveLost;

    public static int lives;

    // Start is called before the first frame update
    void OnEnable()
    {
        lives = 3;
    }

    private void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(OnLiveLost!=null)
            {
                --lives;
                OnLiveLost(lives);
            }
            
        }
    }
}

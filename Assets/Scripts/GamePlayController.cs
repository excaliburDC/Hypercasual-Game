using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamePlayController : MonoBehaviour
{
    public static  GamePlayController instance;

    public ItemSpawner itemSpawner;

    [HideInInspector]
    public ItemScript currentItem;

    public CameraFollow cameraScript;
    private int moveCount;
  
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
  
    void Start()
    {
        itemSpawner.SpawnItem();
    }
 
    public void spawnNewItem()
    {
        Invoke("NewItem", 2f);
    }
    void NewItem()
    {
        itemSpawner.SpawnItem();
    }
    public void MoveCamera()
    {

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
}

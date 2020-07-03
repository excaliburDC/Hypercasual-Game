using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamePlayController : MonoBehaviour
{
    public static  GamePlayController instance;

    public ItemSpawner itemSpawner;
    public CameraFollow cameraScript;

    [HideInInspector]
    public ItemScript currentItem;
   
    //[HideInInspector]
    //public  bool GameComplete = false;

    //for height check
    public GameObject Ruler;
    public GameObject GameCompleteObj;

    public float heightToComplete;


    void Awake()
    {
        PlayerPrefs.SetFloat("HeightVal", -4.5f);
        PlayerPrefs.SetFloat("RulerHeight", 0.15f);     

        if (instance == null)
            instance = this;
        //to set the height for winning the game
        GameCompleteObj.transform.position = new Vector3(GameCompleteObj.transform.position.x, heightToComplete, GameCompleteObj.transform.position.z);

        //to set the height of the ruler
         setRulerHeight(0.15f);
    }

    void Start()
    {
        itemSpawner.SpawnItem();
    }

    void Update()
    {
    // game complete check    
    if (PlayerPrefs.GetFloat("HeightVal") > heightToComplete)
        {
            CancelInvoke("NewItem");          
            Debug.Log("Game Completed");
        }
    
    }
    public void setRulerHeight(float tempHeightVal)
    {
        Ruler.transform.localScale = new Vector3(Ruler.transform.localScale.x, tempHeightVal, Ruler.transform.localScale.z);
    }
    public float rulerHeightFn()
    {
        return (Ruler.transform.localScale.y);
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
        PlayerPrefs.SetFloat("HeightVal", -4.5f);
        PlayerPrefs.SetFloat("RulerHeight", 0.15f);     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }  

}

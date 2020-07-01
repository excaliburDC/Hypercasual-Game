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

    public GameObject Ruler;
  
    void Awake()
    {
        PlayerPrefs.SetFloat("Ruler", 0.1f);

        if (instance == null)
            instance = this;

    }
  
    void Start()
    {
        itemSpawner.SpawnItem();
    }

     void Update()
    {
        setRulerHeight();
    }
    public void setRulerHeight()
    {
        float temp = PlayerPrefs.GetFloat("Ruler");
        Ruler.transform.localScale = new Vector3(Ruler.transform.localScale.x,temp, Ruler.transform.localScale.z);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
}

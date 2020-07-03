using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private float min_x = -2.2f, max_x = 2.2f;
    private bool canMove;

    private Rigidbody2D  mybody;

    private bool gameover;
    private bool ignoreCollision;
  

    //drag option using touch 
    float deltaX, deltaY;
    //bool moveAllowed = false;
    float speedModifier = 1f;

    //for height check
    public static float heightval;
    public static float rulerHeight;

    //drag and drop using mouse
    private Vector3 moffset;
    private float mxCoord;

  
    void Awake()
    {
        heightval = PlayerPrefs.GetFloat("HeightVal");
        rulerHeight = PlayerPrefs.GetFloat("RulerHeight");

        mybody = GetComponent<Rigidbody2D>();
        mybody.gravityScale = 0f;
    }
    void Start()
    {
        canMove = true;
        GamePlayController.instance.currentItem = this;       
    }

    void Update()
    {
        MoveBox();
    }
    void MoveBox() 
    {
        #region Andriod
        if (canMove)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        //if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos))
                        //{
                            deltaX = touchpos.x - transform.position.x;
                            deltaY = touchpos.y - transform.position.y;

                        //moveAllowed = true;
                        //}
                        break;
                    case TouchPhase.Moved:
                        //if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos) && moveAllowed)
                        mybody.MovePosition(new Vector3(Mathf.Clamp((touchpos.x - deltaX), min_x, max_x) * speedModifier,/* transform.position.y*/(touchpos.y - deltaY) * speedModifier)); 
                        Debug.LogWarning(deltaY);
                        break;

                    case TouchPhase.Ended:
                        //moveAllowed = false;
                        DropItem();
                        break;
                }
              
            }
        }
        #endregion Andriod
    }

    //mouse control for testing purpose
    private void OnMouseDown()
    {
        if (canMove)
        {
            mxCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            moffset = gameObject.transform.position - GetMouseWorldPos();
        }
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousepoints = Input.mousePosition;
        mousepoints.z= mxCoord;

        return Camera.main.ScreenToWorldPoint(mousepoints);
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + moffset;
    }
    private void OnMouseUp()
    {
        DropItem();
    }
    //end of the mouse input for testing purpose

    public void DropItem()
    {
        canMove = false;
        mybody.gravityScale =Random.Range(2,4);

    }
    void Landed()
    {
       
        #region heightcheck      
        if (transform.position.y >= heightval)
        {
            rulerHeight = GamePlayController.instance.rulerHeightFn();        
            float tempDiffVal = Mathf.Abs(Mathf.Abs(transform.position.y/10) - Mathf.Abs(heightval/10));
            
            if (transform.position.y > heightval)
            {            
                heightval = transform.position.y;
                rulerHeight =rulerHeight+ (2.5f* tempDiffVal);               
                PlayerPrefs.SetFloat("HeightVal", heightval);
                PlayerPrefs.SetFloat("RulerHeight", rulerHeight);

                GamePlayController.instance.setRulerHeight(rulerHeight);
            }
        }
        #endregion heightcheck
        if (gameover)
            return;
        ignoreCollision = true;
                
            GamePlayController.instance.spawnNewItem();
            GamePlayController.instance.MoveCamera();           
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.tag == "Platform")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }
        if (target.gameObject.tag == "GroceryItem")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }

    }
    
}

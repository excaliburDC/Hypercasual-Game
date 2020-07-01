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
    public static float heightval = -4.5f;
    public static float rulerHeight = 0.1f;

    //drag and drop using mouse
    private Vector3 moffset;
    private float mxCoord;

  
    void Awake()
    {
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
                        mybody.MovePosition(new Vector3(Mathf.Clamp((touchpos.x - deltaX), min_x, max_x) * speedModifier, transform.position.y/*(touchpos.y - deltaY) * speedModifier*/)); 
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
        mxCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
        moffset = gameObject.transform.position - GetMouseWorldPos();
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousepoints = Input.mousePosition;
        mousepoints.y = mxCoord;

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
        //test code for height of stack
        #region heightcheck
        rulerHeight = GamePlayController.instance.rulerHeightFn();
        Debug.Log("rulerheiht before = " + rulerHeight);
       
        if (transform.position.y >= heightval)
        {
            float temp = Mathf.Abs((transform.position.y/10) - (heightval/10));
            heightval = transform.position.y;
          
            if (rulerHeight > heightval )
            {
                rulerHeight =rulerHeight+ temp;
                PlayerPrefs.SetFloat("Ruler", rulerHeight);
                GamePlayController.instance.setRulerHeight();
            }

            Debug.Log("height value "+ heightval+"rulerheiht = " + rulerHeight+temp);

        }
        //end of  code for height of stack
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

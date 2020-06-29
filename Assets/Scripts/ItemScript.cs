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
  

    //drag option
    float deltaX, deltaY;

    bool moveAllowed = false;
    float speedModifier = 1f;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        MoveBox();
    }
    void MoveBox()
    {
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

                        moveAllowed = true;
                        //}
                        break;
                    case TouchPhase.Moved:
                       //if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos) && moveAllowed)
                       if(touchpos.x > min_x && touchpos.x < max_x)
                        mybody.MovePosition(new Vector3((touchpos.x - deltaX) * speedModifier, transform.position.y/*touchpos.y * speedModifier*/));
                        Debug.LogWarning(deltaY);
                        break;

                    case TouchPhase.Ended:
                        moveAllowed = false;
                        DropItem();
                        break;
                }
              
            }
        }
    }
    public void DropItem()
    {
        canMove = false;
        mybody.gravityScale =Random.Range(2,4);
    }
    void Landed()
    {
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

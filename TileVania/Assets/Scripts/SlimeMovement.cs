using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{

    Rigidbody2D myRB2D;
    [SerializeField] float moveSpeed = 1f; //to show on inspector


    // Start is called before the first frame update
    void Start()
    {
        myRB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRB2D.velocity = new Vector2(moveSpeed,0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        flipEnemyDirection();
    }

    void flipEnemyDirection()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRB2D.velocity.x)), 1f); //x value grabs current object velocity
    }
}

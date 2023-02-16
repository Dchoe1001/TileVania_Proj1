using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    Rigidbody2D myRB2D;
    [SerializeField] float shootingSpeed = 15f;

    float currentHorizontalSpeed;

    PlayerMovement player; // a reference to player
    // Start is called before the first frame update
    void Start()
    {
        myRB2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        currentHorizontalSpeed = player.transform.localScale.x * shootingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        myRB2D.velocity = new Vector2(currentHorizontalSpeed, 0f);
        transform.localScale = new Vector2(Mathf.Sign(myRB2D.velocity.x), 1f);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemySlime") {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        Destroy(gameObject); // delete bullet(obj) itself
    }
}

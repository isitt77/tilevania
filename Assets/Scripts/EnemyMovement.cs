using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D enemyRb2d;
    BoxCollider2D wallDetector;
    CompositeCollider2D platformCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb2d = GetComponent<Rigidbody2D>();
        wallDetector = GetComponent<BoxCollider2D>();
        platformCollider2D = GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRb2d.velocity = new Vector2(moveSpeed, 0f);
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "PlatformCollider")
        {
            moveSpeed = -moveSpeed;
            EnemyFlip();
        }
    }

    void EnemyFlip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRb2d.velocity.x)), 1f);
    }
   
}

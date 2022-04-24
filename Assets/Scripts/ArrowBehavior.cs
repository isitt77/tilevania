using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    PlayerMovement player;

    Rigidbody2D arrow;
    [SerializeField] float arrowSpeed = 20f;
    float vectorXspeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        arrow = GetComponent<Rigidbody2D>();
        // Makes sure arrow shoots in the same direction Player is facing.
        vectorXspeed = player.transform.localScale.x * arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        arrow.velocity = new Vector2(vectorXspeed, 0f);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        Destroy(gameObject, 0.3f);
    }

}

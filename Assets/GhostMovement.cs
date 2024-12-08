using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int movement = 1;
    [SerializeField] const int SPEED = 4;
    [SerializeField] float boundaryX = 6.18f;

    // Start is called before the first frame update
    void Start()
    {
     if (rigid == null) {
        rigid = GetComponent<Rigidbody2D>();
     }   
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= boundaryX) {
            transform.position = new Vector3 (boundaryX, transform.position.y, transform.position.z);
            Flip();
        } else if (transform.position.x <= -boundaryX) {
            transform.position = new Vector3 (-boundaryX, transform.position.y, transform.position.z);
            Flip();
        }
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(movement * SPEED, rigid.velocity.y);
    }

    void Flip() {
        movement = -movement;
        transform.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Arrow")) {
            Destroy(collider.gameObject);
        }
    }
}

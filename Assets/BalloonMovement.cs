using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int movement = 1;
    [SerializeField] const int SPEED = 5;
    [SerializeField] float boundaryX = 6.18f;
    [SerializeField] AudioSource popAudio;
    [SerializeField] float growthRate = 0.004f;
    private readonly Vector3 MAXIMUM_BALLOON_SIZE = new Vector3(0.5f, 0.48f, 1.0f);
    private LevelManager levelManager;


    [Serializable] public struct SizeRange {
        public float minSize;
        public float maxSize;
        public int points;
    }

    [SerializeField] SizeRange[] sizeRanges;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null) {
            rigid = GetComponent<Rigidbody2D>();
        }
        if (popAudio == null) {
            popAudio = GetComponent<AudioSource>();
        }

        levelManager = FindObjectOfType<LevelManager>();

        sizeRanges = new SizeRange[] {
            new SizeRange { minSize = 1.15f, maxSize = 1.22f, points = 1 },
            new SizeRange { minSize = 1.09f, maxSize = 1.14f, points = 2 },
            new SizeRange { minSize = 1.00f, maxSize = 1.08f, points = 3 },
        };

        InvokeRepeating("GrowBalloon", 0.0f, 0.1f);
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
        rigid.velocity = new Vector2(SPEED * movement, rigid.velocity.y);
        
    }

    void Flip() {
        movement = -movement;
        transform.Rotate(0, 180, 0);
    }

    void GrowBalloon() {
        if (transform.localScale.magnitude < MAXIMUM_BALLOON_SIZE.magnitude) {
            transform.localScale += new Vector3(growthRate, growthRate, 0);
        } else if (transform.localScale.magnitude > MAXIMUM_BALLOON_SIZE.magnitude) {
            AudioSource.PlayClipAtPoint(popAudio.clip, transform.position);
            Destroy(gameObject);
            levelManager.RestartLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Arrow"))
    {
        AudioSource.PlayClipAtPoint(popAudio.clip, transform.position);
        Destroy(collider.gameObject);
        Scorekeeper.Instance.AddPoints(CalculatePoints(transform.localScale.magnitude));
        
        float delay = popAudio.clip.length;
        Invoke(nameof(DelayedBalloonPopped), delay);

        Destroy(gameObject, delay);
    }
    }

    private int CalculatePoints(float size)
    {
       foreach (var range in sizeRanges) {
        if (size >= range.minSize && size <= range.maxSize) {
            return range.points;
        }
       }
       return 0;
    }

    private void DelayedBalloonPopped()
    {
        levelManager.BalloonPopped();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    public float speed;
    private Vector2 vel;
    public float drag;

    public float clawRefresh;
    private float clawCounter;


    private Animator animator;
    private Rigidbody2D rb;

    public ClawDetector claw;

    private float activeTime = 0.5f;
    private float activeCounter;
    private bool clawActive = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        clawCounter = 0;
        vel = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity /= drag;
        if(clawCounter != 0)
        {
            clawCounter -= Time.deltaTime;
            if(clawCounter < 0)
            {
                clawCounter = 0;
            }
        }
        if (Input.GetKeyDown("space") && clawCounter == 0 && GameManager.gameStarted)
        {
            animator.SetTrigger("Claw");
            clawCounter = clawRefresh;
            activeCounter = activeTime;
        }
        if(activeCounter > 0)
        {
            activeCounter -= Time.deltaTime;
            if(activeCounter <= 0)
            {
                if (clawActive)
                {
                    claw.active = false;
                    clawActive = false;
                }
                else
                {
                    clawActive = true;
                    claw.active = true;
                    activeCounter = activeTime;
                }
            }
        }

        vel.x = 0;
        vel.y = 0;
        if (GameManager.gameStarted)
        {
            if (Input.GetKey("w"))
            {
                vel.y = 1;
            }
            if (Input.GetKey("s"))
            {
                vel.y = -1;
            }
            if (Input.GetKey("a"))
            {
                vel.x = -1;
                transform.localScale = new Vector3(-1, 1, 1);

            }
            if (Input.GetKey("d"))
            {
                vel.x = 1;
                transform.localScale = new Vector3(1, 1, 1);
            }
            rb.velocity = rb.velocity + (vel.normalized * speed);
            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = rb.velocity.normalized * speed;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Oil")
        {
            speed /= 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Oil")
        {
            speed *= 2;
        }
    }
}

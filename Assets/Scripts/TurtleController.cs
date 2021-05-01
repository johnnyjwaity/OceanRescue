using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour
{
    public Vector2 xBounds, yBounds;
    private Vector3 direction;
    public float speed;
    private Rigidbody2D rb;
    private float time, counter;

    private bool isFreed = false;

    public float deathTime;
    private float deathCounter;

    public GameObject plastic;
    private bool removedPlastic = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction.x = Random.Range(-1f, 1f);
        direction.y = Random.Range(-1f, 1f);
        direction.z = 0;
        direction.Normalize();
        time = 2;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }

        if(counter <= 0)
        {
            if (transform.position.x < xBounds.x || transform.position.x > xBounds.y)
            {
                direction.x *= -1;
                counter = time;
            }
            if (transform.position.y < yBounds.x || transform.position.y > yBounds.y)
            {
                direction.y *= -1;
                counter = time;
            }
        }
        

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            angle += 180;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.position = new Vector3(transform.position.x + (direction.x * speed), transform.position.y + (direction.y * speed), transform.position.z);
        rb.velocity = direction * speed;


        if (isFreed)
        {
            deathCounter -= Time.deltaTime;
            if(deathCounter <= 1.5 && !removedPlastic)
            {
                removedPlastic = true;
                Destroy(plastic);
            }
            if(deathCounter <= 0)
            {
                GameManager.turtleCount -= 1;
                Destroy(gameObject);
            }
        }
    }
    public void FreeTurtle()
    {
        if (!isFreed)
        {
            GetComponent<Animator>().SetTrigger("Free");
            deathCounter = deathTime;
            isFreed = true;
            FindObjectOfType<GameManager>().turtleSaved += 1;
            FindObjectOfType<GameManager>().RemovePolution();
        }
    }
}

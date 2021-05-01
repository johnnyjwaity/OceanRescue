using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    public float speed;
    public Vector2 xBounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        if(transform.position.x > xBounds.y || transform.position.x < xBounds.x)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawDetector : MonoBehaviour
{
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active)
        {
            if(collision.gameObject.tag == "Turtle")
            {
                collision.gameObject.GetComponent<TurtleController>().FreeTurtle();
            }
            if(collision.gameObject.tag == "Trash")
            {
                collision.gameObject.GetComponent<TrashController>().RemoveTrash();
            }
        }
    }
}

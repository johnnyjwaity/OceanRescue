using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    public float dragScale;
    private bool hitWater = false;
    private bool trashRemoved = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Water" && !hitWater)
        {
            hitWater = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.drag = dragScale;
        }
    }
    public void RemoveTrash()
    {
        if (!trashRemoved)
        {
            FindObjectOfType<GameManager>().trashSaved += 1;
            FindObjectOfType<GameManager>().RemovePolution();
            trashRemoved = true;
            GetComponent<Animator>().SetTrigger("Fade");
            Destroy(gameObject, 0.8f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{

    public GameObject[] trash;
    public float trashTime;
    private float trashCounter;

    // Start is called before the first frame update
    void Start()
    {
        trashCounter = trashTime;
    }

    // Update is called once per frame
    void Update()
    {
        trashCounter -= Time.deltaTime;
        if(trashCounter <= 0)
        {
            trashCounter = trashTime;
            if (GameManager.gameStarted)
            {
                GameObject t = Instantiate(trash[Random.Range(0, trash.Length)], transform.position, Quaternion.identity);
                t.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
                t.GetComponent<Rigidbody2D>().AddTorque(10);
            }
        }
    }
}

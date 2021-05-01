using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform anchor;
    private Camera cam;

    [Range(1, 10)]
    public float smooth;

    public Vector3 leftBound, rightBound, bottomBound;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(anchor.position.x, anchor.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.fixedDeltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float CameraSpeed;
    public float MouseSensivity;

    float x;
    float y;

    float mouse;

    // Start is called before the first frame update
    void Start()
    {
        mouse = 5;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        mouse -= Input.GetAxisRaw("Mouse ScrollWheel") * MouseSensivity;
        mouse = Mathf.Clamp(mouse, 2, 10);

        transform.Translate(new Vector3(x, y, 0) * CameraSpeed * Time.deltaTime);

        Camera.main.orthographicSize = mouse;
    }
}

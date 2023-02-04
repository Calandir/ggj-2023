using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMovement : MonoBehaviour
{
    public float speed = 5f;
    public float returnSpeed = 2f;
    public float resetTime = 1f;

    private Vector3 originalPosition;
    private float timer;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            timer = 0f;
            transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= resetTime && Mathf.Round(transform.position.y) != Mathf.Round(originalPosition.y))
            {
                transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * returnSpeed);
            }
        }
    }
}

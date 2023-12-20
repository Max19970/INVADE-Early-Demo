using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderSlide : MonoBehaviour
{
    private Vector3 currentPosition;
    private Vector3 desired;
    private Vector3 velocity;
    private float smoothTime;

    public void Move(float xcord)
    {
        desired = new Vector3(xcord, desired.y, desired.z);
    }

    void Update()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, desired, ref velocity, smoothTime, Mathf.Infinity, 0.03f);
    }

    void Awake()
    {
        currentPosition = transform.localPosition;
        desired = currentPosition;
        velocity = Vector3.zero;
        smoothTime = 0.1f;
    }
}

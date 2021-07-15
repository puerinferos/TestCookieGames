using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float min;
    [SerializeField] private float max;
    [Space]
    [SerializeField] private float ballBounceMultiplayer;

    private Ray castPoint;
    private RaycastHit hit;
    private Vector3 mousePosition;
    private Camera _camera;

    private void Start() =>
        _camera = Camera.main;

    void Update()
    {
        if (Input.mousePresent)
        {
            mousePosition = Input.mousePosition;
            castPoint = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                transform.position = ChangedPositionX(Mathf.Clamp(hit.point.x, min, max));
        }
    }

    private Vector3 ChangedPositionX(float x) =>
        new Vector3(x, transform.position.y,
            transform.position.z);

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.GetComponent<Ball>())
        {
            Rigidbody ballRigidbody = other.transform.GetComponent<Rigidbody>();

            Vector3 hitPoint = other.contacts[0].point;

            float xDifference = transform.position.x - hitPoint.x;
            if (hitPoint.x < transform.position.x)
                other.transform.GetComponent<Ball>()
                    .ChangeVelocityVector(new Vector3(-Mathf.Abs(xDifference * ballBounceMultiplayer), 0, 1));
            else
                other.transform.GetComponent<Ball>()
                    .ChangeVelocityVector(new Vector3(Mathf.Abs(xDifference * ballBounceMultiplayer), 0, 1));
        }
    }
}
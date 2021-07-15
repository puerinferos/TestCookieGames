using System;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] [ReadOnly] private float speed;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float maxSpeed;

    [SerializeField] private Animator ballAnimator;

    public event Action OnLoose;
    public event Action OnWin;


    private Rigidbody rigidbody;
    private bool gameStarted = false;

    private static readonly int GameStarted = Animator.StringToHash("gameStarted");

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        speed = initialSpeed;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            Random.Range(-60, 60),
            transform.eulerAngles.z);
    }

    private void StartGame()
    {
        ballAnimator.SetTrigger(GameStarted);

        rigidbody.AddForce(transform.forward * 250);
    }

    public void ChangeVelocityVector(Vector3 direction) =>
        rigidbody.velocity = direction.normalized * speed;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartGame();

            gameStarted = true;
        }

        if (!gameStarted)
            return;
        if (speed < maxSpeed)
            speed += speedMultiplier*Time.deltaTime;
        ballAnimator.speed = speed / initialSpeed;

        rigidbody.velocity = rigidbody.velocity.normalized * speed;
        Debug.DrawRay(transform.position, rigidbody.velocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameStarted)
            transform.forward = rigidbody.velocity;

        if (other.transform.GetComponent<DeathCube>())
        {
            speed = initialSpeed;

            OnLoose?.Invoke();
        }

        if (other.transform.GetComponent<Paddle>())
            OnWin?.Invoke();
    }
}
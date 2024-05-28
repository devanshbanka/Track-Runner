using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RunSpeed;
    public float HorizontalSpeed = 10f;
    public float speedIncrease;

    public Rigidbody rb;

    private int currentLane = 0;

    [SerializeField] private float JumpForce = 350;

    bool isJumping = false;
    bool isSliding = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 forwardMovement = transform.forward * RunSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping && !isSliding)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isJumping && !isSliding)
        {
            Slide();
        }
    }

    private void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        targetLane = Mathf.Clamp(targetLane, -1, 1);

        if (targetLane != currentLane)
        {
            float targetX = targetLane * 3f;
            Vector3 targetPosition = new Vector3(targetX, rb.position.y, rb.position.z);

            rb.MovePosition(targetPosition);

            currentLane = targetLane;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && GetComponent<CapsuleCollider>().center.y == 3.1f)
        {
            isJumping = false;
            isSliding = false;
            GetComponent<Animation>().Play("Run");
        }

        if (collision.gameObject.name == "Graphic")
        {
            StartCoroutine(GameManager.MyInstance.Dead());
        }
        if (collision.gameObject.name == "Coin(Clone)")
        {
            SoundManager.PlaySound("Coin");
            Destroy(collision.gameObject);
            GameManager.MyInstance.Score += 1;
            RunSpeed += speedIncrease;
        }
    }

    void Jump()
    {
        SoundManager.PlaySound("Jump");
        isJumping = true;
        bool animator =  GetComponent<Animation>().Play("Runtojumpspring");
        rb.AddForce(Vector3.up * JumpForce);
    }

    void Slide()
    {
        isSliding = true;
        bool animator = GetComponent<Animation>().Play("Runtoslide");
        float colliderHeight = GetComponent<CapsuleCollider>().center.y;
        colliderHeight = -2f;

        if (!animator)
        {
            isSliding = false;
        }
    }
}
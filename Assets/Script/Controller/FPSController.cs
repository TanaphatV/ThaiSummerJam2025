using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    [Header("Horizontal")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [Header("Vertical")]
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    [Header("hp")]
    [SerializeField] private Image fillBar;

    [SerializeField] private float gizmosHeight = 0f;

    public int Health { get; private set; }

    [field: SerializeField] public int MaxHealth { get; set; }

    public Transform orientation;

    [SerializeField] private Transform camPos;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    bool allowMovement = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Health = MaxHealth;
    }

    private void Update()
    {
        fillBar.fillAmount = (float)Health / MaxHealth;

        if (!allowMovement)
            return;

        SpeedControl();
    }

    private void FixedUpdate()
    {
        if (!allowMovement)
            return;

        MovePlayer();
        ClampPlayerPosition();
    }

    private void ClampPlayerPosition()
    {
        Vector3 clampedPosition = rb.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);

        rb.position = clampedPosition;
    }

    public void Restart()
    {
        allowMovement = true;
        Health = MaxHealth;
    }

    public void OnEndGame()
    {
        allowMovement = false;
    }

    private void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void SetHealth(int health)
    {
        Health = health;
        if(health <= 0)
        {
            GameManager.instance.EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Health -= 1;
        if (other.gameObject.TryGetComponent<ScrollingObject>(out var scrollingObj))
        {
            Health -= 1;
            if(Health <= 0)
            {
                Debug.Log("Dead");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 bottomLeft = new Vector3(minX, transform.position.y - gizmosHeight, minZ);
        Vector3 bottomRight = new Vector3(maxX, transform.position.y - gizmosHeight, minZ);
        Vector3 topRight = new Vector3(maxX, transform.position.y - gizmosHeight, maxZ);
        Vector3 topLeft = new Vector3(minX, transform.position.y - gizmosHeight, maxZ);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
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

    [field : SerializeField] public int Health { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }

    public Transform orientation;

    [SerializeField] private Transform camPos;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
        fillBar.fillAmount = (float)Health / MaxHealth;
    }

    private void FixedUpdate()
    {
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

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
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

    private void OnTriggerEnter(Collider other)
    {
        Health -= 1;
        if (other.gameObject.TryGetComponent<ScrollingObject>(out var scrollingObj))
        {
            Health -= 1;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player;
    public float rayLength = 100f;

    public float clickCooldown = 0.5f; // Delay in seconds
    private float nextClickTime = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Time.time >= nextClickTime && Input.GetMouseButtonDown(0))
        {
            nextClickTime = Time.time + clickCooldown;

            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, rayLength))
            {
                Debug.Log("Mouse hit: " + hit.collider.name);
                Debug.DrawLine(mainCamera.transform.position, hit.point, Color.green, 2f);

                Vector3 targetPoint = hit.point;
                Vector3 directionToTarget = targetPoint - player.position;

                Ray playerRay = new Ray(player.position, directionToTarget.normalized);
                RaycastHit playerHit;

                if (Physics.Raycast(playerRay, out playerHit, directionToTarget.magnitude))
                {
                    if (playerHit.collider == hit.collider)
                    {
                        if (!hit.collider.CompareTag("Player"))
                            Destroy(hit.collider.gameObject);
                    }
                    else
                    {
                        if (!playerHit.collider.CompareTag("Player"))
                            Destroy(playerHit.collider.gameObject);
                    }
                }
                else
                {
                    Debug.DrawLine(player.position, targetPoint, Color.yellow, 2f);
                }
            }
        }
    }
}

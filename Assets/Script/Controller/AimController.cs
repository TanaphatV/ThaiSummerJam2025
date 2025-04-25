using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AimController : MonoBehaviour
{
    public Camera mainCamera;
    public ReloadUI reloadUI;
    public Transform player;

    public Animator animator;
    
    public float rayLength = 100f;

    public float reloadSpeed;

    public float clickCooldown = 0.5f; // Delay in seconds
    private float nextClickTime = 0f;
    public bool isOneShot = false;

    int bulletCount;
    const int maxBullet = 6;

    [SerializeField] private CrossHairGUIManager crossHairGUI;

    public UnityAction onShoot;
    public UnityAction onReload;

    bool isReloading = false;
    bool queShoot = false;

    public AudioSource shootSound;
    public AudioSource reloadSound;
    public AudioSource noBulletShootSound;

    bool allowShoot = false;

    private void Start()
    {
        mainCamera = Camera.main;
        reloadUI.timeBetweenShot = clickCooldown;
        reloadUI.timeToReload = reloadSpeed;
    }

    public void Restart()
    {
        allowShoot = true;
        bulletCount = maxBullet;
        reloadUI.Restart();
    }

    public void End()
    {
        allowShoot = false;
    }

    IEnumerator ReloadIE()
    {
        isReloading = true;
        while (bulletCount < 6)
        {
            if (queShoot)
            {
                Shoot();
                queShoot = false;
                break;
            }

            reloadUI.Reload();
            reloadSound?.Play();

            yield return new WaitForSeconds(reloadSpeed);
            bulletCount++;
        }

        isReloading = false;
    }

    void Update()
    {
        if (!allowShoot)
            return;

        if (Time.time >= nextClickTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (bulletCount <= 0)
                {
                    noBulletShootSound?.Play();
                    return;
                }

                if (!isReloading)
                    Shoot();
                else
                    queShoot = true;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (!isReloading && bulletCount != maxBullet)
                    StartCoroutine(ReloadIE());
            }

        }
    }

    void Shoot()
    {
        reloadUI.Shoot();
        shootSound?.Play();
        animator?.SetTrigger("Shoot");
        bulletCount--;
        nextClickTime = Time.time + clickCooldown;

        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit, rayLength))
        {
            //Debug.Log("Mouse hit: " + hit.collider.name);
            Debug.DrawLine(mainCamera.transform.position, hit.point, Color.green, 2f);

            Vector3 targetPoint = hit.point;
            Vector3 directionToTarget = targetPoint - player.position;

            Ray playerRay = new Ray(player.position, directionToTarget.normalized);
            RaycastHit playerHit;

            if (Physics.Raycast(playerRay, out playerHit, directionToTarget.magnitude))
            {
                if (playerHit.collider == hit.collider)
                {
                    Transform hitTransform = hit.collider.transform;
                    Obstacle obstacle = hitTransform.GetComponentInParent<Obstacle>();

                    if (obstacle != null)
                    {
                        obstacle.OnHitByBullet(isOneShot);
                        crossHairGUI.Hit();
                    }
                }
                else
                {
                    Transform hitTransform = playerHit.collider.transform;
                    Obstacle obstacle = hitTransform.GetComponentInParent<Obstacle>();

                    if (obstacle != null)
                    {
                        obstacle.OnHitByBullet(isOneShot);
                        crossHairGUI.Hit();
                    }
                }
            }
            else
            {
                Debug.DrawLine(player.position, targetPoint, Color.yellow, 2f);
            }
        }
    }
}

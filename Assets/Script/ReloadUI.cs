using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReloadUI : MonoBehaviour
{
    public List<GameObject> bulletGraphic = new(6);
    public RectTransform cylinder;

    int currentBulletIndex;
    GameObject currentBullet => bulletGraphic[currentBulletIndex];

    public float angleBetweenBullet;

    public float timeBetweenShot;
    public float timeToReload;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    void Reload()
    {
        StartCoroutine(ReloadIE());
    }

    void Shoot()
    {
        StartCoroutine(ShootIE());
    }

    IEnumerator ReloadIE()
    {
        float reloadSpeed = angleBetweenBullet / timeToReload;
        float targetRotation = cylinder.rotation.eulerAngles.z + angleBetweenBullet;

        yield return new WaitForSeconds(timeToReload * 0.5f);

        bool needToRotate = currentBullet.activeSelf;
        if (needToRotate)
        {
            while (!AreAnglesClose(cylinder.rotation.eulerAngles.z, targetRotation))
            {
                cylinder.rotation = Quaternion.RotateTowards(
                    cylinder.transform.rotation,
                    Quaternion.Euler(0, 0, targetRotation),
                    reloadSpeed * Time.deltaTime
                );
                yield return null;
            }
            NextBullet();
            currentBullet.SetActive(true);
        }
        else
        {
            currentBullet.SetActive(true);
        }
    }


    IEnumerator ShootIE()
    {
        currentBullet.SetActive(false);
        PreviousBullet();

        float reloadSpeed = angleBetweenBullet / timeToReload;
        float targetRotation = cylinder.rotation.eulerAngles.z - angleBetweenBullet;

        while (!AreAnglesClose(cylinder.rotation.eulerAngles.z, targetRotation))
        {
            cylinder.rotation = Quaternion.RotateTowards(
                cylinder.transform.rotation,
                Quaternion.Euler(0, 0, targetRotation),
                reloadSpeed * Time.deltaTime
            );
            yield return null;
        }
    }

    bool AreAnglesClose(float a, float b, float threshold = 0.1f)
    {
        return Mathf.Abs(Mathf.DeltaAngle(a, b)) < threshold;
    }

    void NextBullet()
    {
        currentBulletIndex++;
        if (currentBulletIndex > 5)
            currentBulletIndex = 0;
    }

    void PreviousBullet()
    {
        currentBulletIndex--;
        if (currentBulletIndex < 0)
            currentBulletIndex = 5;
    }

}

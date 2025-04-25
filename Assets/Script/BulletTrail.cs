using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private LineRenderer lr;

    public Vector3Int startTrail;
    public Vector3Int endTrail;

    [ContextMenu("Test")]
    public void Test()
    {
        Initialize(startTrail, endTrail);
    }

    public void Initialize(Vector3 start, Vector3 end, float duration = 0.05f)
    {
        Debug.Log("Shoot");

        if (!lr) lr = GetComponent<LineRenderer>();

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        StartCoroutine(DestroyAfter(duration));
    }

    private System.Collections.IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

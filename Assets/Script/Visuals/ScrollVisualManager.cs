using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class ScrollVisualManager : MonoBehaviour
{
    public float scrollSpeed;
    public float curveChangeSpeed;
    public float minSideCurve;
    public float maxSideCurve;
    public float backCurveMagnitude;
    public float floorStretchMagnitude;

    public bool previewCurveInEditMode;

    [Range(0, 1.0f)] public float chanceToCurve;

    public float minCurveDuration;
    public float maxCurveDuration;

    private readonly int shaderID_scrollOffset = Shader.PropertyToID(ShaderVariable.SCROLL_OFFSET);
    private readonly int shaderID_sideCurve = Shader.PropertyToID(ShaderVariable.CURVE_SIDE);
    private readonly int shaderID_backCurve = Shader.PropertyToID(ShaderVariable.CURVE_BACK);
    private readonly int shaderID_floorStretch = Shader.PropertyToID(ShaderVariable.CURVE_FLOOR);

    float  _currentY = 0;
    float _currentCurve = 0;

    bool isCurving = false;

    const float curveMultiplier = 0.001f;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(!EditorApplication.isPlaying)
        {
            if(previewCurveInEditMode)
            {
                Shader.SetGlobalFloat(shaderID_backCurve, backCurveMagnitude * curveMultiplier);
                Shader.SetGlobalFloat(shaderID_sideCurve, maxSideCurve * curveMultiplier);
                Shader.SetGlobalFloat(shaderID_floorStretch, floorStretchMagnitude * curveMultiplier);
            }
            else
            {
                Shader.SetGlobalFloat(shaderID_backCurve, 0);
                Shader.SetGlobalFloat(shaderID_sideCurve, 0);
                Shader.SetGlobalFloat(shaderID_floorStretch, 0);
                
            }
        }
    }
#endif
    private void Start()
    {
        Shader.SetGlobalFloat(shaderID_backCurve, backCurveMagnitude * curveMultiplier);
        Shader.SetGlobalFloat(shaderID_sideCurve, 0);
        Shader.SetGlobalFloat(shaderID_floorStretch, floorStretchMagnitude * curveMultiplier);
    }

    private void Update()
    {
        ScrollTexture();
        if (!isCurving)
        {
            if(Random.Range(0,1.0f) > chanceToCurve)
            {
                SetTargetCurveMagnitude(0);
            }
            else
            {
                float curveModifier = Random.Range(0, 2) == 0 ? -1:1;
                SetTargetCurveMagnitude(Random.Range(minSideCurve * curveMultiplier * curveModifier, maxSideCurve * curveMultiplier * curveModifier));
            }
        }
    }

    private void SetTargetCurveMagnitude(float targetCurve)
    {
        StartCoroutine(CurveIE(targetCurve));
    }


    IEnumerator CurveIE(float targetCurve)
    {
        isCurving = true;
        while (!Mathf.Approximately(_currentCurve,targetCurve))
        {

            _currentCurve = Mathf.MoveTowards(_currentCurve, targetCurve, curveChangeSpeed * curveMultiplier * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            Shader.SetGlobalFloat(shaderID_sideCurve, _currentCurve);
        }

        float duration = Random.Range(minCurveDuration,maxCurveDuration);
        yield return new WaitForSeconds(duration);

        isCurving = false;
    }



    private void ScrollTexture()
    {
        _currentY += scrollSpeed * GameManager.instance.speedMultiplier * Time.deltaTime;
        Shader.SetGlobalVector(shaderID_scrollOffset,new Vector2(0,_currentY));
    }
}

public class ShaderVariable
{
    public const string SCROLL_OFFSET = "_ScrollOffset";
    public const string CURVE_SIDE = "_SideCurveMagnitude";
    public const string CURVE_BACK = "_BackCurveMagnitude";
    public const string CURVE_FLOOR = "_FloorCurveMagnitude";
}
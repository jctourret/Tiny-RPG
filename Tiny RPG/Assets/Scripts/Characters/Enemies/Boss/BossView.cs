using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossView : MonoBehaviour
{
    Animator animator;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void TriggerCharge()
    {
        animator.SetTrigger("Charge");
    }

    public void EnableLineRenderer()
    {
        lineRenderer.enabled = true;
    }
    public void DisableLineRenderer()
    {
        lineRenderer.enabled = false;
    }
    public void UpdateLineRenderer(float width, Vector3 start, Vector3 end)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        Vector3[] points = new Vector3[2];
        points[0] = start;
        points[1] = end;
        lineRenderer.SetPositions(points);
    }
}

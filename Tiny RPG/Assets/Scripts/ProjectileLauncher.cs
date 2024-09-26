using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    float fireRateBase = 2;
    float currentfireRate;
    [SerializeField]
    float fireRateFlux = 0.2f;
    float fireTimer;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform launchPoint;
    [SerializeField]
    Vector3 launchDir;
    private void Start()
    {
        currentfireRate = Random.Range(fireRateBase - fireRateFlux, fireRateBase + fireRateFlux);
    }
    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer>=currentfireRate)
        {
            fireTimer = 0;
            currentfireRate = Random.Range(fireRateBase - fireRateFlux, fireRateBase + fireRateFlux);
            GameObject go = Instantiate(projectile,launchPoint.position,launchPoint.rotation,launchPoint);
            go.transform.up = launchDir;
            go.SetActive(true);
        }
    }

}

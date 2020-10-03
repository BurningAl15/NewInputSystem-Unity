using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public enum ShootType
    {
        Instantiation,
        Raycasting
    }

    public ShootType _shootType;

    public Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LineRenderer line;
    [SerializeField] private float angleChangeRatio;
    private void Awake()
    {
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, firePoint.position);
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_shootType == ShootType.Instantiation)
                Shoot_I();
            else
                StartCoroutine(Shoot());
        }
    }

    private void Shoot_I()
    {
        Vector3 firepointRotation = firePoint.rotation.eulerAngles;
        Quaternion rotation = Quaternion.Euler(firepointRotation.x, firepointRotation.y, firepointRotation.z+Random.Range(-angleChangeRatio,angleChangeRatio));
        GameObject tempBullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        
    }

    private IEnumerator Shoot()
    {
        var hitInfo = Physics2D.Raycast(firePoint.localPosition, firePoint.right);

        if (hitInfo)
        {
            //fill info about enemies or stuff here

            line.SetPosition(0, firePoint.localPosition);
            line.SetPosition(1, hitInfo.point);
        }
        else
        {
            line.SetPosition(0, firePoint.localPosition);
            line.SetPosition(1, firePoint.localPosition + firePoint.right * 100);
        }

        line.enabled = true;

        yield return 0;

        line.enabled = false;
    }
}
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
    }

    public ShootType _shootType;

    public Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float angleChangeRatio;
    private float timer = 0;
    private bool canShoot = false;

    [Range(0,.5f)]
    [SerializeField] private float timeBetweenShoots;

    [SerializeField] PlayerInput.PlayerNumber _playerNumber;

    private void Start()
    {
        _playerNumber = GetComponent<PlayerInput>()._playerNumber;
    }


    private void Update()
    {
        if (canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenShoots)
            {
                canShoot = false;
                timer = 0;
            }
        }
    }

    public void Shooting()
    {
        if (!canShoot)
        {
            if (_shootType == ShootType.Instantiation)
            {
                Shoot_I();
                canShoot = true;
            }
        }
    }
    
    private void Shoot_I()
    {
        Vector3 firepointRotation = firePoint.rotation.eulerAngles;
        Quaternion rotation = Quaternion.Euler(firepointRotation.x, firepointRotation.y,
            firepointRotation.z + 3f + Random.Range(-angleChangeRatio, angleChangeRatio));
        GameObject tempBullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        tempBullet.GetComponent<Bullet>().Init(_playerNumber);
    }
}
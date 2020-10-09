using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    public Rigidbody2D rgb;

    [SerializeField] PlayerInput.PlayerNumber _playerNumber;
    
    void Start()
    {
        rgb.velocity = transform.right * speed;
    }

    public void Init(PlayerInput.PlayerNumber _playerNumberTemp)
    {
        _playerNumber = _playerNumberTemp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(_playerNumber!=other.GetComponent<PlayerInput>()._playerNumber)
                other.GetComponent<Player>().ApplyForce(this.transform.position);
        }
        Destroy(gameObject);
    }
}

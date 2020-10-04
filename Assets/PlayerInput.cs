using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerActionControls playerActionControls;

    [SerializeField] private Player _player;
    [SerializeField] private Weapon _weapon;

    private void Awake()
    {
        playerActionControls=new PlayerActionControls();
    }

    private void Start()
    {
        playerActionControls.Player.Jump.performed += ctx => _player.Jump(ctx.ReadValue<float>());
        playerActionControls.Player.Shoot.performed += ctx => _weapon.Shooting();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    private void Update()
    {
        float movementInput = playerActionControls.Player.Move.ReadValue<float>();
        _player.HandleMovement_FullMidAirControl(movementInput);
    }
}

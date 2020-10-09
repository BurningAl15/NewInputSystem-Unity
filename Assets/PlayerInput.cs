using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public enum PlayerNumber
    {
        Player_1,Player_2
    }

    public PlayerNumber _playerNumber;
    
    [SerializeField] private PlayersActionControls playersActionControls;

    [SerializeField] private Player _player;
    [SerializeField] private Weapon _weapon;

    private void Awake()
    {
        playersActionControls = new PlayersActionControls();
    }

    private void Start()
    {
        if (_playerNumber == PlayerNumber.Player_1)
        {
            playersActionControls.Player1.Jump.performed += ctx => _player.Jump(ctx.ReadValue<float>());
            playersActionControls.Player1.Shoot.performed += ctx => _weapon.Shooting();
        }
        else if(_playerNumber==PlayerNumber.Player_2)
        {
            playersActionControls.Player2.Jump.performed += ctx => _player.Jump(ctx.ReadValue<float>());
            playersActionControls.Player2.Shoot.performed += ctx => _weapon.Shooting();
        }
    }

    private void OnEnable()
    {
        playersActionControls.Enable();
    }

    private void OnDisable()
    {
        playersActionControls.Disable();
    }

    private void Update()
    {
        if (_playerNumber == PlayerNumber.Player_1)
        {
            float movementInput = playersActionControls.Player1.Move.ReadValue<float>();
            _player.HandleMovement_FullMidAirControl(movementInput);            
        }
        else if(_playerNumber==PlayerNumber.Player_2)
        {
            float movementInput = playersActionControls.Player2.Move.ReadValue<float>();
            _player.HandleMovement_FullMidAirControl(movementInput);            
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    private Animator _chickenAnimator;
    private GameObject _player;
    private PlayerManager2 _playerManager;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");

        if (_player != null)
        {
            _playerManager = _player.GetComponent<PlayerManager2>();
            _chickenAnimator = _player.GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        
        if(_player.GetComponent<PlayerManager2>().IsGrounded())
        {
            _chickenAnimator.SetBool("isJumping", false);
            _chickenAnimator.SetBool("isGliding", false);
        }
        else
        {    if(_player.GetComponent<PlayerManager2>().CanGlide())
            {
                _chickenAnimator.SetBool("isGliding", true);
                _chickenAnimator.SetBool("isJumping", false);
            }
            else
            {
                //_chickenAnimControler.SetBool("isGliding", false);
                _chickenAnimator.SetBool("isJumping", true);
            }
        }

        if (_player.GetComponent<PlayerManager2>().GetAttack())
        {
            _chickenAnimator.SetBool("isAttacking", true);
        }
        else
        {
            _chickenAnimator.SetBool("isAttacking", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [SerializeField] Animator _chickenAnimControler;
    [SerializeField] GameObject _player;

    void Update()
    {
        
        //if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        
        if(_player.GetComponent<PlayerManager2>().IsGrounded())
        {
            _chickenAnimControler.SetBool("isJumping", false);
            _chickenAnimControler.SetBool("isGliding", false);
        }
        else
        {    if(_player.GetComponent<PlayerManager2>().CanGlide())
            {
                _chickenAnimControler.SetBool("isGliding", true);
                _chickenAnimControler.SetBool("isJumping", false);
            }
            else
            {
                //_chickenAnimControler.SetBool("isGliding", false);
                _chickenAnimControler.SetBool("isJumping", true);
            }
        }
    }
}

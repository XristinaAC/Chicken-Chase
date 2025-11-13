using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    Animator chickenAnimator;
    PlayerManager2 playerManager;

    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerManager = player.GetComponent<PlayerManager2>();
            chickenAnimator = player.GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        if (playerManager == null || chickenAnimator == null) return;

        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
        {
            chickenAnimator.speed = 0f; 
            return;
        }

        chickenAnimator.speed = 1f; 

        if (playerManager.IsGrounded())
        {
            chickenAnimator.SetBool("isJumping", false);
            chickenAnimator.SetBool("isGliding", false);
        }
        else
        {
            if (playerManager.CanGlide())
            {
                chickenAnimator.SetBool("isGliding", true);
                chickenAnimator.SetBool("isJumping", false);
            }
            else
            {
                chickenAnimator.SetBool("isGliding", false);
                chickenAnimator.SetBool("isJumping", true);
            }
        }

        //if(playerManager.GetAttack())
        //{
        //    chickenAnimator.SetBool("isAttacking", true);
        //}
    }
}
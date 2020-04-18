using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    public Animator CharacterAnimator;
    public Animator BoardAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CharacterAnimator.SetTrigger("Jump");
            BoardAnimator.SetTrigger("Attack");
        }
    }
}

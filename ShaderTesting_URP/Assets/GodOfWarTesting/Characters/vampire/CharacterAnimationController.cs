using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("x", Input.GetAxis("Horizontal"));
        animator.SetFloat("y", Input.GetAxis("Vertical"));
    }
}

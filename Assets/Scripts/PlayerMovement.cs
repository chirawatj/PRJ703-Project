using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private static bool playerExists = false;
    void Awake()
    {
        if (!playerExists)
        {
            DontDestroyOnLoad(gameObject);
            playerExists = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseController.IsGamePaused)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }
        rb.velocity = moveInput * moveSpeed;
        animator.SetBool("isWalking", rb.velocity.magnitude > 0);
    }

    public void Move(InputAction.CallbackContext context)
    {

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }
}

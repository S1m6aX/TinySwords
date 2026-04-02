using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D rb;
    public Animator am; 
    public SpriteRenderer sr;

    [Header("Public Variables")]
    public float moveSpeed = 1.0f;
    private Vector2 moveDirection;
    private int attackCombo = 1;
    private bool isAttacking = false;
    private bool isGuarding = false;

    void Start()
    {
        Application.targetFrameRate = 90;
    }

    void Update()
    {
        PlayerWindowsInput();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    /// <summary>
    /// Input
    /// </summary>
    public void PlayerWindowsInput()
    {   
        // Movement input
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        // Attack input
        if (Input.GetKeyDown(KeyCode.J))
        {   
            if (!isAttacking && !isGuarding)
            {
                isAttacking = true;

                if (attackCombo == 1) 
                {
                    PlayerAttack("Attack1");
                    attackCombo = 2;
                }
                else 
                {
                    PlayerAttack("Attack2");
                    attackCombo = 1;
                }
                Invoke("OnAttackEnd", 0.35f);
            }
        }

        // Defence input
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerGuard(true);
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            PlayerGuard(false);
        }
    }

    private void OnAttackEnd()
    {
        isAttacking = false;
    }

    /// <summary>
    /// Moves the player based on the input direction and updates the animation state.
    /// </summary>
    public void PlayerMove()
    {   
        if (!isAttacking && !isGuarding)
        {
            rb.velocity = moveDirection * moveSpeed;
            am.SetBool("IsRunning", moveDirection.magnitude > 0.1f);

            if (moveDirection.magnitude > 0.1f && Mathf.Abs(moveDirection.x) > 0.1f) // Only flip the sprite if the player is moving horizontally
            {
                sr.flipX = moveDirection.x < 0; // flip the sprite based on horizontal movement direction
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop movement while attacking
        }
    }

    /// <summary>
    /// Trigger the attack animation
    /// </summary>
    private void PlayerAttack(string attackType)
    {
        am.SetTrigger(attackType);
    }

    /// <summary>
    /// Handles the guard state of the player. 
    /// </summary>
    /// <param name="enterGuard">Exit gaurd or enter guard status</param>
    private void PlayerGuard(bool enterGuard)
    {
        am.SetBool("IsGuarding", enterGuard);
        isGuarding = enterGuard;
    }
}

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

    void Start()
    {
        Application.targetFrameRate = 90;
    }

    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    /// <summary>
    /// Moves the player based on the input direction and updates the animation state.
    /// </summary>
    public void PlayerMove()
    {
        rb.velocity = moveDirection * moveSpeed;
        am.SetBool("IsRunning", moveDirection.magnitude > 0.1f);
        
        if (moveDirection.magnitude > 0.1f && Mathf.Abs(moveDirection.x) > 0.1f) // Only flip the sprite if the player is moving horizontally
        {
            sr.flipX = moveDirection.x < 0; // flip the sprite based on horizontal movement direction
        }
    }
}

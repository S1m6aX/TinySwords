using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator am; 
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
    }
}

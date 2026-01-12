using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    [Header("Fall Respawn")]
    public float fallLimitY = -10f;   

    private Rigidbody2D rb;
    private Animator anim;

    private bool isGrounded;
    private bool facingRight = true;

  
    private Transform originalParent;

  
    private Vector3 startPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        originalParent = transform.parent;
        startPosition = transform.position; 
    }

    void Update()
    {
        CheckGround();
        Jump();
        UpdateAnimator();
        CheckFall();   
    }

    void FixedUpdate()
    {
        Move();
    }

    
    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );

        
        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();
    }

    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

   
    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheckPosition.position,
            0.2f,
            groundLayer
        );
    }

    
    void UpdateAnimator()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        anim.SetBool("Grounded", isGrounded);
    }

  
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(col.transform);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(originalParent);
        }
    }

  
    void CheckFall()
    {
        if (transform.position.y < fallLimitY)
        {
            Respawn();
        }
    }

 
    void Respawn()
    {
        rb.linearVelocity = Vector2.zero;

       
        transform.SetParent(originalParent);

   
        transform.position = startPosition;

      
        isGrounded = false;
        facingRight = true;
        transform.localScale = Vector3.one;

        anim.SetBool("Grounded", false);
        anim.SetFloat("Speed", 0f);
    }
}

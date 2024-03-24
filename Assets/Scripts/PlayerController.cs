using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Joysticks")]
    [SerializeField] private Joystick moveJoystick;
    [SerializeField] private Joystick fireJoystick;
    [Header("Player Move Speed")]
    [SerializeField] private float speedPlayer;

    private ItemInteractions itemInteractions;
    private PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;
    private Rigidbody2D rigidbodyPlayer;

    private float startSpeed;

    private float moveX;
    private float moveY;
    private void Start()
    {
        startSpeed = speedPlayer;

        itemInteractions = GetComponent<ItemInteractions>();
        playerHealth = GetComponent<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();  
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
        Flip();
    }
    private void Move() 
    {
        speedPlayer = fireJoystick.Horizontal != 0f && itemInteractions.item != null ? startSpeed / 2f : startSpeed;

        moveX = !playerHealth.playerIsDead ? moveJoystick.Horizontal * speedPlayer : 0f;
        moveY = !playerHealth.playerIsDead ? moveJoystick.Vertical * speedPlayer : 0f;

        rigidbodyPlayer.velocity = new Vector2(moveX, moveY);

        playerAnimator.SetBool("Move", rigidbodyPlayer.velocity != Vector2.zero);
    }
    private void Flip()
    {
        if (fireJoystick.Horizontal != 0f && itemInteractions.item != null) 
        {
            spriteRenderer.flipX = fireJoystick.Horizontal < 0f ? true : false;
        }
        else 
        {
            spriteRenderer.flipX = moveX != 0f ? moveX > 0f ? false : true : spriteRenderer.flipX;
        }
    }
}

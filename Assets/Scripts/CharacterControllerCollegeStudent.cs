using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static PlayerConstats;

public class CharacterControllerCollegeStudent : MonoBehaviour
{
    #region Varbiable Initialization
    // Start is called before the first frame update
    public const float playerSpeed = 10.0f;
    public const float playerJumpForce = 12.0f;
    public const float scooterJumpForce = 12.0f;
    public const float ScooterSpeed = 20.0f;
    private Rigidbody2D rigidbody;
    private bool Jumping = false;
    private bool JumpingEnabled = true;
    private bool _scooting = false;
    public bool Scooting
    {
        get { return _scooting; }
        private set { _scooting = value; }
    }
    public Animator animator;
    private AudioSource SFXAudioSource;
    private float groundRayLength = 1f;
    public LayerMask groundLayer;
    public bool grounded = true;
    private Quaternion currentRotation;
    public float maxRaycastDistance = 1.0f;
    private bool IsRotated = false;
    private bool IsMovingForward = true;
    private Vector3 startingPosition { get; set; }
    #endregion
    #region Events
    public Action<PlayerMovementTypes> PlayerMovementChanged;
    #endregion
    #region Start
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        SFXAudioSource = GetComponent<AudioSource>();
        Jumping = false;
        Scooting = false;
        
        BoxCollider2D characterCollider = GetComponent<BoxCollider2D>();
        PhysicsMaterial2D characterPhysicsMaterial = new PhysicsMaterial2D();
        characterPhysicsMaterial.friction = 1f;
        characterCollider.sharedMaterial = characterPhysicsMaterial;
        startingPosition = transform.position;
    }
    #endregion
    #region Update
    // Update is called once per frame
    void Update()
    {
        #region Movement 
        // Movement
        var movement = Input.GetAxis("Horizontal");
        var speed = Scooting ? ScooterSpeed : playerSpeed;
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        if (movement > 0 && !IsMovingForward)
        {
            FlipCharacter();
        }
        else if (movement < 0 && IsMovingForward)
        {
            FlipCharacter();
        }

        if(movement!= 0 && !Jumping)
        {
            animator.SetBool("isRun", true);
            SendNewPlayerMovement(PlayerMovementTypes.Running);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        #endregion

        #region Jumping
        // Jumping
        if (Input.GetButtonDown("Jump") && JumpingEnabled)
        {
            if(Scooting)
            {
                rigidbody.AddForce(new Vector2(0, scooterJumpForce), ForceMode2D.Impulse);
            }
            else
            {
                rigidbody.AddForce(new Vector2(0, playerJumpForce), ForceMode2D.Impulse);

            }
            Jumping = true;
            animator.SetBool("isJump", true);
            animator.SetBool("isRun", false);
            SendNewPlayerMovement(PlayerMovementTypes.Jumping);
        }
        else
        {
            Jumping = false;
        }
        #endregion
        #region Scooter
        // Scooter
        if (Input.GetButton("Scooter"))
        {
            Scooting = true;
            animator.SetBool("isScooter", true);
            SendNewPlayerMovement(PlayerMovementTypes.Scooting);
        }
        else
        {
            if(Scooting)
            {
                Scooting = false;
            }
            animator.SetBool("isScooter", false);
        }
        #endregion
        CharacterSlope(beginOfSlope: new Vector2(9f, -4f), endOfSlope: new Vector2(26f, 4.6f), Rotate90: false);

    }
    private void FlipCharacter()
    {     
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        IsMovingForward = !IsMovingForward;
    }
    #endregion

    #region OnEnable 
    private void OnEnable()
    {
        //Subscribing to the Event which handles what happens if the player touches the world border (for ex. falling out of the world)
        PlayerDeathEvents.TouchingWorldBorder += TeleportPlayer;
    }
    private void OnDisable()
    {
        PlayerDeathEvents.TouchingWorldBorder -= TeleportPlayer;
    }
    #endregion

    #region Collision Handling
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Ground")
        {
            Debug.Log("Grounded");
            JumpingEnabled = true;
            Jumping = false;
            animator.SetBool("isJump", false);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            JumpingEnabled = false;
        }
    }
    #endregion
    #region Sound Effects
    public void PlaySFX(AudioClip sfx)
    {
        SFXAudioSource.clip = sfx;
        SFXAudioSource.Play();
    }
    #endregion
    private void CharacterSlope(Vector2 beginOfSlope, Vector2 endOfSlope, bool Rotate90)
    {
        Vector2 playerPosition = transform.position; // E
        Vector2 positionInGroundBelowPlayer = new Vector2(playerPosition.x, beginOfSlope.y); // F
        Vector2 pointContainingAlpha = new Vector2(endOfSlope.x, beginOfSlope.y); // D
        Vector2 hypothenuseVector = playerPosition - pointContainingAlpha;
        double hypothenuse = hypothenuseVector.magnitude;
        Vector2 oppositeCatheteVector = playerPosition - positionInGroundBelowPlayer;
        double oppositeCathete = oppositeCatheteVector.magnitude; // Gegenkathete
        double sinusAlpha = oppositeCathete / hypothenuse;
        float angleToPlayerInRadians =  Mathf.Asin(Convert.ToSingle(sinusAlpha));
        float angleToPlayerInDegrees = angleToPlayerInRadians *(180/Mathf.PI);
        int roundedAngleToPlayer = Mathf.CeilToInt(angleToPlayerInDegrees);

        {
            if (roundedAngleToPlayer > 45 && roundedAngleToPlayer <= 90 && !IsRotated && hypothenuse <17) 
            {
                if(roundedAngleToPlayer== 90 && !Rotate90)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 26.666f);
                    IsRotated = true;
                }
                transform.rotation = Quaternion.Euler(0f, 0f, 26.666f);
                IsRotated = true;
            }
            else if (roundedAngleToPlayer >3 && roundedAngleToPlayer < 45 && !IsRotated && hypothenuse < 17)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 26.666f);
                IsRotated = true;
            }
            else if ((roundedAngleToPlayer <= 3 || roundedAngleToPlayer >= 89 || hypothenuse >=17) && IsRotated || Rotate90)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                IsRotated = false;
            }
        }
    }
    private void TeleportPlayer()
    {
        transform.position = startingPosition;
    }
    #region EventHandlers


    private void SendNewPlayerMovement(PlayerMovementTypes newAction)
    {
        PlayerMovementChanged.Invoke(newAction);
    }
    #endregion
}

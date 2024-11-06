using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private Rigidbody rb;
    public float MovementSpeed = 5.0f;
    private Vector2 movementInput;
    private Vector2 nowDirection;

    private Animator animator;

    private bool isShooting = false;
    private bool isRolling = false;

    private float rollSpeed = 7.5f;
    private float rollDuration = 1f;

    public GameObject bulletPrefab;
    public Transform gunBarrel;          
    public float bulletSpeed = 20f;      
    public static Controller obj; 

    private SFXController soundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        soundEffect = GetComponent<SFXController>();
    }

    void Update()
    {
        if (!isShooting && !isRolling) {
            UpdateDirection();
        }

        animator.SetFloat("Speed", rb.velocity.magnitude);

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void FixedUpdate()
    {
        if (!isShooting && !isRolling)
        {
            Vector3 move = new Vector3(nowDirection.x, 0, nowDirection.y).normalized;
            rb.velocity = move * MovementSpeed + Vector3.up * rb.velocity.y;

            if (move.magnitude > 0.1f)
            {
                transform.eulerAngles = new Vector3(0, Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.x, rb.velocity.z), 0);
            }
        }
        else if (isRolling)
        {
            // Keep rolling direction velocity active during roll
            rb.velocity = transform.forward * rollSpeed + Vector3.up * rb.velocity.y;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && !isShooting && !isRolling)
        {
            StartCoroutine(Shoot());
            rb.velocity = Vector3.zero;
        }
    }

    public void Rolling(InputAction.CallbackContext context)
    {
        if (context.performed && !isRolling)
        {
            StartCoroutine(Roll());
        }
    }

    private IEnumerator Roll()
    {
        isRolling = true;
        isShooting = true;  // Prevent shooting during roll

        Vector3 rollDirection;

        if (rb.velocity.magnitude > 0.1f)
        {
            rollDirection = rb.velocity.normalized;  
            transform.rotation = Quaternion.LookRotation(rollDirection);
        }
        else
        {
            rollDirection = -transform.forward;  
            transform.rotation = Quaternion.LookRotation(rollDirection);
        }

        rb.velocity = rollDirection * rollSpeed;

        animator.SetBool("Roll", true); 

        yield return new WaitForSeconds(rollDuration);

        animator.SetBool("Roll", false); 

        isRolling = false;
        isShooting = false;
    }

    private IEnumerator Shoot()
    {
        isShooting = true;

        rb.velocity = Vector3.zero;

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray castray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (playerPlane.Raycast(castray, out float enter))
        {
            Vector3 targetPosition = castray.GetPoint(enter);
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction);
        }

        animator.SetBool("IsShooting", true);
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = transform.forward * bulletSpeed;
        }
        soundEffect.PlayAttack();
        Destroy(bullet, 1.5f);
        yield return new WaitForSeconds(0.7f);

        nowDirection = Vector2.zero;

        animator.SetBool("IsShooting", false);

        isShooting = false;
    }

    #region Do not modify
    Vector2 Vec2Interpolation(Vector2 input_1, Vector2 input_2, float alpha)
    {
        return input_1 * (1 - alpha) + input_2 * alpha;
    }

    // Make your player move smoothly
    void UpdateDirection()
    {
        if (isShooting || isRolling)
            nowDirection = Vec2Interpolation(Vector2.zero, nowDirection, 0.4f);
        else
            nowDirection = Vec2Interpolation(movementInput, nowDirection, 0.4f);
    }
    #endregion
}

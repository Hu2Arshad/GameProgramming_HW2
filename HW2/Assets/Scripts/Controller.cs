using System.Collections;
using System.Collections.Generic;
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

    public GameObject bulletPrefab;
    public Transform gunBarrel;          
    public float bulletSpeed = 20f;      
    public static Controller obj; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!isShooting) {
            UpdateDirection();
        }

        animator.SetFloat("Speed", rb.velocity.magnitude);

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void FixedUpdate()
    {
        if (!isShooting) {
            Vector3 move = new Vector3(nowDirection.x, 0, nowDirection.y).normalized;
            rb.velocity = move * MovementSpeed + Vector3.up * rb.velocity.y;

            if (move.magnitude > 0.1f)
            {
                transform.eulerAngles = new Vector3(0, Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.x, rb.velocity.z), 0);
            }
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
        if (context.performed && !isShooting)
        {
            StartCoroutine(Shoot());
            rb.velocity = Vector3.zero;
        }
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
        if (isShooting)
            nowDirection = Vec2Interpolation(Vector2.zero, nowDirection, 0.4f);
        else
            nowDirection = Vec2Interpolation(movementInput, nowDirection, 0.4f);
    }
    #endregion
}

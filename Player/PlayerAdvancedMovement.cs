using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdvancedMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private float jumpForce = 7f;
    public CapsuleCollider capsuleCollider;
   
    public LayerMask ground;
    public float maxGroundAngle = 180;
    public bool debug;

    float groundAngle;
    Vector3 forward;
    RaycastHit hitInfo;
    bool grounded;

    public float velocity = 5;
    public float turnSpeed = 10;
    public float height = 0.5f;
    public float heightPadding = 0.05f;

    float angle;

    float x;
    float y;
    Transform cam;
    Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main.transform;

        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return (Physics.CheckCapsule(capsuleCollider.center, new Vector3(capsuleCollider.bounds.center.x,
            capsuleCollider.bounds.min.y, capsuleCollider.center.z), capsuleCollider.radius * .9f, ground));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();
        DrawDebugLines();

        if ((Mathf.Abs(x) < 1) && (Mathf.Abs(y) < 1)) return;

        if (!playerIsAttacking())
        {

            Move();
            Rotate();

        }


    }

    void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(x, y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    void Rotate()
    {

        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void Move()
    {
        if (groundAngle >= maxGroundAngle) return;
        transform.position += forward * velocity * Time.deltaTime;
    }

    void CalculateForward()
    {
        if (!grounded)
        {
            forward = transform.forward;
            return;
        }

        forward = Vector3.Cross(hitInfo.normal, -transform.right);
    }

    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(hitInfo.normal, transform.right);
    }

    void CheckGround()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, height + heightPadding, ground))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void ApplyGravity()
    {
        if (!grounded)
        {
            transform.position += Physics.gravity * Time.deltaTime;
        }
    }

    void DrawDebugLines()
    {
        Debug.DrawLine(transform.position, transform.position + forward * height * 2, Color.blue);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.green);
    }

    bool playerIsAttacking()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack1")
    || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack4")
    || anim.GetCurrentAnimatorStateInfo(0).IsName("2Hand-Sword-Attack6")
    ))
        {

            return true;
        }
        else
        {
            return false;
        }
    }
}
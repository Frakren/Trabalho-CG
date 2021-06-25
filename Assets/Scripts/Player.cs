using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject ViewCamera;
    public float speed, jumpForce, groundRange;
    private new Rigidbody rigidbody;
    public Animator animator;
    void Start()
    {
        GameManager.main.AtualizarDados();
        rigidbody = GetComponent<Rigidbody>();
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),out hit, groundRange, LayerMask.GetMask("Ground") | LayerMask.GetMask("Platform")))
        {
            if(hit.collider.GetComponent<Animator>())
            {
                hit.collider.GetComponent<Animator>().SetTrigger("Voltar");
                Destroy(hit.collider.GetComponent<Animator>());
            }
        }
    }
    private void Update()
    {
        SetMove();
    }
    private void FixedUpdate()
    {
        SetJump();
        if (ViewCamera != null)
        {
            Vector3 direction = (Vector3.up * 2 + Vector3.back) * 2;
            RaycastHit hit;
            Debug.DrawLine(transform.position, transform.position + direction, Color.red);
            ViewCamera.transform.position = transform.position + direction;
            ViewCamera.transform.LookAt(transform.position);
        }
    }
    // Start is called before the first frame update
    public void SetJump()
    {
        if (IsGrounded() && Input.GetKey(KeyCode.Space))
        {
            AddForceJump();
        }
        else
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
        }
    }
    public void AddForceJump()
    {
        rigidbody.velocity = Vector3.up * jumpForce;
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), groundRange, LayerMask.GetMask("Ground") | LayerMask.GetMask("Platform"));
    }
    public void SetMove()
    {
        float Xmove = Input.GetAxisRaw("Horizontal");
        float Ymove = Input.GetAxisRaw("Vertical");
        Vector3 vector = new Vector3(Xmove, 0, Ymove).normalized;
        //rigidbody.velocity = new Vector3(vector.x * speed, rigidbody.velocity.y, vector.z * speed);
        if (vector.magnitude > 0.1f)
        {
            if (IsGrounded())
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
                animator.SetBool("Jump", false);
            }
            float angle = Mathf.Atan2(vector.x, vector.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = rotation;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            rigidbody.velocity = new Vector3(dir.x * speed, rigidbody.velocity.y, dir.z * speed);
        }
        else
        {
            if (IsGrounded())
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Run", false);
                animator.SetBool("Jump", false);
            }
        }
    }
}

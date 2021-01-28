using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private fieldOfView fieldOfView;
    [SerializeField] private fieldOfView1 fieldOfView1;

    public float walkSpeed;
    public float runSpeed;
    public float silentSpeed;
    float currentSpeed;

    Rigidbody2D rb;
    public Camera cam;

    Vector2 mousePos;
    Vector2 movement;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = silentSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        fieldOfView.SetAimDirection(lookDir);
        fieldOfView.SetOrigin(transform.position);
        fieldOfView1.SetAimDirection(lookDir);
        fieldOfView1.SetOrigin(transform.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        if (!rb.freezeRotation)
        {
            rb.rotation = angle;
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        }
    }
}

//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    public float speed = 5f;
//    public Rigidbody2D rb;
//    public Camera cam;

//    Vector2 movement;
//    Vector2 mousePos;

//    void Update()
//    {
//        movement.x = Input.GetAxisRaw("Horizontal");
//        movement.y = Input.GetAxisRaw("Vertical");

//        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
//    }

//    private void FixedUpdate()
//    {
//        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

//        Vector2 lookDir = mousePos - rb.position;
//        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
//        rb.rotation = angle;
//    }
//}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    public Joystick leftJoystick;
    public Joystick rightJoystick;

    public bool isMobileControl = false;

    private Vector2 movement;
    private Vector2 mousePos;


    void Update()
    {
        if (isMobileControl)
        {
            // Get joystick input for movement
            movement = leftJoystick.GetInput();

            // Get joystick input for rotation
            Vector2 lookDirection = rightJoystick.GetInput();
            if (lookDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
            }
        }
        else
        {
            // Get keyboard input for movement
            float moveX = Input.GetAxisRaw("Horizontal"); // -1, 0, or 1
            float moveY = Input.GetAxisRaw("Vertical");   // -1, 0, or 1
            movement = new Vector2(moveX, moveY).normalized;

            // Get mouse input for rotation
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        // Calculate new position
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        // Apply the clamped position
        rb.MovePosition(newPosition);

        // Rotate the player toward the mouse for PC controls
        if (!isMobileControl)
        {
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }
}



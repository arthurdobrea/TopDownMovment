using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Camera camera;
    public Plane plane;
    public CharacterController characterController;

    public float finalSpeed;
    public float runSpeed = 3;
    public float moveSpeed = 1.5f;
    private Vector3 finalVelocity;
    private Vector3 input;
    private Vector3 correction;
    private float gravity = -16;
    private Actions actions;
    private float run = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        plane = new Plane(Vector3.up, Vector3.zero);
        characterController = GetComponent<CharacterController>();
        actions = GetComponent<Actions>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                finalSpeed = runSpeed;
                actions.Run();
            }
            else
            {
                finalSpeed = moveSpeed;
                actions.Walk();
            }
        }

        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) ||
                 Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.C))
        {
            actions.Stay();
        }

        input = new Vector3(Input.GetAxisRaw("Horizontal"), gravity * Time.deltaTime, Input.GetAxisRaw("Vertical"));
        finalVelocity = input.normalized * finalSpeed;


        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        float rayDistance;

        if (plane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            lookAt(point);
        }
    }

    void lookAt(Vector3 point)
    {
        correction = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(correction);
        transform.Rotate(0, Input.GetAxisRaw("Horizontal"), 0);
    }

    void FixedUpdate()
    {
        characterController.Move(finalVelocity * Time.deltaTime);
    }
}
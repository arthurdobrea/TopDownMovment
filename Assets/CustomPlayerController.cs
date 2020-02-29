using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Camera camera;
    public Plane plane;
    public CharacterController characterController;

    private float moveSpeed = 3;
    private Vector3 finalVelocity;
    private Animator animator;
    private Vector3 input;
    private Vector3 correction;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); 
        plane = new Plane(Vector3.up,Vector3.zero);
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    { 
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        finalVelocity = input.normalized * moveSpeed;
        
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        
        if (plane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin,point,Color.red);
            lookAt(point);
        }
    }

    void lookAt(Vector3 point)
    { 
        correction = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(correction);
    }

    void FixedUpdate()
    {
        characterController.Move(finalVelocity * Time.deltaTime);
    }
}
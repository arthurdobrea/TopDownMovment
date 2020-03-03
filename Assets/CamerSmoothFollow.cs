using UnityEngine;

public class CamerSmoothFollow : MonoBehaviour
{
    public GameObject player;

    public float smoothSpeed = 0.5f;
    public Vector3 offset;

    private float mouseX;
    private int mouseY;
    private int speedOfRotation = 10;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            mouseX = +speedOfRotation * Input.GetAxis("Mouse X");
            Debug.Log(Input.GetAxis("Mouse X"));
            
            transform.LookAt(player.transform);
        }
        else
        {
            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(player.transform);
        }
    }
}
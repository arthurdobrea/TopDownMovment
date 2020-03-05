using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouseSeparateScript : MonoBehaviour
{
    public Camera camera;

    private Vector3 correction;
    private Plane plane;
    // Start is called before the first frame update
    void Start()
    {
        plane =  new Plane(Vector3.up, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        rotateTowardsMouseLocalCoordinate();
    }
    
    private void rotateTowardsMouseLocalCoordinate()
    {
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
    }
}

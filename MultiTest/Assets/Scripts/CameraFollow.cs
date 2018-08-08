using UnityEngine;

public class CameraFollow : MonoBehaviour {
    //Variables
    public Transform target;

    public float smoothSpeed = 10f;
    public Vector3 offset;
    public Vector3 offsetRotation = new Vector3(70f,0f,0f);
    //Functions
    private void LateUpdate()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);


        transform.position = smoothedPosition;
        transform.rotation = Quaternion.Euler(offsetRotation);
    }

}

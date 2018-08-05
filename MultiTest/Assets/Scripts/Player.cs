using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class Player : MonoBehaviour {
    //Variables
    [SerializeField]
    private float movementSpeed;

    public Camera camera;
    private PlayerMotor motor;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }
    //Functions
    private void Update()
    {
        //Player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        float hitDistance = 0.0f;

        if(playerPlane.Raycast(ray, out hitDistance))
        {
            Vector3 targetPoint = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);

        }

        //Player movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * movementSpeed;

        motor.Move(moveVelocity);
    }
   

}

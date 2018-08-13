using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class Player : MonoBehaviour {
    //Variables
    [SerializeField]
    private float movementSpeed;

    public Camera camera;
    private PlayerMotor motor;
    private ConfigurableJoint joint;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    [SerializeField]
    private float thrusterForce = 1000f;

    [SerializeField]
    private float thrusterFuelBurnSpeed = .5f;

    [SerializeField]
    private float thrusterFuelRegenSpeed = 0.3f;
    private float thrusterFuelAmount = 1f;

    public float GetThrusterFuelAmount()
    {
        return thrusterFuelAmount;
    }

    [SerializeField]
    private LayerMask environomentMask;

    [Header("Spring settings:")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;



    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
    }
    //Functions

    private void Update()
    {
        //Setting targetposition for spring
        //This makes the physics act right wen it comes to applying gravity when flying over objects
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down,out _hit, 100f,environomentMask))
        {
            joint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
        }
        else
        {
            joint.targetPosition = new Vector3(0f, 0f, 0f);
        }


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


        //Calculate the thrusterforce based on player input
        Vector3 _thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump") && thrusterFuelAmount > 0f)
        {
            thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;

            if(thrusterFuelAmount > 0.01f)
            {
                _thrusterForce = Vector3.up * thrusterForce;
                SetJointSettings(0f);
            }
            
        }
        else
        {
            thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;
            SetJointSettings(jointSpring);
        }

        thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0f, 1f);

        //Apply the thruster force
        motor.ApplyThruster(_thrusterForce);

    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };

    }

}

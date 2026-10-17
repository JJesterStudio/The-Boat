using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Boat : MonoBehaviour
{
    [SerializeField]
    private Sail Sail;
    [SerializeField]
    private UIEvents UI;
    [SerializeField]
    private Transform Steer;

    private const float rotationSpeed = 1.0f;
    private const float unfurlingSpeed = 0.01f;
    private const float steeringSpeed = 1.0f;

    private float sailPercentage = 1f;
    private float sailAngle = 0f;
    private float steerAngle = 0f;
    private float rotateInput;
    private float unfurlInput;
    private float steerInput;

    private float acceleration;
    private float speed = 0f;

    public float windCoefficient = 0.001f;
    public float dragCoefficient = 0.05f;
    public float mass = 10f;
    public float maxSpeed = 0.15f;

    private void Update()
    {
        sailAngle = Mathf.Clamp(sailAngle + rotateInput, -45f, 45f);
        sailPercentage = Mathf.Clamp(sailPercentage + unfurlInput, 0f, 1f);
        steerAngle = Mathf.Clamp(steerAngle + steerInput, -80f, 80f);     
    }

    private void FixedUpdate()
    {
        RotateSail();
        UnfurlSail();
        SteerBoat();
        MoveBoat();
    }

    private void MoveBoat()
    {
        float windAngle = Vector3.Angle(transform.forward, Wind.direction);
        float windEfficiency = Mathf.Abs(Mathf.Sin(windAngle * Mathf.Deg2Rad));
        float sailEfficiency = Mathf.Clamp01(Mathf.Cos((Mathf.Abs(windAngle) / 2f - Mathf.Abs(sailAngle)) * Mathf.Deg2Rad));
        float windForce = windCoefficient * Wind.direction.magnitude * Wind.direction.magnitude * sailPercentage * windEfficiency * sailEfficiency;
        float dragForce = dragCoefficient * speed;
        acceleration = (windForce - dragForce) / mass;
        speed += acceleration;
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
        transform.Rotate(Vector3.up * speed * steerAngle / 10f);
        transform.position += transform.forward * speed;
    }

    private void RotateSail()
    {
        Sail.transform.localEulerAngles = new Vector3(0.0f, sailAngle, 0.0f);
        UI.SetSailsRotation(sailAngle.ToString());
    }

    private void UnfurlSail()
    {
        Sail.SetSailPercentage(sailPercentage);
        UI.SetSailsUnfurling((sailPercentage * 100f).ToString());
    }

    private void SteerBoat()
    {
        Steer.localEulerAngles = new Vector3(0.0f, -steerAngle, 0.0f);
    }

    public void RotateSailEvent(CallbackContext context)
    {
        rotateInput = context.ReadValue<float>() * rotationSpeed;
    }

    public void UnfurlSailEvent(CallbackContext context)
    {
        unfurlInput = context.ReadValue<float>() * unfurlingSpeed;
    }

    public void SteerEvent(CallbackContext context)
    {
        steerInput = context.ReadValue<float>() * steeringSpeed;
    }
}

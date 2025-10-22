using UnityEngine;

public class VehicleControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float acceleration = 2.0f;
    public float maxSpeed = 50.0f;
    public float turnSpeed = 100.0f;
    
    private float currentSpeed;
    private Rigidbody rb;
    private MeshCollider meshCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
        
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        rb.mass = 10000f; // 차량의 질량 증가
        rb.centerOfMass = new Vector3(0, -2.0f, 0); // 무게 중심을 더 낮추기
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;
        }
        
        AddTriggerCollider();
        SetVehicleActive(false);
    }

    void Update()
    {
        if (rb.isKinematic) return;
        HandleMovement();
    }

    void FixedUpdate()
    {
        // 최대 속도 제한
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void HandleMovement()
    {
        float moveForward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = speed;
        }
        
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        
        Vector3 movement = transform.forward * moveForward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
        
        float rotation = turn * turnSpeed * Time.deltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void SetVehicleActive(bool isActive)
    {
        rb.isKinematic = !isActive;
    }

    void AddTriggerCollider()
    {
        GameObject triggerObject = new GameObject("TriggerCollider");
        triggerObject.transform.SetParent(transform);
        triggerObject.transform.localPosition = Vector3.zero;
        
        BoxCollider triggerCollider = triggerObject.AddComponent<BoxCollider>();
        triggerCollider.size = new Vector3(2.0f, 2.0f, 4.0f);
        triggerCollider.isTrigger = true;
        
        Rigidbody triggerRb = triggerObject.AddComponent<Rigidbody>();
        triggerRb.isKinematic = true;
    }
}
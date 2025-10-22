using UnityEngine;

public class VehicleInteraction : MonoBehaviour
{
    public GameObject player;
    public Transform vehicleSeat;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float enterDistance = 2.0f;
    
    private bool isDriving = false;
    private VehicleControl vehicleControl;
    private ManMove playerMovement;

    void Start()
    {
        vehicleControl = GetComponent<VehicleControl>();
        if (player != null)
        {
            playerMovement = player.GetComponent<ManMove>();
        }
        
        if (firstPersonCamera != null)
            firstPersonCamera.enabled = false;
    }

    void Update()
    {
        if (isDriving)
        {
            // Space 키로 하차
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExitVehicle();
            }
        }
        else
        {
            // E 키로 탑승
            if (player != null && Vector3.Distance(player.transform.position, transform.position) < enterDistance && Input.GetKeyDown(KeyCode.E))
            {
                EnterVehicle();
            }
        }
    }

    void EnterVehicle()
    {
        isDriving = true;
        
        // 플레이어 이동 비활성화
        if (playerMovement != null)
            playerMovement.enabled = false;
        
        // 플레이어를 차량 좌석 위치로 이동
        if (vehicleSeat != null)
        {
            player.transform.position = vehicleSeat.position;
            player.transform.rotation = vehicleSeat.rotation;
        }
        
        // 카메라 전환: 3인칭 -> 1인칭
        if (thirdPersonCamera != null)
            thirdPersonCamera.enabled = false;
        if (firstPersonCamera != null)
            firstPersonCamera.enabled = true;
        
        // 차량 제어 활성화
        if (vehicleControl != null)
            vehicleControl.SetVehicleActive(true);
        
        // 플레이어 모델 숨기기
        Renderer playerRenderer = player.GetComponent<Renderer>();
        if (playerRenderer != null)
            playerRenderer.enabled = false;
    }

    void ExitVehicle()
    {
        isDriving = false;
        
        // 플레이어를 차량 옆으로 이동
        player.transform.position = transform.position + transform.right * 2;
        
        // 카메라 전환: 1인칭 -> 3인칭
        if (firstPersonCamera != null)
            firstPersonCamera.enabled = false;
        if (thirdPersonCamera != null)
            thirdPersonCamera.enabled = true;
        
        // 차량 제어 비활성화
        if (vehicleControl != null)
            vehicleControl.SetVehicleActive(false);
        
        // 플레이어 이동 활성화
        if (playerMovement != null)
            playerMovement.enabled = true;
        
        // 플레이어 모델 표시
        Renderer playerRenderer = player.GetComponent<Renderer>();
        if (playerRenderer != null)
            playerRenderer.enabled = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enterDistance);
    }
}
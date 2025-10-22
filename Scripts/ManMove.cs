using UnityEngine;

public class ManMove : MonoBehaviour
{
    CharacterController cc;
    Vector3 dir;
    Animator ani;
    
    public float moveSpeed = 3f;
    public float runSpeed = 6f;
    public float walkSpeed = 3f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        ani = GetComponentInChildren<Animator>();
        
        if (cc == null)
        {
            Debug.LogError("CharacterController가 연결되지 않았습니다.");
        }
        
        if (ani == null)
        {
            Debug.LogError("Animator가 연결되지 않았습니다.");
        }
    }

    void Update()
    {
        // 사용자 입력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        dir = new Vector3(h, 0, v);
        dir = dir.normalized * moveSpeed;
        
        // 애니 설정
        bool isMove = dir.magnitude > 0;
        ani.SetFloat("BlendSpeed", dir.magnitude);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed; // 달리기 속도
        }
        else
        {
            moveSpeed = walkSpeed; // 걷기 속도
        }
        
        // 방향
        if (dir.x != 0 || dir.z != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }
        
        // 중력 적용
        dir.y += gravity * Time.deltaTime;
        
        // 점프
        if (cc.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                dir.y = jumpForce;
            }
        }
        
        // 이동
        cc.Move(dir * Time.deltaTime);
    }
}
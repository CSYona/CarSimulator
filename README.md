# 자동차 시뮬레이션 게임

Unity 3D로 제작한 자동차 운전 시뮬레이션 게임

플레이어는 맵을 자유롭게 걸어다니다가 자동차에 탑승하여 1인칭 시점으로 운전할 수 있다.











![com-speed](C:\Users\yonac\Downloads\carsimulator-ezgif.com-speed.gif)









## 목차

* [프로젝트 소개 ](#프로젝트-소개)
* [주요 기능](#주요 기능)
* [프로젝트 구조](#프로젝트 구조)
* [조작 방법](#조작 방법)
* [스크립트 설명](#스크립트 설명)
* [문제 해결](#문제 해결)







## 프로젝트 소개



* **인칭 캐릭터 이동**: 플레이어가 농장 맵을 자유롭게 탐험
* **차량 탑승 시스템**: 자동차 근처에서 상호작용하여 탑승
* **시점 자동 전환**: 탑승 시 3인칭 → 1인칭, 하차 시 1인칭 → 3인칭
* **물리 기반 주행**: Rigidbody를 활용한 리얼한 차량 주행 시뮬레이션







## 주요 기능



1. 플레이어 시스템 (3인칭 시점)
* WASD 키를 이용한 자유로운 이동
* Shift 키를 누르면 달리기
* Space 키로 점프
* 애니메이션 블렌딩 지원 (걷기/달리기)
* Character Controller 기반 물리 적용



2. 차량 상호작용 시스템
* 플레이어가 자동차 근처에 접근하면 탑승 가능
* **E 키**를 눌러 자동차 탑승
* 탑승 시 자동으로 3인칭 → 1인칭 시점 전환
* **Space 키**를 눌러 자동차에서 하차
* 하차 시 자동으로 1인칭 → 3인칭 시점 전환
* 하차 위치는 차량 옆으로 자동 배치



3. 차량 주행 시스템 (1인칭 시점)
* W/S 키로 전진/후진
* A/D 키로 좌우 회전
* Shift 키로 가속 (점진적 속도 증가)
* 물리 기반 차량 움직임 (Rigidbody)
* 최대 속도 제한 기능
* 충돌 감지 및 처리 (장애물과 충돌 시 정지)



4. 카메라 시스템
* 3인칭 카메라: 플레이어를 부드럽게 추적
* 1인칭 카메라: 운전자 시점 제공
* 시점 전환 시 자연스러운 카메라 전환
  
  
  
  

## 프로젝트 구조



CarSimulator/
│
│── Scripts/
│       ├── ManMove.cs              # 플레이어 3인칭 이동 및 애니메이션
│       ├── VehicleControl.cs       # 차량 조종 및 물리 시뮬레이션
│       ├── VehicleInteraction.cs   # 탑승/하차 시스템 및 카메라 전환
│       └── CameraFollow.cs         # 3인칭 카메라 부드러운 추적
│
└── README.md                       



## 조작 방법

### 플레이어 모드 (3인칭 시점)

| 키     | 동작              |
| ----- | --------------- |
| W     | 전진              |
| S     | 후진              |
| A     | 왼쪽 이동           |
| D     | 오른쪽 이동          |
| Shift | 달리기             |
| Space | 점프              |
| E     | 차량 탑승 (차량 근처에서) |



### 차량 모드 (1인칭 시점)

| 키     | 동작      |
| ----- | ------- |
| W     | 전진      |
| S     | 후진      |
| A     | 왼쪽 회전   |
| D     | 오른쪽 회전  |
| Shift | 가속      |
| Space | 차량에서 하차 |





## 스크립트 설명



1. `ManMove.cs` - 플레이어 이동 제어

**주요 기능:**

* Character Controller 기반 이동 시스템
* WASD 키 입력 처리
* 걷기/달리기 속도 전환
* 점프 기능 (중력 적용)
* 애니메이션 블렌딩 (BlendSpeed 파라미터)



주요 변수:

```
public float moveSpeed = 3f;      // 기본 이동 속도
public float runSpeed = 6f;       // 달리기 속도
public float walkSpeed = 3f;      // 걷기 속도
public float jumpForce = 5f;      // 점프 힘
public float gravity = -9.81f;    // 중력 가속도
```



2. `VehicleControl.cs` - 차량 조종

**주요 기능:**

* Rigidbody 기반 물리 시뮬레이션
* 전진/후진 및 회전 처리
* 가속 시스템 (Shift 키)
* 최대 속도 제한
* 충돌 감지 및 처리
* 트리거 콜라이더 자동 생성



주요 변수:

```
public float speed = 10.0f;           // 기본 속도
public float acceleration = 2.0f;      // 가속도
public float maxSpeed = 50.0f;         // 최대 속도
public float turnSpeed = 100.0f;       // 회전 속도
```



**핵심 메서드:**

* `SetVehicleActive(bool)`: 차량 활성화/비활성화
* `AddTriggerCollider()`: 탑승용 트리거 콜라이더 생성
3. `VehicleInteraction.cs` - 탑승/하차 시스템
   
   

**주요 기능:**

* 플레이어와 차량 간 거리 감지
* E 키 탑승, Space 키 하차
* 카메라 시점 전환 (3인칭 ↔ 1인칭)
* 플레이어 이동 스크립트 활성화/비활성화
* 차량 제어 활성화/비활성화
  
  

주요 변수:

```
public GameObject player;              // 플레이어 오브젝트
public Transform vehicleSeat;          // 좌석 위치
public Camera firstPersonCamera;       // 1인칭 카메라
public Camera thirdPersonCamera;       // 3인칭 카메라
public float enterDistance = 2.0f;     // 탑승 가능 거리
```



**핵심 메서드:**

* `EnterVehicle()`: 탑승 처리
* `ExitVehicle()`: 하차 처리
4. `CameraFollow.cs` - 3인칭 카메라 추적
   
   

**주요 기능:**

* 타겟(플레이어)을 부드럽게 추적
* Lerp를 이용한 자연스러운 이동
* LookAt으로 항상 타겟을 바라봄
  
  

주요 변수:

```
public Transform target;              // 추적할 대상
public Vector3 offset;                // 카메라 오프셋
public float smoothSpeed = 0.125f;    // 부드러움 정도
```







## 문제 해결



### "There are 2 audio listeners in the scene"

**원인:** Main Camera와 FirstPersonCamera에 Audio Listener가 둘 다 있음

**해결:**

1. FirstPersonCamera 선택
2. Audio Listener 컴포넌트 제거 (Remove Component)
   
   

------------------------------------------



### 차량이 지면을 뚫고 내려감

**원인:** 충돌 감지 문제 또는 Rigidbody 설정 문제

**해결:**

1. Terrain에 Terrain Collider가 있는지 확인
2. 차량의 Rigidbody Mass를 10000으로 증가
3. VehicleControl.cs에서 `rb.centerOfMass`가 낮게 설정되어 있는지 확인
4. 차량의 Mesh Collider에서 Convex 옵션 활성화
   
   

---------------------------------------------



### 차량 탑승이 안됨

**원인:** 태그 설정 오류 또는 거리 문제

**해결:**

1. Player 오브젝트의 태그가 "Player"인지 확인
2. Vehicle 오브젝트의 태그가 "Vehicle"인지 확인
3. VehicleInteraction의 Enter Distance 값을 더 크게 조정 (예: 3.0)
4. Scene 뷰에서 노란색 와이어 구체(Gizmo)를 확인하여 탑승 범위 시각화
5. Console 창에서 에러 메시지 확인
   
   

-------------------------------------------------



### 카메라가 이상하게 움직임

**원인:** CameraFollow 설정 문제

**해결:**

1. Smooth Speed 값 조정 (0.05 ~ 0.2 사이)
2. Offset 값 재조정 (Y 값을 높이면 위에서 내려다보는 시점)
3. 카메라가 플레이어의 자식이 아닌지 확인 (독립된 오브젝트여야 함)
   
   

---------------------------------------------------------



### 애니메이션이 작동하지 않음

**원인:** Animator 설정 문제

**해결:**

1. 캐릭터 모델에 Animator 컴포넌트가 있는지 확인
2. Animator Controller에 "BlendSpeed" Float 파라미터 추가
3. BlendSpeed 값에 따라 Idle → Walk → Run 전환되도록 Blend Tree 설정
   
   

----------------------------------



### 차량이 쉽게 뒤집힘

**원인:** 무게 중심이 너무 높음

**해결:**

1. VehicleControl.cs의 Start() 메서드에서:

```
rb.centerOfMass = new Vector3(0, -2.0f, 0); // Y 값을 더 낮게
```



2. Rigidbody의 Mass를 더 증가 (15000 ~ 20000)



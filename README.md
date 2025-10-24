# 自動車シミュレーションゲーム

Unity 3Dで制作した自動車運転シミュレーションゲーム

プレイヤーはマップを自由に歩き回り、自動車に乗車して一人称視点で運転することができます。











## 目次

* [プロジェクト紹介](#プロジェクト紹介)
* [主な機能](#主な機能)
* [プロジェクト構造](#プロジェクト構造)
* [操作方法](#操作方法)
* [スクリプト説明](#スクリプト説明)
* [トラブルシューティング](#トラブルシューティング)
  
  
  
  
  
  

## プロジェクト紹介



* **三人称キャラクター移動**: プレイヤーが農場マップを自由に探索
* **車両乗車システム**: 自動車の近くでインタラクションして乗車
* **視点自動切り替え**: 乗車時 三人称 → 一人称、下車時 一人称 → 三人称
* **物理ベース走行**: Rigidbodyを活用したリアルな車両走行シミュレーション
  
  
  
  
  
  

## 主な機能



1. プレイヤーシステム（三人称視点）
* WASDキーによる自由な移動
* Shiftキーを押すとダッシュ
* Spaceキーでジャンプ
* アニメーションブレンディング対応（歩行/走行）
* Character Controllerベースの物理適用
2. 車両インタラクションシステム
* プレイヤーが自動車の近くに接近すると乗車可能
* **Eキー**を押して自動車に乗車
* 乗車時に自動的に三人称 → 一人称視点に切り替え
* **Spaceキー**を押して自動車から下車
* 下車時に自動的に一人称 → 三人称視点に切り替え
* 下車位置は車両の横に自動配置
3. 車両走行システム（一人称視点）
* W/Sキーで前進/後退
* A/Dキーで左右回転
* Shiftキーで加速（段階的な速度増加）
* 物理ベースの車両動作（Rigidbody）
* 最高速度制限機能
* 衝突検知と処理（障害物との衝突時に停止）
4. カメラシステム
* 三人称カメラ: プレイヤーをスムーズに追跡
* 一人称カメラ: ドライバー視点を提供
* 視点切り替え時の自然なカメラ遷移
  
  
  
  

## プロジェクト構造



<img width="922" height="360" alt="image" src="https://github.com/user-attachments/assets/bef640e4-3601-43f9-80c2-f3f1e121062c" />

## 操作方法

### プレイヤーモード（三人称視点）

| キー    | 動作               |
| ----- | ---------------- |
| W     | 前進               |
| S     | 後退               |
| A     | 左移動              |
| D     | 右移動              |
| Shift | ダッシュ             |
| Space | ジャンプ             |
| E     | 車両乗車（車両の近くにいる時） |



### 車両モード（一人称視点）

| キー    | 動作      |
| ----- | ------- |
| W     | 前進      |
| S     | 後退      |
| A     | 左回転     |
| D     | 右回転     |
| Shift | 加速      |
| Space | 車両から下車  |





## スクリプト説明



1. `ManMove.cs` - プレイヤー移動制御

**主な機能:**

* Character Controllerベースの移動システム
* WASDキー入力処理
* 歩行/走行速度切り替え
* ジャンプ機能（重力適用）
* アニメーションブレンディング（BlendSpeedパラメータ）
  
  

主な変数:

```
public float moveSpeed = 3f;      // 基本移動速度
public float runSpeed = 6f;       // 走行速度
public float walkSpeed = 3f;      // 歩行速度
public float jumpForce = 5f;      // ジャンプ力
public float gravity = -9.81f;    // 重力加速度
```



2. `VehicleControl.cs` - 車両操縦

**主な機能:**

* Rigidbodyベースの物理シミュレーション
* 前進/後退および回転処理
* 加速システム（Shiftキー）
* 最高速度制限
* 衝突検知と処理
* トリガーコライダー自動生成
  
  

主な変数:

```
public float speed = 10.0f;           // 基本速度
public float acceleration = 2.0f;      // 加速度
public float maxSpeed = 50.0f;         // 最高速度
public float turnSpeed = 100.0f;       // 回転速度
```



**主要メソッド:**

* `SetVehicleActive(bool)`: 車両アクティブ化/非アクティブ化
* `AddTriggerCollider()`: 乗車用トリガーコライダー生成
3. `VehicleInteraction.cs` - 乗車/下車システム
   
   

**主な機能:**

* プレイヤーと車両間の距離検知
* Eキー乗車、Spaceキー下車
* カメラ視点切り替え（三人称 ↔ 一人称）
* プレイヤー移動スクリプトのアクティブ化/非アクティブ化
* 車両制御のアクティブ化/非アクティブ化
  
  

主な変数:

```
public GameObject player;              // プレイヤーオブジェクト
public Transform vehicleSeat;          // 座席位置
public Camera firstPersonCamera;       // 一人称カメラ
public Camera thirdPersonCamera;       // 三人称カメラ
public float enterDistance = 2.0f;     // 乗車可能距離
```



**主要メソッド:**

* `EnterVehicle()`: 乗車処理
* `ExitVehicle()`: 下車処理
4. `CameraFollow.cs` - 三人称カメラ追跡
   
   

**主な機能:**

* ターゲット（プレイヤー）をスムーズに追跡
* Lerpを使用した自然な移動
* LookAtで常にターゲットを注視
  
  

主な変数:

```
public Transform target;              // 追跡対象
public Vector3 offset;                // カメラオフセット
public float smoothSpeed = 0.125f;    // スムーズさの度合い
```







## トラブルシューティング



### "There are 2 audio listeners in the scene"

**原因:** Main CameraとFirstPersonCameraの両方にAudio Listenerがある

**解決方法:**

1. FirstPersonCameraを選択
2. Audio Listenerコンポーネントを削除（Remove Component）
   
   

------------------------------------------



### 車両が地面を突き抜けて落ちる

**原因:** 衝突検知の問題またはRigidbody設定の問題

**解決方法:**

1. TerrainにTerrain Colliderがあるか確認
2. 車両のRigidbody Massを10000に増加
3. VehicleControl.csで`rb.centerOfMass`が低く設定されているか確認
4. 車両のMesh ColliderでConvexオプションを有効化
   
   

---------------------------------------------



### 車両に乗車できない

**原因:** タグ設定エラーまたは距離の問題

**解決方法:**

1. PlayerオブジェクトのタグがPlayerになっているか確認
2. VehicleオブジェクトのタグがVehicleになっているか確認
3. VehicleInteractionのEnter Distance値をより大きく調整（例: 3.0）
4. Scene Viewで黄色いワイヤー球体（Gizmo）を確認して乗車範囲を可視化
5. Consoleウィンドウでエラーメッセージを確認
   
   

-------------------------------------------------



### カメラが変な動きをする

**原因:** CameraFollow設定の問題

**解決方法:**

1. Smooth Speed値を調整（0.05～0.2の間）
2. Offset値を再調整（Y値を高くすると上から見下ろす視点になる）
3. カメラがプレイヤーの子オブジェクトになっていないか確認（独立したオブジェクトである必要がある）
   
   

---------------------------------------------------------



### アニメーションが動作しない

**原因:** Animator設定の問題

**解決方法:**

1. キャラクターモデルにAnimatorコンポーネントがあるか確認
2. Animator Controllerに「BlendSpeed」Floatパラメータを追加
3. BlendSpeed値に応じて Idle → Walk → Run に遷移するようにBlend Treeを設定
   
   

----------------------------------



### 車両が簡単に横転する

**原因:** 重心が高すぎる

**解決方法:**

1. VehicleControl.csのStart()メソッドで:

```
rb.centerOfMass = new Vector3(0, -2.0f, 0); // Y値をより低く
```



2. RigidbodyのMassをさらに増加（15000～20000）

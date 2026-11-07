using UnityEngine;
using UnityEngine.InputSystem;

public class DroneCamera : MonoBehaviour
{
    // === フィールド === //
    [System.Serializable]
    public class Parameter
    {   // Parameter クラス
        // カメラのオプション変数をまとめる内部クラス
        public Vector3 Position = new Vector3(0, 0, -10);   // 座標
        public Vector3 Angles = Vector3.zero;               // 角度
        public float Distance = 10;     // 対象との距離
        public float DistanceMin = 0;   // 対象との距離限界（最小値）
        public float DistanceMax = 10;  // 対象との距離限界（最大値）
        public float TiltMin = -90;     // チルト制限（最小値）
        public float TiltMax = 90;      // チルト制限（最大値）
        public Vector3 PositionOffset = Vector3.zero;
        public Vector3 AnglesOffset = Vector3.zero;
    }

    // === インスペクター表示用変数 === //
    public Parameter parameter;

    // === カメラ本体の設定 === //
    [Header("=== Camera Settings ===")]
    public Transform Joint;
    public Camera Camera;
    public Transform TrackingTarget;

    void Start()
    {
        
    }


    void Update()
    {
        // 追いかける
        parameter.Position = TrackingTarget.position;

        // マウス操作
        Vector2 inputAxis = Mouse.current.delta.value;
        parameter.Angles.x += inputAxis.y;
        parameter.Angles.y += inputAxis.x * -1;

        Vector2 wheel = Mouse.current.scroll.value;
        parameter.Distance += wheel.y;
    }


    void LateUpdate()
    {
        // === カメラの計算結果を代入
        transform.position = parameter.Position;

        parameter.Angles.x = Mathf.Clamp(
            parameter.Angles.x,
            parameter.TiltMin,
            parameter.TiltMax
            );
        transform.eulerAngles = parameter.Angles;

        parameter.Distance = Mathf.Clamp(
            parameter.Distance,
            parameter.DistanceMin,
            parameter.DistanceMax
            );
        Joint.localPosition = Vector3.forward * -parameter.Distance;

        Camera.transform.localPosition = parameter.PositionOffset;
        Camera.transform.localEulerAngles = parameter.AnglesOffset;
    }
}

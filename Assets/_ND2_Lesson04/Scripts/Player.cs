using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.wKey.IsPressed())
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }

        RaycastHit raycast;             // 光線の衝突情報
        // === ベクトルを可視化する
        // 原点から現在地のベクトル
        CreateAndShowOfRay(Vector3.zero, transform.position, out raycast, Color.red);

        // 移動ベクトル
        Vector3 moveVector = transform.position + new Vector3(0, 0, moveSpeed * Time.deltaTime);
        CreateAndShowOfRay(transform.position, moveVector, out raycast, Color.yellow);

        // 自分の真下に向いたベクトル
        Vector3 downVector = transform.position + new Vector3(0, -2, 0);
        CreateAndShowOfRay(transform.position, downVector, out raycast, Color.blue);

        // Raycastで衝突したメッシュの向き
        Vector3 normalVector = raycast.point + raycast.normal;
        CreateAndShowOfRay(raycast.point, normalVector, out raycast, Color.green);
    }


    /// <summary>
    /// ベクトルを可視化して、光線を飛ばすメソッド
    /// </summary>
    public void CreateAndShowOfRay( Vector3 start, Vector3 target, out RaycastHit hit, Color color )
    {
        // Vector3.Cross();     // 外積：結果がVector3型
        // Vector3.Dot();       // 内積：結果がfloat型

        // ベクトルの差分
        // 差分 = ベクトル2 - ベクトル1;
        Vector3 difference = target - start;

        // ベクトルの距離
        // 距離 = Mathf.Sqrt(((target.x - start.x) * (target.x - start.x)) + ((target.y - start.y) * (target.y - start.y)) + ((target.z - start.z) * (target.z - start.z)))
        float distance = Vector3.Distance(start, target);

        // ベクトルの可視化
        Debug.DrawRay(start, difference, color, 0, false);

        // 光線を飛ばす
        // Raycast(開始点, 向き, out 衝突情報, 距離, マスク );
        Physics.Raycast(start, difference, out hit, distance, LayerMask.GetMask("Terrain"));
    }
}

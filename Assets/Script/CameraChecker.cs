using UnityEngine;

public class CameraChecker : MonoBehaviour
{
    private bool _isInside = false; // 一度でも画面内に入ったかどうかのフラグ

    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        // 1. 画面内に入ったかどうかの判定
        if (!_isInside)
        {
            if (viewPos.x > 0f && viewPos.x < 1f && viewPos.y > 0f && viewPos.y < 1f)
            {
                _isInside = true; // 画面内に入った！
            }
        }
        else
        {
            // 2. 一度画面に入った後、画面の外に出たか判定
            // 敵は大きい場合があるので、少し余裕（-0.2f 〜 1.2f）を持たせると自然です
            if (viewPos.x < -0.2f || viewPos.x > 1.2f || viewPos.y < -0.2f || viewPos.y > 1.2f)
            {
                Destroy(gameObject);
            }
        }
    }
}
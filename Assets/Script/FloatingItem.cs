using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [Header("浮動設定")]
    public float floatAmplitude = 0.2f; // 飄浮的高度（上下移動範圍）
    public float floatFrequency = 2f;   // 飄浮的速度

    [Header("旋轉設定")]
    public float rotateSpeed = 50f;     // 旋轉速度（如果不想要旋轉可以設為 0）

    private Vector3 startPos;

    void Start()
    {
        // 紀錄鑰匙原本的位置
        startPos = transform.position;
    }

    void Update()
    {
        // 1. 飄浮邏輯：使用正弦波 (Mathf.Sin) 產生上下規律移動
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // 2. 旋轉邏輯：讓鑰匙慢慢轉動，增加立體感
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
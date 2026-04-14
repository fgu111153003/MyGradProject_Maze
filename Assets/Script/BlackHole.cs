using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [Header("傳送設定")]
    [Tooltip("請把場景中的安全空地(Empty Object)拖進來")]
    public Transform[] teleportPoints; 

    [Header("視覺效果")]
    public float rotateSpeed = -200f; // 黑洞旋轉速度
    public float scalePulseSpeed = 2f; // 縮放律動速度

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // 1. 讓黑洞持續旋轉 (很有吸入感)
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        // 2. 讓黑洞有一點點縮放律動，看起來像在「呼吸」
        float pulse = 0.1f * Mathf.Sin(Time.time * scalePulseSpeed);
        transform.localScale = originalScale + new Vector3(pulse, pulse, 0);
    }

    // 當物體撞到黑洞時觸發
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 只傳送玩家
        if (other.CompareTag("Player"))
        {
            PerformTeleport(other.transform);
        }
    }

    void PerformTeleport(Transform playerTransform)
    {
        if (teleportPoints.Length > 0)
        {
            // 隨機選一個點
            int randomIndex = Random.Range(0, teleportPoints.Length);
            Transform destination = teleportPoints[randomIndex];

            // 瞬間移動
            playerTransform.position = destination.position;

            Debug.Log("🌀 幽靈被黑洞吸走並傳送到：" + destination.name);
        }
        else
        {
            Debug.LogWarning("黑洞裡沒有設定傳送點！");
        }
    }
}
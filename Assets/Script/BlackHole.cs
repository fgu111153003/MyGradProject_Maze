using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [Header("傳送設定")]
    [Tooltip("請把場景中的『玩家起點/原點』物件拖進來")]
    public Transform respawnPoint; // 絕對傳送的原點

    // 移除了 Start 和 Update 裡面的旋轉、縮放程式碼，讓黑洞保持靜止

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
        if (respawnPoint != null)
        {
            // 瞬間移動到原點
            playerTransform.position = respawnPoint.position;

            Debug.Log("🌀 小鼓棒被黑洞吸入，已遣返回原點：" + respawnPoint.name);
        }
        else
        {
            Debug.LogWarning("⚠️ 黑洞沒有設定 respawnPoint 原點 請檢查 Inspector。");
        }
    }
}
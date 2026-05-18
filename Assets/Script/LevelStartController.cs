using UnityEngine;
using UnityEngine.UI;

public sealed class LevelStartController : MonoBehaviour
{
    [Header("相機設定")]
    [Tooltip("放入那台看全景的相機 CM_OverviewCamera")]
    public GameObject overviewCamera; 

    [Header("UI 設定")]
    [Tooltip("放入你的 BriefingPanel")]
    public GameObject briefingPanel;

    private void Start()
    {
        // 1. 遊戲一開始，開啟全景相機與提示 UI
        if (overviewCamera != null) overviewCamera.SetActive(true);
        if (briefingPanel != null) briefingPanel.SetActive(true);

        // 2. 暫停遊戲時間（讓幽靈和倒數計時器在看說明時先不要動）
        Time.timeScale = 0f;
    }

    // 當玩家點擊 UI 上的「出發」按鈕時執行
    public void OnClickStartChallenge()
    {
        // 1. 恢復遊戲時間
        Time.timeScale = 1f;

        // 2. 關閉提示 UI
        if (briefingPanel != null) briefingPanel.SetActive(false);

        // 3. 關閉全景相機
        if (overviewCamera != null) overviewCamera.SetActive(false);
        
        /* * 教授的動力學小教室：
         * 當這台 Priority 高的 overviewCamera 被關閉後，
         * Cinemachine 大腦會自動啟動下一台相機（追蹤 Player 那台），
         * 並且會用非常流暢的動畫「慢慢縮小並滑動」移回主角身上，視覺效果極流暢！
         */
        
        Debug.Log("🎯 挑戰開始，相機回歸主角！");
    }
}
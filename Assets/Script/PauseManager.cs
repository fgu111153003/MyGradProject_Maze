using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI 面板")]
    [Tooltip("請把 Canvas 底下的 PausePanel 拖進來")]
    public GameObject pausePanel;

    [Header("相機設定")]
    public Camera mapCamera;           // 拖入你的 Map_Camera

    private bool isPaused = false;

    void Start()
    {
        // 遊戲開始時，確保暫停面板與地圖相機都是關閉的
        if (pausePanel != null) pausePanel.SetActive(false);
        if (mapCamera != null) mapCamera.enabled = false;
    }

    void Update()
    {
        // 偵測玩家是否按下鍵盤的 ESC 鍵 (或是 P 鍵)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // 1. 暫停遊戲
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;            // 暫停遊戲時間（角色停止移動）
        
        if (pausePanel != null) pausePanel.SetActive(true);   // 顯示暫停地圖 UI
        if (mapCamera != null) mapCamera.enabled = true;      // 啟用相機渲染地圖
    }

    // 2. 繼續遊戲
    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false); // 隱藏暫停畫面
        
        // 恢復遊戲時間
        Time.timeScale = 1f; 
        
        Debug.Log("▶️ 遊戲繼續");
    }

    // 3. 重新開始此關卡
    public void RestartLevel()
    {
        // 【最重要】重開關卡前，一定要把時間速度恢復成 1，否則新關卡會動彈不得！
        Time.timeScale = 1f; 
        
        // 重新載入當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        Debug.Log("🔄 關卡重新開始");
    }
}
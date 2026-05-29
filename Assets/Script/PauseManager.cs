using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI 面板")]
    [Tooltip("請把 Canvas 底下的 PausePanel 拖進來")]
    public GameObject pausePanel;

    private bool isPaused = false;

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
        if (pausePanel != null) pausePanel.SetActive(true); // 顯示暫停畫面
        
        // 【核心】將遊戲時間縮放設為 0
        // 這會讓 PlayerController 裡的 Update 停止跑倒數計時，幽靈也會動彈不得！
        Time.timeScale = 0f; 
        
        Debug.Log("⏸️ 遊戲暫停，時間倒數已停止");
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
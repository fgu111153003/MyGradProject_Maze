using UnityEngine;
using UnityEngine.SceneManagement; // 確保有這一行，才能控制場景切換

public class EndingManagers : MonoBehaviour
{
    // 綁定給結局場景中「返回主選單」按鈕的方法
    public void BackToMainMenu()
    {
        // 確保時間流速恢復正常（預防萬一）
        Time.timeScale = 1f; 
        
        // 直接載入主畫面場景
        // 提示：請確保引號內的文字 "MainMenuScene" 與你主畫面場景的精確名稱一模一樣（包含大小寫）
        SceneManager.LoadScene("MainMenuScene"); 
    }
}
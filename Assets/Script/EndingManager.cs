using UnityEngine;
using UnityEngine.SceneManagement; 

public class EndingManagers : MonoBehaviour
{
    // 綁定給結局場景中「返回主選單」按鈕的方法
    public void BackToMainMenu()
    {
        // 確保時間正常
        UnityEngine.Time.timeScale = 1f; 
        
        // 直接載入主畫面場景
        SceneManager.LoadScene("MainMenuScene"); 
    }
}
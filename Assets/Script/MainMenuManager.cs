using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI 面板切換")]
    public GameObject mainMenuPanel;
    public GameObject infoPanel;
    public GameObject creditsPanel;

    void Start()
    {
        // 遊戲一開始，確保只顯示主選單，其他藏起來
        ShowMainMenu();
    }

    // 1. 開始遊戲按鈕（直接進入你的任務簡報或第一關）
    public void PlayGame()
    {
        // 恢復時間縮放（預防萬一）
        Time.timeScale = 1f;
        // 載入第一關，或者是你之前做的「方案一簡報場景」
        SceneManager.LoadScene("Level_1"); 
    }

    // 2. 顯示主選單
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        infoPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    // 3. 顯示物件與遊戲說明
    public void ShowInfo()
    {
        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    // 4. 顯示製作人員與學校Logo
    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}

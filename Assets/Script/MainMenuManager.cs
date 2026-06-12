using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 使用舊版 UI 文字與按鈕必備

public class MainMenuManager : MonoBehaviour
{
    [Header("【第一層】主面板切換")]
    public GameObject mainMenuPanel;    
    public GameObject infoPanel;        

    [Header("【第二層】Info 底下的子分頁")]
    public GameObject collectionSubPanel; 
    public GameObject mechanicsSubPanel;  

    void Start()
    {
        ShowMainMenu();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Level_1"); 
    }

    // ==========================================
    // 核心邏輯：控制大門的開關
    // ==========================================

    // 點擊「返回主選單」按鈕呼叫這個（完全退出說明）
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    // 點擊主選單的「遊戲說明」呼召這個
    public void ShowInfo()
    {
        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(true);

        // 預設狀態：一進來時，兩個子分頁都先不打開，讓玩家看到清晰的選擇畫面
        // 或者你也可以維持原本的 OpenCollectionTab()，看你的視覺設計
        BackToInfoMain(); 
    }

    // ==========================================
    // 新增邏輯：控制子分頁的切換與退回
    // ==========================================

    // 點擊「收藏品」標籤按鈕時呼叫
    public void OpenCollectionTab()
    {
        collectionSubPanel.SetActive(true);
        mechanicsSubPanel.SetActive(false);
    }

    // 點擊「冒險與機制」標籤按鈕時呼叫
    public void OpenMechanicsTab()
    {
        collectionSubPanel.SetActive(false);
        mechanicsSubPanel.SetActive(true);
    }

    // 【全新功能】點擊子分頁裡的「返回」按鈕時呼叫
    // 功能：不關閉 Info 總面板，只把裡面的子分頁藏起來，退回選單層
    public void BackToInfoMain()
    {
        collectionSubPanel.SetActive(false);
        mechanicsSubPanel.SetActive(false);
        Debug.Log("↩️ 已退回說明主層");
    }
}
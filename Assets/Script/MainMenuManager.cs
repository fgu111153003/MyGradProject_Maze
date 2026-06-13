using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 使用舊版 UI 文字與按鈕必備

public class MainMenuManager : MonoBehaviour
{
    [Header("【第一層】主面板切換")]
    public GameObject mainMenuPanel;    
    public GameObject infoPanel;
    public GameObject creditsPanel;        
    
    [Header("【第二層】致謝分頁控制")]
    public GameObject staffSubPanel;    // 人員與致謝分頁 (Page 1)
    public GameObject sourceSubPanel;   // 來源與版權分頁 (Page 2)

    [Header("Info 底下的子分頁")]
    public GameObject collectionSubPanel; 
    public GameObject mechanicsSubPanel;
    public GameObject infoMainButtons;  // 新增：專門控制「收藏品」與「冒險與機制」這兩個大按鈕的顯示
    public GameObject backToMenuButton;  // 全新新增：專門控制「返回主選單」這個按鈕的顯示  
    public GameObject infoMainText;  // 新增說明圖示與文字

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

    // ==========================================
    // 1. 進入與離開致謝總介面
    // ==========================================

    // 點擊主畫面的「製作人員」按鈕呼叫
    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        OpenStaffPage(); // 預設打開第一頁：人員
    }

    // 在任何一頁點擊「返回主畫面」時呼叫
    public void HideCredits()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    // ==========================================
    // 2. 致謝內部的分頁切換 (Page 1 <-> Page 2)
    // ==========================================

    // 點擊「下一頁」或由 ShowCredits 呼叫
    public void OpenStaffPage()
    {
        staffSubPanel.SetActive(true);
        sourceSubPanel.SetActive(false);
    }

    // 在第一頁點擊「查看來源 / 下一頁」時呼叫
    public void OpenSourcePage()
    {
        staffSubPanel.SetActive(false);
        sourceSubPanel.SetActive(true);
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
        infoMainButtons.SetActive(false);    // 隱藏主介面的大按鈕！
        infoMainText.SetActive(false);       // 隱藏說明文字
        backToMenuButton.SetActive(false);   // 隱藏「返回主選單」按鈕！
        collectionSubPanel.SetActive(true);
        mechanicsSubPanel.SetActive(false);
    }

    // 點擊「冒險與機制」標籤按鈕時呼叫
    public void OpenMechanicsTab()
    {
        infoMainButtons.SetActive(false);    // 隱藏主介面的大按鈕！
        infoMainText.SetActive(false);       // 隱藏說明文字
        backToMenuButton.SetActive(false);   // 隱藏「返回主選單」按鈕！
        collectionSubPanel.SetActive(false);
        mechanicsSubPanel.SetActive(true);
    }

    // 【全新功能】點擊子分頁裡的「返回」按鈕時呼叫
    // 功能：不關閉 Info 總面板，只把裡面的子分頁藏起來，退回選單層
    public void BackToInfoMain()
    {
        infoMainButtons.SetActive(true);     // ↩️ 讓大按鈕重新顯示出來！
        infoMainText.SetActive(true);        // ↩️ 讓說明文字重新顯示出來！
        backToMenuButton.SetActive(true);    // ↩️ 讓「返回主選單」按鈕重新顯示出來！
        collectionSubPanel.SetActive(false);
        mechanicsSubPanel.SetActive(false);
        Debug.Log("↩️ 已退回說明主層，所有主選單按鈕已恢復");
    }
}
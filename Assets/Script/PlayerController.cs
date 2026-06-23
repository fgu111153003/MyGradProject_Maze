using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // <--- 加入這一行才能控制 UI

public sealed class PlayerController : MonoBehaviour
{
    [Header("玩家設定")]
    public float moveSpeed = 5f;

    [Header("遊戲進度")]
    public int keysCollected = 0;    // 當前收集到的鑰匙數量
    public int keysRequired = 2;     // 開門需要的總數 (可以在 Inspector 調整)

    [Header("UI 設定")]
    public Text keyText; // 把你的 Text 物件拖到這個欄位
    public Text timerText; // <--- 新增：用來顯示時間的 Text

    [Header("計時設定")]
    public float timeRemaining = 180f; // 設定這關有幾秒
    private bool isGameOver = false;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("--- 移動音效設定 ---")]
    public AudioSource moveSfxSource;   // 專門播移動音效的播放器
    public AudioClip moveSound;         // 移動時的短音效 (.wav)
    public float stepInterval = 0.3f;   // 音效連續發出的間隔時間 (秒)，數值越小節奏越快
    private float stepTimer;            // 計時器內部計算用

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateKeyUI(); // 遊戲開始時先更新一次
    }

    // 建立一個更新 UI 的小工具
    void UpdateKeyUI()
    {
        if (keyText != null)
        {
            keyText.text = "Keys: " + keysCollected + " / " + keysRequired;
        }
    }
    void Update()
    {
        // 這兩行負責抓取鍵盤的 WASD 或 方向鍵，並存進 moveInput 裡
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // 🔍 偵測玩家目前有沒有在按按鍵移動
        bool isMoving = moveInput.x != 0 || moveInput.y != 0;

        if (isMoving)
        {
            // 計時器累積時間
            stepTimer += Time.deltaTime;

            // 時間到了就播一次音效
            if (stepTimer >= stepInterval)
            {
                // 確保你有在變數宣告區加上：public AudioSource moveSfxSource; 和 public AudioClip moveSound;
                if (moveSfxSource != null && moveSound != null)
                {
                    moveSfxSource.PlayOneShot(moveSound, 0.3f);
                }
                stepTimer = 0f;
            }
        }
        else
        {
            // 停下時重置計時器，確保下次起步立刻發聲
            stepTimer = stepInterval;
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 👈 每一幀都扣除現實世界經過的時間
        }
        else
        {
            timeRemaining = 0;
            GameOver();
            // 這裡可以放時間到的邏輯，例如：玩家死亡、Game Over 或是重新開始關卡
        }
        UpdateTimerUI(); // 👈 呼叫你寫好的計時器更新與閃爍特效！
    }

    // 更新計時器文字
    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // 將秒數轉成整數顯示
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString() + "s";

            // 剩下的時間不到 10 秒時，讓文字閃爍或變色
            if (timeRemaining < 30f)
            {
                timerText.color = Color.red;
                timerText.transform.localScale = Vector3.one * (1 + Mathf.PingPong(Time.time * 2, 0.2f));
            }
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("時間到！挑戰失敗");
        // 這邊可以直接重啟關卡，或是跳到失敗畫面
        Invoke("RestartLevel", 2f); // 2秒後重啟
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    // 處理 Trigger 碰撞 (鑰匙、終點、陷阱)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. 撿鑰匙邏輯
        if (other.CompareTag("Key"))
        {
            keysCollected++;
            UpdateKeyUI(); // 撿到鑰匙時更新 UI
            Destroy(other.gameObject);
        }

        // 2. 抵達終點邏輯
        if (other.CompareTag("Goal"))
        {
            Debug.Log("抵達終點！跳轉下一關");
            LoadNextLevel();
        }

        // 3. 踩到陷阱邏輯
        if (other.CompareTag("Trap"))
        {
            RestartLevel();
        }

        // 檢查是不是踩到第 7 關終點
        if (other.CompareTag("Finish"))
        {
            // 確保時間流速正常，才能順利載入新場景
            UnityEngine.Time.timeScale = 1f;

            // 直接載入你的新結局場景（請確保名稱與你新創的 Scene 一模一樣）
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClearScene");
        }
    }

    // 處理實體碰撞 (門)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (keysCollected >= keysRequired)
            {
                Debug.Log("鑰匙足夠，開門成功！");
                Destroy(collision.gameObject);
            }
            else
            {
                int need = keysRequired - keysCollected;
                Debug.Log("鑰匙不夠！還差 " + need + " 把才能開門。");
            }
        }
    }

    void LoadNextLevel()
    {
        // 1. 取得當前場景的編號
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // 2. 取得當前場景的名字
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // 3. 判斷是不是第七關 (可以用編號判斷，也可以用場景名字判斷，用名字最安全！)
        // 提示：引號內請改成你第 7 關在 Unity 裡的精確名稱，例如 "Level7" 或 "Stage7"
        if (currentSceneName == "Level_7")
        {
            // 如果是第七關，就直接載入全部通關的結局場景
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameClearScene");
        }
        else
        {
            // 如果不是最後一關，就正常載入下一關 (編號 + 1)
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
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
        if (isGameOver) return; // 如果遊戲結束，主角就不能動了

        // --- 新增：倒數計時邏輯 ---
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 每一幀扣除時間
            UpdateTimerUI();
        }
        else
        {
            timeRemaining = 0;
            GameOver(); // 時間到，遊戲結束
        }
        
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
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
        // 記得在 Build Settings 裡放好場景，或是改用 SceneManager.LoadScene("場景名");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
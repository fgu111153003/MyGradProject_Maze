using UnityEngine;
using UnityEngine.SceneManagement; // <--- 必須加這行，才能切換場景

public sealed class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // 新增：有沒有拿到鑰匙的狀態
    public bool hasKey = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    // 新增：處理碰撞事件
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果撞到的是標籤為 "Key" 的物件
        if (other.CompareTag("Key"))
        {
            hasKey = true; 
            Debug.Log("拿到鑰匙了！");
            Destroy(other.gameObject); // 讓鑰匙在畫面上消失
        }

        // 撿鑰匙的邏輯
        if (other.CompareTag("Key"))
        {
            hasKey = true; 
            Destroy(other.gameObject);
        }

        // --- 新增：過關邏輯 ---
        if (other.CompareTag("Goal"))
        {
            Debug.Log("抵達終點！");
            LoadNextLevel();
        }

        // --- 新增：碰到陷阱重來 ---
        if (other.CompareTag("Trap"))
        {
            Debug.Log("踩到陷阱！重啟本關...");
            RestartLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果撞到的是標籤為 "Door" 的物件，且手上有鑰匙
        if (collision.gameObject.CompareTag("Door") && hasKey)
        {
            Debug.Log("門開了！");
            Destroy(collision.gameObject); // 讓門消失
        }
    }

    void LoadNextLevel()
    {
        // 取得當前場景的編號，並載入下一個 (例如 0 號載入 1 號)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void RestartLevel()
    {
        // 取得當前「這一關」的名字並重新載入
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
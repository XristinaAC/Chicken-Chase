using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance { get; private set; }

    [Header("Crosshair UI")]
    [SerializeField] private RectTransform crosshair;
    [SerializeField] private RectTransform movementArea;
    [SerializeField] private RectTransform bossTargetUI;
    [SerializeField] private RectTransform miniGameUI;
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private BossManager bossTarget;
    
    [Header("Timer Settings")]
    [SerializeField] private float maxTime = 10f;
    [SerializeField] private TMP_Text timerText;
    private float currentTime;
    [Space] 
    [SerializeField] private TextMeshProUGUI hitText;

    private CanonManager currentCanon;
    private bool isActive;
    private bool hasShot;

    private void Awake()
    {
        Instance = this;
        movementArea.gameObject.SetActive(false);
        miniGameUI.gameObject.SetActive(false);
        hitText.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (!isActive) return;

        MoveCrosshair();
        UpdateTimer();

        if (Input.GetKeyDown(KeyCode.Space) && !hasShot)
        {
            hasShot = true;
            TryShoot();
        }
    }
    
    public void StartMiniGame(CanonManager canon)
    {
        currentCanon = canon;
        isActive = true;
        hasShot = false;
        movementArea.gameObject.SetActive(true);
        miniGameUI.gameObject.SetActive(true);
        
        currentTime = maxTime;
        if (timerText != null)
            timerText.gameObject.SetActive(true);
    }
    
    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (timerText != null)
            timerText.text = $"{currentTime:F1}s";

        if (currentTime <= 0f)
        {
            Debug.Log("⏱️ Time's up! MiniGame ended.");
 
            EndMiniGame();
        }
    }

    public void ShowHitText()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        hitText.gameObject.SetActive(true);
        hitText.text = "Successfuly Shot";
        
        yield return new WaitForSeconds(2f);
        
        hitText.gameObject.SetActive(false);
    }
    public void CloseHitText()
    {
        hitText.gameObject.SetActive(false);
        hitText.text = "Missed Shot";
    }

    private void MoveCrosshair()
    {
        Vector2 offset = new Vector2(
            Mathf.Sin(Time.time * 1.7f),
            Mathf.Cos(Time.time * 2f)
        ) * (moveSpeed * Time.deltaTime);

        crosshair.anchoredPosition += offset;

        crosshair.anchoredPosition = new Vector2(
            Mathf.Clamp(crosshair.anchoredPosition.x, -movementArea.rect.width / 2, movementArea.rect.width / 2),
            Mathf.Clamp(crosshair.anchoredPosition.y, -movementArea.rect.height / 2, movementArea.rect.height / 2)
        );
    }

    private void TryShoot()
    {

        if (RectOverlaps(crosshair, bossTargetUI))
        {
            bossTarget.TakeDamage(1);
            
            Vector3 targetWorldPos = bossTarget.transform.position;
            currentCanon.FireProjectile(targetWorldPos);
        }
        else
        {
            CloseHitText();
        }
        EndMiniGame();
    }
    

    private bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Vector3[] aCorners = new Vector3[4];
        Vector3[] bCorners = new Vector3[4];
        a.GetWorldCorners(aCorners);
        b.GetWorldCorners(bCorners);

        Rect aRect = new Rect(aCorners[0].x, aCorners[0].y,
                              aCorners[2].x - aCorners[0].x, aCorners[2].y - aCorners[0].y);
        Rect bRect = new Rect(bCorners[0].x, bCorners[0].y,
                              bCorners[2].x - bCorners[0].x, bCorners[2].y - bCorners[0].y);

        return aRect.Overlaps(bRect);
    }

    private void EndMiniGame()
    {
        isActive = false;
        movementArea.gameObject.SetActive(false);
        miniGameUI.gameObject.SetActive(false);

        currentCanon.ResetCanon();
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }
}

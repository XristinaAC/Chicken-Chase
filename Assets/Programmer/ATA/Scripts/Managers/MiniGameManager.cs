using System.Collections;
using TMPro;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance { get; private set; }

    [SerializeField] RectTransform crosshair;
    [SerializeField] RectTransform movementArea;
    [SerializeField] RectTransform bossTargetUI;
    [SerializeField] RectTransform miniGameUI;
    [SerializeField] float moveSpeed = 200f;
    [SerializeField] BossManager bossTarget;
    [SerializeField] float maxTime = 10f;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TextMeshProUGUI hitText;

    float currentTime;
    CanonManager currentCanon;
    bool isActive, hasShot;

	float noiseOffsetX, noiseOffsetY, angleOffset, speedMultiplier;

    void Awake()
    {
        Instance = this;
        movementArea.gameObject.SetActive(false);
        miniGameUI.gameObject.SetActive(false);
        hitText.gameObject.SetActive(false);
    }

    void Update()
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
        if (InventoryController.Instance == null || !InventoryController.Instance.HasProjectile())
        {
            ShowFeedback("No Projectiles");
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);
            return;
        }

		noiseOffsetX = Random.Range(0f, 1000f);
    	noiseOffsetY = Random.Range(0f, 1000f);
    	angleOffset  = Random.Range(0f, Mathf.PI * 2f);
    	speedMultiplier = Random.Range(0.8f, 1.3f);

        currentCanon = canon;
        isActive = true;
        hasShot = false;
        movementArea.gameObject.SetActive(true);
        miniGameUI.gameObject.SetActive(true);
        currentTime = maxTime;
        if (timerText)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = $"{currentTime:F1}s";
        }
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        if (timerText)
            timerText.text = $"{currentTime:F1}s";
        if (currentTime <= 0f)
        {
            ShowFeedback("Time's Up!");
            EndMiniGame();
        }
    }

    void MoveCrosshair()
    {
        if (bossTargetUI == null) return;

    	Vector2 centerPos = bossTargetUI.anchoredPosition;

    
    	float randomSeed = Mathf.Repeat(Time.time + noiseOffsetX, 1000f);
    	float t = (Time.time * 1.5f + randomSeed) * speedMultiplier;

    	float angle = t + angleOffset; 
    	float radius = 240 + Mathf.Sin(t * 2.2f + noiseOffsetY) * 40f;

    	float x = Mathf.Cos(angle) * radius;
    	float y = Mathf.Sin(angle * 1.2f + noiseOffsetY * 0.5f) * radius;

  
    	float noiseX = (Mathf.PerlinNoise(Time.time * 1.3f + noiseOffsetX, Time.time * 0.7f) - 0.5f) * 40f;
    	float noiseY = (Mathf.PerlinNoise(Time.time * 0.9f, Time.time * 1.1f + noiseOffsetY) - 0.5f) * 40f;

    	Vector2 targetPos = centerPos + new Vector2(x + noiseX, y + noiseY);

    	crosshair.anchoredPosition = Vector2.Lerp(
        	crosshair.anchoredPosition,
        	targetPos,
        	Time.deltaTime * 3f
    	);
    }

    void TryShoot()
    {
        if (RectOverlaps(crosshair, bossTargetUI))
            FireCollectedProjectile();
        else
        {
            ShowFeedback("Missed Shot!");
            EndMiniGame();
        }
    }

    void FireCollectedProjectile()
    {
        var nextProjectile = InventoryController.Instance.GetNextProjectile();
        if (nextProjectile == null)
        {
            ShowFeedback("No Projectiles Collected!");
            EndMiniGame();
            return;
        }

        Vector3 targetWorldPos = bossTarget.transform.position;
        currentCanon.FireProjectile(targetWorldPos, nextProjectile.projectilePrefab);
        bossTarget.TakeDamage(1);
        ShowFeedback($"Fired {nextProjectile.itemName}!");
        EndMiniGame();
    }

    bool RectOverlaps(RectTransform a, RectTransform b)
    {
        Vector3[] aC = new Vector3[4], bC = new Vector3[4];
        a.GetWorldCorners(aC);
        b.GetWorldCorners(bC);
        Rect ra = new Rect(aC[0].x, aC[0].y, aC[2].x - aC[0].x, aC[2].y - aC[0].y);
        Rect rb = new Rect(bC[0].x, bC[0].y, bC[2].x - bC[0].x, bC[2].y - bC[0].y);
        return ra.Overlaps(rb);
    }

    void ShowFeedback(string message)
    {
        StartCoroutine(ShowFeedbackRoutine(message));
    }

    IEnumerator ShowFeedbackRoutine(string message)
    {
        hitText.text = message;
        hitText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        hitText.gameObject.SetActive(false);
    }

    void EndMiniGame()
    {
        isActive = false;
        movementArea.gameObject.SetActive(false);
        miniGameUI.gameObject.SetActive(false);
        currentCanon.ResetCanon();
        //InventoryController.Instance.Clear();
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }
}

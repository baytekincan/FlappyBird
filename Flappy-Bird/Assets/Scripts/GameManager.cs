using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject playCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI boostTimerText;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource soundEffectSource;
    [SerializeField] private AudioClip flyClip;
    [SerializeField] private float spawnInterval = 5f;
    
    [SerializeField] private float starSpawnInterval = 5f;
    [SerializeField] private float doubleScoreDuration = 10f;

    private bool doubleScoreActive = false;

    void Awake()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        Time.timeScale = 0f;
        playCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        scoreText.gameObject.SetActive(false);
        backgroundMusicSource.Play();
        boostTimerText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        playCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        scoreText.gameObject.SetActive(true);
        Time.timeScale = 1f;
        soundEffectSource.Play();
        StartCoroutine(SpawnPipes());
        StartCoroutine(SpawnStars());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            PlayFlySound();
        }
    }

    void PlayFlySound()
    {
        if (flyClip != null)
        {
            soundEffectSource.PlayOneShot(flyClip);
        }
    }

    IEnumerator SpawnPipes()
    {
        while (true)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + Random.Range(-3f, 3f), transform.position.z);
            Instantiate(pipePrefab, spawnPos, Quaternion.identity);
            spawnInterval = Random.Range(1f, 3f);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnStars()
    {
        while (true)
        {
            yield return new WaitForSeconds(starSpawnInterval);

            float minY = -3f;
            float maxY = 4.5f;
            float minX = -2f;
            float maxX = 2f;
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

            if (starPrefab != null)
            {
                GameObject star = Instantiate(starPrefab, spawnPos, Quaternion.identity);
                if (star != null)
                {
                    SpriteRenderer sr = star.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        Debug.Log($"Star instantiated at {spawnPos} with SpriteRenderer.");
                    }
                    else
                    {
                        Debug.LogError("Star instantiated but missing SpriteRenderer component.");
                    }
                }
                else
                {
                    Debug.LogError("Star instantiation failed.");
                }
            }
            else
            {
                Debug.LogError("starPrefab is not assigned.");
            }
        }
    }

    public void ActivateDoubleScore()
    {
        StartCoroutine(DoubleScoreCoroutine());
    }

    IEnumerator DoubleScoreCoroutine()
    {
        doubleScoreActive = true;
        boostTimerText.gameObject.SetActive(true);
        float timeRemaining = doubleScoreDuration;

        while (timeRemaining > 0)
        {
            boostTimerText.text = $"2x : {timeRemaining:F0}";
            yield return new WaitForSeconds(1f);
            timeRemaining--;
        }

        doubleScoreActive = false;
        boostTimerText.gameObject.SetActive(false);
    }

    public bool IsDoubleScoreActive()
    {
        return doubleScoreActive;
    }

    public void GameOver()
    {
        StopAllCoroutines();
        gameCanvas.SetActive(false);
        playCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        scoreText.gameObject.SetActive(false);
        Time.timeScale = 0f;
        soundEffectSource.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    private float spawnRate = 1.0f;
    private int score;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive=false;
        restartButton.gameObject.SetActive(true);
        audioSource.Stop();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        isGameActive = true;
        score = 0;

        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        audioSource.clip = backgroundMusic;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void PlayClickSound(AudioClip clip) 
    {
        audioSource.PlayOneShot(clip);
    }
}

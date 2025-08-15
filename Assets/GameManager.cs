using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public double score = 0;
    public TextMeshProUGUI scoreText;
    public double scorePerSecond = 0;
    public double upgradeCost = 10;
    public TextMeshProUGUI upgradeCostText;
    private AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip upgradeSound;
    public Transform clickButtonTransform;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        LoadGame();
    }

    void Update()
    {
        score += scorePerSecond * Time.deltaTime;
        scoreText.text = "Очки: " + score.ToString("F0");
    }

    public void OnClick()
    {
        score++;
        audioSource.PlayOneShot(clickSound);
        clickButtonTransform.localScale = new Vector3(0.9f, 0.9f, 1f);
        Invoke("ResetButtonScale", 0.1f);
    }
    
    void ResetButtonScale()
    {
        clickButtonTransform.localScale = Vector3.one;
    }

    public void BuyUpgrade()
    {
        if (score >= upgradeCost)
        {
            score -= upgradeCost;
            scorePerSecond++;
            upgradeCost *= 1.5;
            upgradeCostText.text = "Цена: " + upgradeCost.ToString("F0");
            audioSource.PlayOneShot(upgradeSound);
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetString("score", score.ToString());
        PlayerPrefs.SetString("scorePerSecond", scorePerSecond.ToString());
        PlayerPrefs.SetString("upgradeCost", upgradeCost.ToString());
    }

    public void LoadGame()
    {
        score = double.Parse(PlayerPrefs.GetString("score", "0"));
        scorePerSecond = double.Parse(PlayerPrefs.GetString("scorePerSecond", "0"));
        upgradeCost = double.Parse(PlayerPrefs.GetString("upgradeCost", "10"));
        upgradeCostText.text = "Цена: " + upgradeCost.ToString("F0");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
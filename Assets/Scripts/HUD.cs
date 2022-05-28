using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI energyText;
    PlayerHealth health;

    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI highScoreText;

    private void Start() {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (energyText != null)
            energyText.text = $"Gargh: {health.GetHealth()}";
        if (scoreText != null)
            scoreText.text = $"Score: {GameManager.Instance.Score}";
        if (highScoreText != null)
            highScoreText.text = $"Highscore: {GameManager.Instance.HighScore}";
    }
}

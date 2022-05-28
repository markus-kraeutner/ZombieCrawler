using TMPro;
using UnityEngine;

public class Dead : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;

    void Update()
    {
        if (scoreText != null)
            scoreText.text = $"Your Score was: {GameManager.Instance.Score}";
    }
}

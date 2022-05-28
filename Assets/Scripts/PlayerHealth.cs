using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float invincibleTime = 1;
    float currentInvicibleTimer=-1;

    [SerializeField]
    float health = 100;
    [SerializeField]
    float currentHealth;
    [SerializeField]
    GameObject playerSprite;

    public float GetHealth() {
        return currentHealth;
    }

    public void DoDamage(float damage) {
        if (currentInvicibleTimer > 0) return;

        currentInvicibleTimer = invincibleTime;
        currentHealth -= damage;
        if(currentHealth<0) {
            GameManager.Instance.UpdateGameState(GameState.Dead);
        }
    }

    public void RestoreDamage(float damage) {
        if (currentInvicibleTimer > 0) return;
        currentHealth += damage;
    }

    public void ResetHealth() {
        currentHealth = health;
        currentInvicibleTimer = -1;
    }

    float flickerCounter = -1;
    [SerializeField]
    float reloadFlickerCounter = 0.05f;
    private void Update() {
        currentInvicibleTimer -= Time.deltaTime;
        flickerCounter -= Time.deltaTime;
        if(currentInvicibleTimer>0) {
            if(flickerCounter<0) {
                playerSprite.SetActive(!playerSprite.activeSelf);
                flickerCounter = reloadFlickerCounter;
            }
        } else {
            playerSprite.SetActive(true);
        }
    }

}

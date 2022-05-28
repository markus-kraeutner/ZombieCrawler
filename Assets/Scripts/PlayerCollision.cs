using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    PlayerHealth health;

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerDamage damage = collision.GetComponent<PlayerDamage>();
        if (damage != null)
            health.DoDamage(damage.GetDamage());
        Brain brain= collision.GetComponent<Brain>();
        if (brain != null) {
            collision.gameObject.SetActive(false);
            health.RestoreDamage(brain.GetGargh());
            GameManager.Instance.AddScore(500);
        }
    }
}

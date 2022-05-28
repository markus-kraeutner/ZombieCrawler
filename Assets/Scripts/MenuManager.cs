using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject titleMenu;
    [SerializeField]
    GameObject hud;
    [SerializeField]
    GameObject dead;

    
    private void Awake() {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }
    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState state) {
        titleMenu?.SetActive(state == GameState.TitleScreen);
        hud?.SetActive(state == GameState.Playing);
        dead?.SetActive(state == GameState.Dead);
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning()) {
            bool pressed = false;

            if (Input.GetKeyDown(KeyCode.Space))
                pressed = true;

            if (pressed) {
                GameManager.Instance.UpdateGameState(GameState.Playing);
            }
        }        
    }
}

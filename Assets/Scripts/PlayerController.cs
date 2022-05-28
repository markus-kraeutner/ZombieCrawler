using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float maxVelocity = 12f;
    [SerializeField]
    float moveAcceleration = 0.1f;
    [SerializeField]
    float currentVelo;
    [SerializeField]
    float damping = 0.05f;

    void Update() {
        bool left = false;
        bool right = false;

        if (Input.GetKey(KeyCode.LeftArrow))
            left = true;
        if (Input.GetKey(KeyCode.RightArrow))
            right = true;

        float myMaxVelocity = maxVelocity * GameManager.Instance.GetSpeedup();
        float myMoveAcceleration = moveAcceleration * GameManager.Instance.GetSpeedup();

        AdjustVelocity(left, right, myMaxVelocity, myMoveAcceleration);

        Bounds bounds = Camera.main.OrthographicBounds();
        const float playerWidth = 0.6f;
        float xBounds = bounds.extents.x - playerWidth;
        Vector3 pos = transform.position;
        if (pos.x < -xBounds) {
            pos.x = -xBounds;
            currentVelo = 0f;
        } else if (pos.x > xBounds) {
            pos.x = xBounds;
            currentVelo = 0f;
        } else {
            pos.x += currentVelo * Time.deltaTime;
        }
        transform.position = pos;
    }

    private float AdjustVelocity(bool left, bool right, float maxVelo, float moveAcc) {
        float input = 0;
        if (left) {
            if (currentVelo > -maxVelo)
                input = -moveAcc * Time.deltaTime / 1;
            currentVelo += input;
        } else if (right) {
            if (currentVelo < maxVelo)
                input = moveAcc * Time.deltaTime / 1;
            currentVelo += input;
        } else if (Mathf.Abs(currentVelo) > 0.01) {
            currentVelo += -currentVelo * damping;
        } else {
            currentVelo = 0f;
        }

        return input;
    }
}

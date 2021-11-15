using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Class that controlls time within the game scene
    /// Enables bullet time by decreasing the speed at which each physics calculation step happens, aswell as decreasing the players speed
    /// </summary>

    public float slowmotionFactor = .05f;
    public float slowmotionResetLength = 2f;

    bool resetSlowmotion;

    PlayerControlls playerControlls;

    private void Start()
    {
        playerControlls = FindObjectOfType<PlayerControlls>();
    }

    private void Update()
    {
        if (resetSlowmotion)
        {
            Time.timeScale += (1f / slowmotionResetLength) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime = Time.timeScale * .02f;

            //TODO: hardcoded, change
            playerControlls.setSpeed(.15f * Time.timeScale, 2 * Time.timeScale);

            if (Time.timeScale >= 1) {
                Time.timeScale = 1;
                Time.fixedDeltaTime = Time.timeScale * .02f;
                resetSlowmotion = false;

                playerControlls.setSpeed(.15f * Time.timeScale, 2 * Time.timeScale);
            }
        }
    }

    public void slowDownTime() {
        Time.timeScale = slowmotionFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        playerControlls.setSpeed(.15f * Time.timeScale, 2 * Time.timeScale);

        resetSlowmotion = false;
    }

    public void resetTime() {
        resetSlowmotion = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    const float TimeOut = 5.0f;
    enum SplashStates
    {
        Moving, FinishAndWait, FinishPressKey
    }
    SplashStates State;
    float startTime;
    void Start()
    {
        State = SplashStates.Moving;
        startTime = Time.time;
    }

    void Update()
    {
        switch (State)
        {
            case SplashStates.Moving:
                if (Time.time - startTime > TimeOut)
                    State = SplashStates.FinishAndWait;
                if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
                    State = SplashStates.FinishPressKey;
                break;
            case SplashStates.FinishPressKey:
                SceneManager.LoadScene("MainMenu");
                break;
            case SplashStates.FinishAndWait:
                if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
                    SceneManager.LoadScene("MainMenu");
                break;
            default: break;
        }
    }
}

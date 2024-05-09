using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _time;
    private bool _isTimerOn = false;
    public bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0f;
        _isTimerOn = false;
    }

    public void setTimer(float time) {
        _time = time;
    }

    IEnumerator timerOn() {
        Debug.Log("in cor");
        while(_time > 0) {
            _time -= 1f;
            Debug.Log("Timer : " + _time);
            yield return new WaitForSeconds(1);
        }

        Debug.Log("time out!!");

        _isTimerOn = false;
        _isGameOver = true;
        yield return null;
    }

    public void startTimer() {
        if(_isTimerOn)
            return;

        Debug.Log("start timer!!");
        _isTimerOn = true;

        setTimer(5f);

        StartCoroutine("timerOn");
    }
}

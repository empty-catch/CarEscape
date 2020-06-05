#pragma warning disable CS0649

using System.Collections;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private int minute;
    [SerializeField]
    private Text text;
    [SerializeField]
    private BoolEvent toggledPause;
    [SerializeField]
    private UnityEvent elapsed;

    private bool isPaused;
    private IEnumerator measure;
    private TimeSpan total;
    private Stopwatch stopwatch = new Stopwatch();

    public TimeSpan Elapsed => stopwatch.Elapsed;
    public bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;
            toggledPause?.Invoke(value);

            if (value)
            {
                stopwatch.Stop();
                measure?.Stop(this);
            }
            else
            {
                stopwatch.Start();
                measure?.Start(this);
            }
        }
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
    }

    private void Awake()
    {
        total = new TimeSpan(0, minute, 0);
        stopwatch.Start();
        measure = Measure().Start(this);
    }

    private IEnumerator Measure()
    {
        while (stopwatch.Elapsed < total)
        {
            var timeText = (total - stopwatch.Elapsed).ToString(@"m\:ss\:ff");
            text.text = timeText;
            yield return null;
        }

        stopwatch.Stop();
        text.text = "0:00:00";
        elapsed?.Invoke();
    }
}

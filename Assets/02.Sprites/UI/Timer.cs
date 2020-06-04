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
    private UnityEvent elapsed;

    private TimeSpan total;
    private Stopwatch stopwatch = new Stopwatch();

    private IEnumerator Start()
    {
        total = new TimeSpan(0, minute, 0);
        stopwatch.Start();

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

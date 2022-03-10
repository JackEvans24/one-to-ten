using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    private static LogManager instance;

    [SerializeField] private TMP_Text[] logFields;
    private Queue<TMP_Text> logFieldQueue = new Queue<TMP_Text>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        foreach (var item in logFields)
            this.logFieldQueue.Enqueue(item);
    }

    public static void Log(string message)
    {
        if (instance == null)
            return;

        instance.Log_Internal(message);
    }

    private void Log_Internal(string message)
    {
        var nextField = this.logFieldQueue.Dequeue();
        nextField.text = message;
        this.logFieldQueue.Enqueue(nextField);
    }
}

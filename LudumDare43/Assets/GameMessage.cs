using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMessage : SimpleSingleton<GameMessage>
{

    private Animator anim;
    private TextMeshProUGUI tmpro;

    private bool isMessageShowing = false;

    Queue<QueuedMessage> queue = new Queue<QueuedMessage>();
    private bool isReadyToDequeue = true;

    struct QueuedMessage
    {
        public string message;
        public float duration;

        public QueuedMessage(string message, float duration)
        {
            this.message = message;
            this.duration = duration;
        }
    }


    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        tmpro = GetComponent<TextMeshProUGUI>();
        tmpro.text = "";
    }

    public void Update()
    {
        if (!isMessageShowing && isReadyToDequeue &&  queue.Count > 0)
        {
            isReadyToDequeue = false;
            var qMsg = queue.Dequeue();
            ShowMessage(qMsg.message, qMsg.duration);            
        }
    }

    private IEnumerator WaitToDequeue()
    {
        yield return new WaitForSeconds(.5f);
        isReadyToDequeue = true;

    }


    public void ShowMessage(string message, float duration = 2f, bool important = false)
    {
        Debug.Log("message: " + message);
        if (!isMessageShowing)
        {
            isMessageShowing = true;
            tmpro.text = message;
            anim.SetTrigger("showMessage");
            StartCoroutine(EndMessage(duration));
        }
        else
        {
            var qMsg = new QueuedMessage(message, duration);
            if (important)
            {
                var items = queue.ToArray();
                queue.Clear();
                queue.Enqueue(qMsg);
                foreach(var i in items)
                {
                    queue.Enqueue(i);
                }
            }
            else
            {
                queue.Enqueue(qMsg);
            }
            Debug.Log("enqueue message: " + message);
            StartCoroutine(WaitToDequeue());
        }
    }

    private IEnumerator EndMessage(float duration)
    {
        yield return new WaitForSeconds(duration);
        anim.SetTrigger("hideMessage");
        isMessageShowing = false;
        isReadyToDequeue = true;
    }
}


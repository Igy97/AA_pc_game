using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class exitretrynext : MonoBehaviour
{
    //exit, retry, next se pojavljuju na kraju igre

    public RectTransform retry;
    public Text retryt;
    public RectTransform exit;
    public Text exitt;
    public RectTransform next;
    public Text nextt;

    //Exit button

    public void ExitOn()
    {
        exit.localScale = new Vector3(3.1f, 0.8073783f, 1);
        FindObjectOfType<AudioManager>().Play("ButtonEnter");
    }

    public void ExitOff()
    {
        exit.localScale = new Vector3(2.105277f, 0.8073783f, 1);
    }

    public void ExitClickOn()
    {
        exitt.color = Color.yellow;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void ExitClickOff()
    {
        exitt.color = Color.white;

    }

//Retry button

    public void RetrytOn()
    {
        retry.localScale = new Vector3(3.212329f, 0.8067297f, 1);
        FindObjectOfType<AudioManager>().Play("ButtonEnter");
    }

    public void RetryOff()
    {
        retry.localScale = new Vector3(2.141553f, 0.8067297f, 1);
    }

    public void RetryClickOn()
    {
        retryt.color = Color.yellow;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void RetryClickOff()
    {
        retryt.color = Color.white;

    }

    //Next button

    public void NexttOn()
    {
        next.localScale = new Vector3(3.319407f, 0.9874998f, 1);
        FindObjectOfType<AudioManager>().Play("ButtonEnter");
    }

    public void NextOff()
    {
        next.localScale = new Vector3(2.141553f, 0.8067297f, 1);
    }

    public void NextClickOn()
    {
        nextt.color = Color.yellow;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void NextClickOff()
    {
        nextt.color = Color.white;

    }




}

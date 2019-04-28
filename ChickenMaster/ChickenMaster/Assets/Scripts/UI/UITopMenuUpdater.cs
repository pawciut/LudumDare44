using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITopMenuUpdater : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI RoundLabelGUI;
    [SerializeField]
    TextMeshProUGUI RoundValueGUI;

    [SerializeField]
    TextMeshProUGUI ScoreLabelGUI;
    [SerializeField]
    TextMeshProUGUI ScoreValueGUI;

    [SerializeField]
    TextMeshProUGUI WaveTimerLabelGUI;
    [SerializeField]
    TextMeshProUGUI WaveTimerValueGUI;

    [SerializeField]
    TextMeshProUGUI TotalTimerValueGUI;

    private void Start()
    {

    }


    public void HideAll()
    {
        HideRound();
        HideScore();
        HideWaveTime();
        HideTime();
    }

    public void ShowRound(int roundNumber)
    {

        if (RoundLabelGUI != null)
            RoundLabelGUI.gameObject.SetActive(true);
        if (RoundValueGUI != null)
        {
            RoundValueGUI.gameObject.SetActive(true);
            RoundValueGUI.text = roundNumber.ToString();
        }
    }
    public void HideRound()
    {
        if (RoundLabelGUI != null)
            RoundLabelGUI.gameObject.SetActive(false);
        if (RoundValueGUI != null)
        {
            RoundValueGUI.gameObject.SetActive(false);
            RoundValueGUI.text = string.Empty;
        }
    }

    public void ShowScore(int score)
    {

        if (ScoreLabelGUI != null)
            ScoreLabelGUI.gameObject.SetActive(true);
        if (ScoreValueGUI != null)
        {
            ScoreValueGUI.gameObject.SetActive(true);
            ScoreValueGUI.text = score.ToString();
        }
    }
    public void HideScore()
    {
        if (ScoreLabelGUI != null)
            ScoreLabelGUI.gameObject.SetActive(false);
        if (ScoreValueGUI != null)
        {
            ScoreValueGUI.gameObject.SetActive(false);
            ScoreValueGUI.text = string.Empty;
        }
    }




    public void ShowWaveTime(string title, string timeString)
    {

        if (WaveTimerLabelGUI != null)
        {
            WaveTimerLabelGUI.gameObject.SetActive(true);
            WaveTimerLabelGUI.text = title;
        }
        if (WaveTimerValueGUI != null)
        {
            WaveTimerValueGUI.gameObject.SetActive(true);
            WaveTimerValueGUI.text = timeString;
        }
    }
    public void HideWaveTime()
    {
        if (WaveTimerLabelGUI != null)
        {
            WaveTimerLabelGUI.gameObject.SetActive(false);
            WaveTimerLabelGUI.text = string.Empty;
        }
        if (WaveTimerValueGUI != null)
        {
            WaveTimerValueGUI.gameObject.SetActive(false);
            WaveTimerValueGUI.text = string.Empty;
        }
    }


    public void ShowTime(string timeString)
    {
        if (TotalTimerValueGUI != null)
        {
            TotalTimerValueGUI.gameObject.SetActive(true);
            TotalTimerValueGUI.text = timeString;
        }
    }
    public void HideTime()
    {
        if (TotalTimerValueGUI != null)
        {
            TotalTimerValueGUI.gameObject.SetActive(false);
            TotalTimerValueGUI.text = string.Empty;
        }
    }

}

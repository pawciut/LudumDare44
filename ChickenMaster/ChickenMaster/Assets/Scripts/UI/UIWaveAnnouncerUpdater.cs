using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveAnnouncerUpdater : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI NameGUI;
    [SerializeField]
    TextMeshProUGUI DescGUI;
    [SerializeField]
    float Duration;

    float DefaultTransparency;

    float FadeTime = 2f;


    private void Start()
    {

    }

    public void Show(string towerName, string towerDesc)
    {
        gameObject.SetActive(true);

        if (NameGUI != null)
            NameGUI.text = towerName.ToString();
        if (DescGUI != null)
            DescGUI.text = towerDesc.ToString();

        //TODO:zeby byl fade out to trzeba alpha zmniejszac w panelu jak i w  MeshPro w ustawieniu Face
        //jako coroutine

        StartCoroutine(FadeOut());
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        if (NameGUI != null)
            NameGUI.text = string.Empty;
        if (DescGUI != null)
            DescGUI.text = string.Empty;
    }



    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(Duration);
        Hide();
    }
}

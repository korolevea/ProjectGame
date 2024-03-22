using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DownloadingSign : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimText());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator AnimText()
    {
        textMeshPro.text = "Dowloading.";
        yield return new WaitForSeconds(delay);
        textMeshPro.text = "Dowloading..";
        yield return new WaitForSeconds(delay);
        textMeshPro.text = "Dowloading...";
        yield return new WaitForSeconds(delay);
        textMeshPro.text = "Dowloading.";
        yield return new WaitForSeconds(delay);
        textMeshPro.text = "Dowloading..";
        yield return new WaitForSeconds(delay);
        textMeshPro.text = "Dowloading...";
        yield return new WaitForSeconds(delay);
        gameObject.transform.parent.gameObject.SetActive(false);

    }
}

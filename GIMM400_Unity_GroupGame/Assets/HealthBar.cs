using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //[SerializeField]
    public Image foregroundImage;
    //[SerializeField]
    public float updateSpeedSeconds = 0.2f;

    // Start is called before the first frame update
    private void Awake()
    {
        GetComponentInParent<PlayerHealth>().OnHealthPctChanged += HandleHealthChanged;
    }

    // Update is called once per frame
    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }
    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0.0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }
    //private void LateUpdate()
    //{
    //    transform.LookAt(Camera.main.transform);
    //    transform.Rotate(0, 180, 0);
    //}
}

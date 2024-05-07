using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class bling : MonoBehaviour
{
    public float blinkIntervalMin = 0.1f; // ����������� �������� ����� ���������
    public float blinkIntervalMax = 2f; // ������������ �������� ����� ���������
    public float blinkDuration = 0.1f; // ����������������� �������

    private float timer;
    private float blinkInterval;

    private Light2D light1;

    private void Start()
    {
        light1 = GetComponent<Light2D>();
        timer = 0f;
        SetBlinkInterval();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            StartCoroutine(Blink());
            SetBlinkInterval();
            timer = 0f;
        }
    }

    private void SetBlinkInterval()
    {
        blinkInterval = Random.Range(blinkIntervalMin, blinkIntervalMax);
    }

    private IEnumerator Blink()
    {
        light1.enabled = !light1.enabled;
        yield return new WaitForSeconds(blinkDuration);
        light1.enabled = !light1.enabled;
    }

}

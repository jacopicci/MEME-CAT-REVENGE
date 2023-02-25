using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class StartingScene : MonoBehaviour
{
    [SerializeField] Volume vlm;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject igHUD;
    [SerializeField] float rotationAmount = 90.0f;
    [SerializeField] float fieldOfView = 60.0f;
    [SerializeField] scacchiera scacchieraScript;
    private float animationTime = 2.0f;
    bool started;
    public float rotationSpeed = 100f;
    public float maxRotationAngle = -20f;

    private float currentRotationAngle = 0f;


    private Coroutine animCoroutine = null;
    public void OnClick()
    {
        Time.timeScale = 1;
        if (animCoroutine != null)
        {
            StopCoroutine(animCoroutine); // ferma la coroutine in esecuzione (se presente)
        }
        animCoroutine = StartCoroutine(AnimateObject()); // avvia la coroutine e salva il riferimento in animCoroutine
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0 && started)
        {
            currentRotationAngle += -Input.mouseScrollDelta.y * rotationSpeed;
            currentRotationAngle = Mathf.Clamp(currentRotationAngle, maxRotationAngle, 0f);

            Quaternion targetRotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, currentRotationAngle);

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed*50);
        }


    }

    IEnumerator AnimateObject()
    {
        float timeElapsed = 0.0f;

        menu.SetActive(false);
        while (timeElapsed < animationTime)
        {
            float lerpValue = timeElapsed / animationTime;

            // Gradualmente riduci il valore del post processing depth of field
            vlm.GetComponent<Volume>().weight = Mathf.Lerp(1.0f, 0.0f, lerpValue);

            // Gradualmente ruota l'oggetto su cui è attaccato di 90°
            Quaternion startRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            Quaternion endRotation = Quaternion.Euler(0f, -rotationAmount, 0.0f);
            Quaternion rotation = Quaternion.Slerp(startRotation, endRotation, lerpValue);
            transform.rotation = rotation;

            // Gradualmente riduci il fov della telecamera fino a 30
            fieldOfView = Mathf.Lerp(56.5f, 38.0f, lerpValue);
            Camera.main.fieldOfView = fieldOfView;

            timeElapsed += Time.deltaTime;
            yield return null;
        }


        // Disattiva l'oggetto
        scacchieraScript.enabled = true;
        igHUD.SetActive(true);
        started = true;
    }
}


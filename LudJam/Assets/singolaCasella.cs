using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class singolaCasella : MonoBehaviour
{
    [SerializeField] float fadeDuration = 0.5f;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] caselleScacchiera caselleScacchiera;
    private SpriteRenderer image;
    private Color targetColor;
    private Color currentColor;
    private bool isMouseOver = false;
    private bool isFading = false;
    bool fullyOn;
    bool fullyOff = true;
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        targetColor = new Color(1,1,1,0);
        currentColor = image.color;

        IEnumerator In = FadeIn();
        IEnumerator Out = FadeOut();
    }

    void Update()
    {
        if (isMouseOver && !isFading && fullyOff && !fullyOn)
        {
            
            StartCoroutine(FadeIn());
            isFading = true;
        }
        else if (!isMouseOver && !isFading && fullyOn && !fullyOff)
        {
            
            StartCoroutine(FadeOut());
            isFading = true;
        }
        
    }

    void OnMouseOver()
    {
        
        isMouseOver = true;
    }
    private void OnMouseDown()
    {
        if (playerManager.boughtToSpawn)
        {
            Debug.Log("2");
            StartCoroutine(caselleScacchiera.caselleAllies(transform.parent.gameObject.name, transform.position));
        }
        if (playerManager.boughtToDestroy)
        {
            playerManager.DeleteNearestObject(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "NewRow")
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            StartCoroutine(FadeIn());

        }
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }

    IEnumerator FadeIn()
    {
        
        float timer = 0f;
        currentColor = image.color;
        while (timer*100 < fadeDuration*100)
        {
            timer += Time.deltaTime;
            currentColor.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            image.color = currentColor;
            yield return null;
            
        }
        isFading = false;
        fullyOn= true;
        fullyOff= false;
    }

    IEnumerator FadeOut()
    {

        
        float timer = 0f;
        currentColor = image.color;
        while (timer * 100 < fadeDuration * 100)
        {
            timer += Time.deltaTime;
            currentColor.a = Mathf.Lerp(1, 0, timer / fadeDuration);
            image.color = currentColor;
            yield return null;
            

        }
        isFading = false;
        fullyOff = true;
        fullyOn = false;
    }
}

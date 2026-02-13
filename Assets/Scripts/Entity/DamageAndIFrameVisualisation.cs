using System.Collections;
using UnityEngine;

public class DamageAndIFrameVisualisation : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [ColorUsage(true, true)] [SerializeField] private Color flashColor = Color.white;
    
    [Header("Damage flash settings")]
    [SerializeField] private float flashTime = 0.25f;
    
    [Header("I-frame settings")]
    [Tooltip("The time it takes for the flash to reach it's target")]
    [SerializeField] private float iFrameFlashTime = 0.2f;
    [SerializeField] [Range(0, 1)] private float iFrameFlashIntensity = 0.6f;
    
    
    private Coroutine _flashCoroutine;
    private Coroutine _invincibleCoroutine;
    private bool _isInvincible;
    private Material _material;
    

    private void Awake()
    {
        _material = spriteRenderer.material;
    }

    public void VisualizeIFrames()
    {
        _invincibleCoroutine = StartCoroutine(IFrameFlash());
    }

    public void StopIFrameVisualisation()
    {
        _isInvincible = false;
    }

    private IEnumerator IFrameFlash()
    {
        _isInvincible = true;
        
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        
        bool increasing = true;

        while (_isInvincible)
        {
            if (increasing)
            {
                elapsedTime += Time.deltaTime;
                currentFlashAmount = Mathf.Lerp(0f, iFrameFlashIntensity, elapsedTime / iFrameFlashTime);
                _material.SetFloat("_FlashAmount", currentFlashAmount);
            }
            else
            {
                elapsedTime -= Time.deltaTime;
                currentFlashAmount = Mathf.Lerp(iFrameFlashIntensity, 0f, 1 - (elapsedTime / iFrameFlashTime));
                _material.SetFloat("_FlashAmount", currentFlashAmount);
            }

            if (increasing && elapsedTime >= iFrameFlashTime || !increasing && elapsedTime <= 0)
            {
                increasing = !increasing;
            }

            yield return null;
        }
        
        _material.SetFloat("_FlashAmount", 0);
    }

    public void CallFlashCoroutine()
    {
        _flashCoroutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        _material.SetColor("_FlashColor", flashColor);

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / flashTime);
            _material.SetFloat("_FlashAmount", currentFlashAmount);
            yield return null;
        }
    }
}

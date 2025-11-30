using UnityEngine;
using UnityEngine.UI;

public class MentalDistortionAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Material distortionMaterial;
    
    [Header("Distortion Settings")]
    [SerializeField] private float baseDistortionStrength = 0.02f;
    [SerializeField] private float maxDistortionStrength = 0.08f;
    [SerializeField] private float distortionVariationSpeed = 1f;
    
    [Header("Speed Settings")]
    [SerializeField] private float baseDistortionSpeed = 1f;
    [SerializeField] private float maxDistortionSpeed = 3f;
    [SerializeField] private float speedVariationSpeed = 0.5f;
    
    [Header("Color Shift Settings")]
    [SerializeField] private float baseColorShift = 0.1f;
    [SerializeField] private float maxColorShift = 0.25f;
    [SerializeField] private float colorVariationSpeed = 0.8f;
    
    [Header("Noise Settings")]
    [SerializeField] private float baseNoiseScale = 0.05f;
    [SerializeField] private float maxNoiseScale = 0.1f;
    [SerializeField] private float noiseVariationSpeed = 0.7f;
    
    [Header("Alpha Settings")]
    [SerializeField] private float baseAlphaFade = 0.5f;
    [SerializeField] private float maxAlphaFade = 0.8f;
    [SerializeField] private float alphaVariationSpeed = 0.6f;
    
    [Header("Intensity Control")]
    [SerializeField] [Range(0f, 1f)] private float currentIntensity = 0f;
    [SerializeField] private bool autoAnimateIntensity = true;
    [SerializeField] private float intensityChangeSpeed = 0.3f;
    [SerializeField] private float maxIntensityDuration = 2f;
    
    private float intensityTimer = 0f;
    private bool increasingIntensity = true;

    void Start()
    {
        if (distortionMaterial == null)
        {
            Image image = GetComponent<Image>();
            if (image != null)
            {
                distortionMaterial = image.material;
            }
        }
        
        UpdateShaderParameters();
    }

    void Update()
    {
        if (distortionMaterial == null) return;

        // Автоматическая анимация интенсивности
        if (autoAnimateIntensity)
        {
            AnimateIntensity();
        }
        
        // Анимация отдельных параметров для дополнительной динамики
        AnimateIndividualParameters();
        
        UpdateShaderParameters();
    }

    private void AnimateIntensity()
    {
        if (increasingIntensity)
        {
            currentIntensity += Time.deltaTime * intensityChangeSpeed;
            if (currentIntensity >= 1f)
            {
                currentIntensity = 1f;
                intensityTimer += Time.deltaTime;
                
                if (intensityTimer >= maxIntensityDuration)
                {
                    increasingIntensity = false;
                    intensityTimer = 0f;
                }
            }
        }
        else
        {
            currentIntensity -= Time.deltaTime * intensityChangeSpeed;
            if (currentIntensity <= 0f)
            {
                currentIntensity = 0f;
                increasingIntensity = true;
            }
        }
    }

    private void AnimateIndividualParameters()
    {
        // Осцилляция для дополнительной динамики даже при постоянной интенсивности
        float time = Time.time;
        
        // Легкие колебания скорости искажений
        float speedVariation = Mathf.Sin(time * speedVariationSpeed) * 0.3f + 0.7f;
        distortionMaterial.SetFloat("_DistortionSpeed", 
            Mathf.Lerp(baseDistortionSpeed, maxDistortionSpeed, currentIntensity) * speedVariation);
            
        // Легкие колебания шума
        float noiseVariation = Mathf.PerlinNoise(time * noiseVariationSpeed, 0) * 0.5f + 0.5f;
        distortionMaterial.SetFloat("_NoiseScale", 
            Mathf.Lerp(baseNoiseScale, maxNoiseScale, currentIntensity) * noiseVariation);
    }

    private void UpdateShaderParameters()
    {
        if (distortionMaterial == null) return;

        // Интерполяция параметров на основе текущей интенсивности
        distortionMaterial.SetFloat("_DistortionStrength", 
            Mathf.Lerp(baseDistortionStrength, maxDistortionStrength, currentIntensity));
            
        distortionMaterial.SetFloat("_ColorShift", 
            Mathf.Lerp(baseColorShift, maxColorShift, currentIntensity));
            
        distortionMaterial.SetFloat("_AlphaFade", 
            Mathf.Lerp(baseAlphaFade, maxAlphaFade, currentIntensity));
    }

    // Публичные методы для контроля из других скриптов
    public void SetIntensity(float intensity)
    {
        currentIntensity = Mathf.Clamp01(intensity);
        autoAnimateIntensity = false;
    }

    public void SetAutoAnimate(bool autoAnimate)
    {
        autoAnimateIntensity = autoAnimate;
    }

    public void TriggerDistortion(float targetIntensity, float duration)
    {
        StartCoroutine(AnimateToIntensity(targetIntensity, duration));
    }

    private System.Collections.IEnumerator AnimateToIntensity(float targetIntensity, float duration)
    {
        float startIntensity = currentIntensity;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        currentIntensity = targetIntensity;
    }

    public void ResetToBase()
    {
        currentIntensity = 0f;
        autoAnimateIntensity = true;
    }

    // Для отладки в инспекторе
    private void OnValidate()
    {
        if (distortionMaterial != null)
        {
            UpdateShaderParameters();
        }
    }
}
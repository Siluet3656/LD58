using UnityEngine;

public class WavyMoveToTarget : MonoBehaviour
{
    [Header("Цель движения")]
    public Transform target;

    [Header("Параметры движения")]
    public float moveSpeed = 3f;       // скорость движения
    public float waveAmplitude = 0.5f; // амплитуда извилин
    public float waveFrequency = 2f;   // частота извилин

    [Header("Исчезновение")]
    public float disappearDistance = 0.5f; // расстояние, при котором начинаем исчезать
    public float shrinkSpeed = 2f;         // скорость уменьшения

    private Vector3 initialScale;
    private bool isDisappearing = false;
    private float randomOffset;

    void Start()
    {
        initialScale = transform.localScale;
        randomOffset = Random.Range(0f, 100f); // уникальный сдвиг волны
    }

    void Update()
    {
        if (target == null) return;

        // Направление к цели (в 2D)
        Vector2 direction = (target.position - transform.position).normalized;

        // Извилистость — перпендикулярное направление в плоскости XY
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);
        Vector2 waveOffset = perpendicular * Mathf.Sin((Time.time + randomOffset) * waveFrequency) * waveAmplitude;

        // Итоговое направление движения
        Vector2 moveDir = (direction + waveOffset).normalized;

        // Перемещаем объект
        transform.position += (Vector3)(moveDir * moveSpeed * Time.deltaTime);

        // Проверяем расстояние до цели
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance < disappearDistance)
            isDisappearing = true;

        // Эффект исчезновения
        if (isDisappearing)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * shrinkSpeed);
            if (transform.localScale.magnitude < 0.06f)
                Destroy(gameObject);
        }
    }
}
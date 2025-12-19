using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabFieldPlacer : MonoBehaviour
{
    [Header("Pool Settings")]
    [Tooltip("Префаб для пула")]
    public GameObject prefab;

    [Tooltip("Количество объектов в пуле")]
    public int poolSize = 10;

    [Header("Field Settings (2D)")]
    public Vector2 fieldCenter = Vector2.zero;
    public Vector2 fieldSize = new Vector2(10f, 5f);

    [Header("Movement Settings")]
    [Tooltip("Минимальная скорость")]
    public float minSpeed = 1f;

    [Tooltip("Максимальная скорость")]
    public float maxSpeed = 3f;

    [Tooltip("Как часто меняется направление")]
    public float directionChangeTime = 1.5f;

    [Header("Behaviour")]
    public bool placeOnStart = true;

    private List<GameObject> pool = new List<GameObject>();
    private List<Coroutine> movementCoroutines = new List<Coroutine>();

    private void Start()
    {
        CreatePool();

        if (placeOnStart)
            HideAll();
    }

    /// <summary>
    /// СОЗДАНИЕ ПУЛА (объекты создаются сразу)
    /// </summary>
    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    /// <summary>
    /// СПАВН + ХАОТИЧНОЕ ПЕРЕМЕЩЕНИЕ
    /// НЕ МЕНЯТЬ ИМЯ МЕТОДА
    /// </summary>
    public void PlaceAll()
    {
        StopAllMovements();

        foreach (var obj in pool)
        {
            Vector2 startPos = GetRandomPosition();
            obj.transform.position = startPos;
            obj.SetActive(true);

            Coroutine c = StartCoroutine(RandomMoveRoutine(obj));
            movementCoroutines.Add(c);
        }
    }

    /// <summary>
    /// Скрыть все объекты
    /// </summary>
    public void HideAll()
    {
        StopAllMovements();

        foreach (var obj in pool)
            obj.SetActive(false);
    }

    private IEnumerator RandomMoveRoutine(GameObject obj)
    {
        Vector2 target;
        float speed;

        while (obj.activeSelf)
        {
            target = GetRandomPosition();
            speed = Random.Range(minSpeed, maxSpeed);

            while (Vector2.Distance(obj.transform.position, target) > 0.05f)
            {
                obj.transform.position = Vector2.MoveTowards(
                    obj.transform.position,
                    target,
                    speed * Time.deltaTime
                );

                yield return null;
            }

            yield return new WaitForSeconds(directionChangeTime);
        }
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(
            fieldCenter.x - fieldSize.x / 2f,
            fieldCenter.x + fieldSize.x / 2f
        );

        float y = Random.Range(
            fieldCenter.y - fieldSize.y / 2f,
            fieldCenter.y + fieldSize.y / 2f
        );

        return new Vector2(x, y);
    }

    private void StopAllMovements()
    {
        foreach (var c in movementCoroutines)
            if (c != null)
                StopCoroutine(c);

        movementCoroutines.Clear();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(fieldCenter, fieldSize);
    }
#endif
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeControllerScript : MonoBehaviour
{
    public List<Transform> targetPositions; // Глобальный список целевых позиций (в нужном порядке)
    public static int currentTargetIndex = 0; // Статический индекс (общий для всех)
    public GameObject yellowKeyPicker; // Ссылка на YellowKeyPicker

    void Start()
    {
        // Изначально скрываем YellowKeyPicker
        if (yellowKeyPicker != null)
        {
            yellowKeyPicker.SetActive(false);
        }
        else
        {
            Debug.LogError("YellowKeyPicker не назначен в ShapeController!");
        }
    }

    public static void IncrementTargetIndex()
    {
        currentTargetIndex++;
        Debug.Log("Индекс увеличен. Текущий индекс: " + currentTargetIndex); // Для отладки
    }

    // Метод для получения ожидаемого типа фигуры для текущей позиции
    public ShapeMover.ShapeType GetExpectedShapeTypeForCurrentPosition()
    {
        if (currentTargetIndex >= 0 && currentTargetIndex < targetPositions.Count)
        {
            ShapeMover targetShape = targetPositions[currentTargetIndex].GetComponent<ShapeMover>();
            if (targetShape != null)
            {
                return targetShape.shapeType;
            }
            else
            {
                Debug.LogError("На целевой позиции " + currentTargetIndex + " нет ShapeMover скрипта!");
                return ShapeMover.ShapeType.Triangle; // Значение по умолчанию
            }
        }
        else
        {
            return ShapeMover.ShapeType.Triangle; // Значение по умолчанию
        }
    }

    // Метод для активации YellowKeyPicker
    public void ActivateYellowKeyPicker()
    {
        if (yellowKeyPicker != null)
        {
            yellowKeyPicker.SetActive(true);
            Debug.Log("YellowKeyPicker активирован!");
        }
        else
        {
            Debug.LogError("YellowKeyPicker не назначен в ShapeController!");
        }
    }
}
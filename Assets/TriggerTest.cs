using UnityEngine;
using System.Collections;

public class ShapeMover : MonoBehaviour
{
    public enum ShapeType { Triangle, Square, Circle, Capsule }
    public ShapeType shapeType;
    public float moveSpeed = 5f;
    private Vector3 initialPosition;
    private bool isMoving = false;
    private bool isCorrectPosition = false;
    public ShapeControllerScript shapeController; // Ссылка на ShapeController

    void Start()
    {
        initialPosition = transform.position;
        isCorrectPosition = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isMoving && !isCorrectPosition)
        {
            if (shapeController != null && ShapeControllerScript.currentTargetIndex < shapeController.targetPositions.Count)
            {
                isMoving = true;
                StartCoroutine(MoveToTarget(shapeController.targetPositions[ShapeControllerScript.currentTargetIndex]));
            }
            else
            {
                // Все позиции пройдены (больше не двигаемся, но ничего не делаем)
                Debug.Log("Все позиции пройдены (OnTriggerEnter2D)!");
                enabled = false; // Отключаем скрипт, чтобы больше не реагировать на триггеры
                return; // Выходим из функции, чтобы избежать дальнейших действий
            }
        }
    }

    private System.Collections.IEnumerator MoveToTarget(Transform target)
    {
        while (Vector2.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target.position;
        StartCoroutine(CheckPosition());
    }

    private System.Collections.IEnumerator CheckPosition()
    {
        yield return new WaitForSeconds(0.1f);

        // Получаем ожидаемый тип фигуры из контроллера
        ShapeType expectedShapeType = shapeController.GetExpectedShapeTypeForCurrentPosition();

        if (expectedShapeType != shapeType)
        {
            Debug.Log("Неправильная позиция! Возвращаю " + shapeType + " назад.");
            StartCoroutine(MoveToInitialPosition());
        }
        else
        {
            Debug.Log(shapeType + " на правильной позиции!");
            isMoving = false;
            isCorrectPosition = true;

            // Увеличиваем индекс через контроллер
            ShapeControllerScript.IncrementTargetIndex();

            // Проверяем, все ли позиции пройдены после увеличения индекса
            if (ShapeControllerScript.currentTargetIndex >= shapeController.targetPositions.Count)
            {
                // Все позиции пройдены!  Активируем YellowKeyPicker
                Debug.Log("Все позиции пройдены (CheckPosition)!");
                shapeController.ActivateYellowKeyPicker();
                enabled = false; // Отключаем скрипт, чтобы больше не реагировать на триггеры
            }
        }
    }

    private System.Collections.IEnumerator MoveToInitialPosition()
    {
        while (Vector2.Distance(transform.position, initialPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = initialPosition;
        isMoving = false;
        isCorrectPosition = false;
    }
}
using UnityEngine;
using MoreMountains.CorgiEngine; // Добавляем пространство имен CorgiEngine

public class BEEBRA : MonoBehaviour
{
    public GameObject questionCanvas; // Ссылка на ваш Canvas с вопросом
    public GameManager gameManager; // Ссылка на GameManager

    private Collider2D _collider; // Хранит ссылку на триггер-коллайдер

    private void Start()
    {
        // Получаем компонент Collider2D
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, если игрок вошел в триггер
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Игрок коснулся чекпоинта!"); // Сообщение в консоль

            // Активируем викторину
            if (gameManager != null)
            {
                gameManager.ActivateQuiz();
            }
        }
    }

    // Метод для отключения триггера
    public void DisableTrigger()
    {
        if (_collider != null)
        {
            _collider.enabled = false; // Отключаем коллайдер
        }
    }
}
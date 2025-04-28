using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
    public QuestionManager questionManager; // Ссылка на QuestionManager
    public GameObject[] platforms; // Массив платформ, которые нужно активировать

    private void Start()
    {
        // Деактивируем все платформы при старте
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, если игрок вошел в триггер
        if (collision.CompareTag("Player"))
        {
            questionManager.StartQuestion(); // Запускаем вопрос

            // Активируем платформы
            foreach (GameObject platform in platforms)
            {
                platform.SetActive(true);
            }
        }
    }
}
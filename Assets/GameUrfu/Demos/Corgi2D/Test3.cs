using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject levelCompleteSplash; // Ссылка на объект LevelCompleteSplash

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, если игрок вошел в триггер
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Игрок достиг конца уровня!"); // Сообщение в консоль

            // Проверяем, что объект LevelCompleteSplash не равен null
            if (levelCompleteSplash != null)
            {
                levelCompleteSplash.SetActive(true); // Активируем объект LevelCompleteSplash
            }

            // Здесь можно добавить дополнительные действия, например, отключить управление игроком
            // playerController.enabled = false; // Если у вас есть ссылка на контроллер игрока
        }
    }
}
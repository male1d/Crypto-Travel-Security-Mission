using UnityEngine;

public class TruthLieBlock : MonoBehaviour
{
    // Ссылка на QuestionManager
    public QuestionManager questionManager;

    // Переменная для хранения текста сообщения
    public bool isTruth; // true, если это правда, false, если это ложь

    // Метод, который будет вызван при столкновении с игроком
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, если объект, с которым произошло столкновение, является игроком
        if (collision.CompareTag("Player"))
        {
            // Передаем ответ в QuestionManager
            questionManager.Answer(isTruth);
        }
    }
}
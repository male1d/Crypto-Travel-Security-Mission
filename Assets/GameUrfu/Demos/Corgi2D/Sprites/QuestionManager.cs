using System.Collections;
using UnityEngine;
using TMPro; // Не забудьте добавить это, если вы используете TextMeshPro

public class QuestionManager : MonoBehaviour
{
    public TextMeshPro questionText; // Ссылка на текст вопроса
    public TextMeshPro timerText; // Ссылка на текст таймера
    public GameObject[] platforms; // Массив платформ, которые нужно активировать или деактивировать
    public GameObject questionTrigger; // Ссылка на объект QuestionTrigger
    public float questionDuration = 15f; // Время на ответ
    private bool questionActive = false; // Флаг активности вопроса
    private int currentQuestionIndex = 0; // Индекс текущего вопроса
    private Coroutine timerCoroutine; // Ссылка на корутину таймера

    private string[] questions = {
        "Правда ли, что криптовалюты децентрализованы?",
        "Биткойн был создан в 2019 году?",
        "Правда ли, что блокчейн обеспечивает безопасность данных?"
    };

    private bool[] answers = { true, false, true }; // Массив правильных ответов

    private void Start()
    {
        // Скрываем текстовые элементы и платформы в начале
        questionText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        HidePlatforms(); // Скрываем платформы при старте
    }

    public void StartQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            questionActive = true;
            questionText.gameObject.SetActive(true); // Активируем текст вопроса
            timerText.gameObject.SetActive(true); // Активируем таймер
            questionText.text = questions[currentQuestionIndex]; // Устанавливаем текст вопроса

            // Запускаем таймер
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine); // Останавливаем старый таймер, если он существует
            }
            timerCoroutine = StartCoroutine(StartTimer(questionDuration));
        }
    }

    private IEnumerator StartTimer(float duration)
    {
        float timer = duration;
        while (timer > 0)
        {
            timerText.text = timer.ToString("F0"); // Обновляем текст таймера
            yield return new WaitForSeconds(1f);
            timer--;
        }

        // Если время истекло, сбрасываем вопрос
        questionActive = false;
        questionText.gameObject.SetActive(false); // Скрываем текст вопроса
        timerText.gameObject.SetActive(false); // Скрываем таймер
        currentQuestionIndex = 0; // Сбрасываем индекс
        HidePlatforms(); // Скрываем платформы, если время истекло
    }

    public void Answer(bool playerAnswer)
    {
        if (playerAnswer == answers[currentQuestionIndex]) // Проверяем ответ
        {
            currentQuestionIndex++;
            questionActive = false;
            questionText.gameObject.SetActive(false); // Скрываем текст вопроса
            timerText.gameObject.SetActive(false); // Скрываем таймер

            if (currentQuestionIndex < questions.Length)
            {
                StartQuestion(); // Запускаем следующий вопрос
            }
            else
            {
                Debug.Log("Поздравляем! Вы ответили на все вопросы.");
                HidePlatforms(); // Скрываем платформы, если все ответы правильные
                HideQuestionTrigger(); // Скрываем триггер
            }
        }
        else
        {
            Debug.Log("Неправильный ответ. Попробуйте снова.");
            questionActive = false;
            questionText.gameObject.SetActive(false); // Скрываем текст вопроса
            timerText.gameObject.SetActive(false); // Скрываем таймер
            currentQuestionIndex = 0; // Сбрасываем индекс
            HidePlatforms(); // Скрываем платформы, если ответ неправильный
        }
    }

    private void HidePlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false); // Деактивируем платформы
        }
    }

    private void HideQuestionTrigger()
    {
        if (questionTrigger != null)
        {
            questionTrigger.SetActive(false); // Деактивируем QuestionTrigger
        }
    }
}
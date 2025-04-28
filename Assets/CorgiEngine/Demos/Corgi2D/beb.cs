using UnityEngine;
using UnityEngine.UI;
using MoreMountains.CorgiEngine; // Добавляем пространство имен CorgiEngine

public class GameManager : MonoBehaviour
{
    public Text questionText;
    public InputField answerInputField;
    public Text answerDisplayText;
    public string correctAnswer = "крипта"; // Замените на свой ответ
    public GameObject pauseSplash; // Ссылка на интерфейс паузы
    public BEEBRA checkpoint; // Ссылка на ваш чекпоинт

    private void Start()
    {
        // Подписка на событие изменения текста в InputField
        answerInputField.onValueChanged.AddListener(UpdateAnswerDisplay);
    }

    private void UpdateAnswerDisplay(string input)
    {
        answerDisplayText.text = input;
    }

    public void CheckAnswer()
    {
        if (answerInputField.text.Trim().ToLower() == correctAnswer.ToLower())
        {
            Debug.Log("Ответ правильный! Уровень завершен.");

            // Скрываем интерфейс паузы, если он активен
            if (pauseSplash.activeSelf)
            {
                pauseSplash.SetActive(false);
            }

            // Снимаем паузу
            CorgiEngineEvent.Trigger(CorgiEngineEventTypes.TogglePause);

            // Скрываем Canvas с вопросом
            gameObject.SetActive(false);

            // Отключаем триггер
            if (checkpoint != null)
            {
                checkpoint.DisableTrigger();
            }
        }
        else
        {
            Debug.Log("Попробуйте еще раз.");
            answerInputField.text = ""; // Очищаем поле ввода
            answerDisplayText.text = ""; // Очищаем отображаемый ответ
        }
    }

    // Метод для активации викторины
    public void ActivateQuiz()
    {
        // Отображаем викторину и ставим игру на паузу
        gameObject.SetActive(true);
        CorgiEngineEvent.Trigger(CorgiEngineEventTypes.TogglePause);

        // Скрываем интерфейс паузы
        if (pauseSplash.activeSelf)
        {
            pauseSplash.SetActive(false);
        }
    }
}
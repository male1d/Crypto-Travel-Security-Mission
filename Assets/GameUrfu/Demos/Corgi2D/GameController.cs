using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public InputField[] inputFields; // Массив полей ввода
    private int currentIndex = 0;

    public void OnLetterButtonClicked(string letter)
    {
        if (currentIndex < inputFields.Length && string.IsNullOrEmpty(inputFields[currentIndex].text))
        {
            inputFields[currentIndex].text = letter;
            currentIndex++;
        }
        else if (currentIndex > 0)
        {
            currentIndex--;
            inputFields[currentIndex].text = string.Empty;
        }
    }
}
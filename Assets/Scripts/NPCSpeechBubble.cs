using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}

public class NPCSpeechBubble : MonoBehaviour
{
    [Header("Настройки текста")]
    public TextMeshProUGUI speechText;      // Ссылка на компонент текста
    public List<string> messages;           // Список сообщений
    public float changeInterval = 5f;       // Интервал смены сообщений в секундах

    private int currentMessageIndex = 0;

    void Start()
    {
        // Если список сообщений пуст, задаем стандартный набор
        if (messages == null || messages.Count == 0)
        {
            messages = new List<string>()
            {
                "Бегите, спасайся, кто может!",
                "Огонь! Немедленно эвакуируйтесь!",
                "Спасайтесь, пока есть возможность!"
            };
        }

        if (speechText == null)
        {
            Debug.LogError("Не назначен компонент speechText!");
            return;
        }

        // Устанавливаем первое сообщение
        speechText.text = messages[currentMessageIndex];

        // Запускаем цикл смены сообщений
        StartCoroutine(ChangeMessageRoutine());
    }

    IEnumerator ChangeMessageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeInterval);
            currentMessageIndex = (currentMessageIndex + 1) % messages.Count;
            speechText.text = messages[currentMessageIndex];
        }
    }
}

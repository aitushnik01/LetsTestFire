using UnityEngine;
using TMPro;

public class NPCSpeechBubble : MonoBehaviour
{
    public GameObject speechBubble; // Ссылка на объект облачка
    public float displayTime = 3f; // Время показа

    private void Start()
    {
        speechBubble.SetActive(false); // Скрываем при старте
    }

    public void ShowSpeech()
    {
        speechBubble.SetActive(true);
        Invoke("HideSpeech", displayTime);
    }

    private void HideSpeech()
    {
        speechBubble.SetActive(false);
    }
}

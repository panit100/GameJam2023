using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;

    public void TextPrompt(string _sizetext)
    {
        textField.gameObject.SetActive(true);
        textField.text = $"[E] to press {_sizetext} button";
    }

    public void CloseTextPrompt()
    {
        textField.gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI decision1;
    [SerializeField] TextMeshProUGUI decision2;

    public bool selection;

    public void SetTextBoxActive(bool isActive) 
    {
        image.gameObject.SetActive(isActive);
        
    }

    public void SetNameText(string _text)
    {
        nameText.text = _text;
    }
    public void SetDialogueText(string _text)
    {
        dialogueText.text = _text;
    }
    public void SetResponse1Text(string _text)
    {
        decision1.text = _text;
    }
    public void SetResponse2Text(string _text)
    {
        decision2.text = _text;
    }

    public void ChangeSelection() { 
        if (selection) { decision1.color = new Color(0, 40, 100); decision2.color = new Color(255, 255, 255); }
        else { decision2.color = new Color(0, 40, 100); decision1.color = new Color(255, 255, 255); }
    }

}

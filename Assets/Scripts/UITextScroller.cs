using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextScroller : MonoBehaviour
{
    public float timeBetweenChars = 0.05f;
    public float timeBetweenLines = 3f;

    public List<string> lines;

    private TextMeshProUGUI textComponent;
    private int currentLineIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ScrollText());
    }

    public void SetLines(List<string> newLines)
    {
        lines = newLines;
        currentLineIndex = 0;
        StartCoroutine(ScrollText());
    }

    // Coroutine to display text
    IEnumerator ScrollText()
    {
        while (currentLineIndex < lines.Count)
        {
            textComponent.text = ""; // Clear the text before displaying the next line
            string currentLine = lines[currentLineIndex];
            
            // Display one character at a time
            for (int i = 0; i < currentLine.Length; i++)
            {
                textComponent.text += currentLine[i];
                yield return new WaitForSeconds(timeBetweenChars);
            }

            // Wait before showing the next line
            currentLineIndex++;
            yield return new WaitForSeconds(timeBetweenLines);
            textComponent.text = "";
        }
    }
}
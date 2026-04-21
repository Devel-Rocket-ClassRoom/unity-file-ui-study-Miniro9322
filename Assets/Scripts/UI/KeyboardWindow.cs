using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class KeyboardWindow : GenericWindow
{
    public TextMeshProUGUI text;
    private float blinkTime = 0.5f;
    private float time = 0f;
    private bool blink = false;

    private readonly StringBuilder sb = new StringBuilder();



    public override void Open()
    {
        sb.Clear();
        time = 0f;
        blink = false;
        base.Open();
        UpdateInputField();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= blinkTime)
        {
            blink = !blink;
            UpdateInputField();
            time = 0f;
        }

    }

    private void UpdateInputField()
    {
        bool showCursor = sb.Length < 7 && blink == false;

        if(showCursor == true)
        {
            sb.Append('_');
        }
        text.SetText(sb);
        if (showCursor == true)
        {
            sb.Length -= 1;
        }
    }

    public void GetAlphabet(string c)
    {
        if(sb.Length < 7)
        {
            sb.Append(c);
            UpdateInputField();
        }
    }

    public void Delete()
    {
        if(sb.Length > 0)
        {
            sb.Length -= 1;
            UpdateInputField();
        }
    }

    public void Cancel()
    {
        sb.Clear();
        UpdateInputField();
    }

    public void Accept()
    {
        windowManager.Open(0);
    }
}
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;

    public Button nextButton;

    private bool showScore;
    private int score;
    private float elapsed;
    private float duration = 1f;
    private Coroutine coroutine;

    private string[] labels = new string[6];
    private string[] values = new string[6];


    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
        showScore = false;
    }

    private void OnEnable()
    {
        elapsed = 0f;
        score = Random.Range(0, 100000000);
        leftStatLabel.text = string.Empty;
        leftStatValue.text = string.Empty;
        rightStatLabel.text = string.Empty;
        rightStatValue.text = string.Empty;
        scoreValue.text = string.Empty.PadLeft(8, '0');
        nextButton.interactable = false;
    }

    private void Update()
    {
        if (nextButton.interactable == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
                leftStatLabel.text = $"{labels[0]}\n{labels[1]}\n{labels[2]}";
                leftStatValue.text = $"{values[0]}\n{values[1]}\n{values[2]}";
                rightStatLabel.text = $"{labels[3]}\n{labels[4]}\n{labels[5]}";
                rightStatValue.text = $"{values[3]}\n{values[4]}\n{values[5]}";
                scoreValue.text = $"{score}".PadLeft(8, '0');
                nextButton.interactable = true;
            }
        }

        if(showScore == true)
        {
            elapsed += Time.deltaTime;
            var t = elapsed / duration;

            var temp = Mathf.RoundToInt(Mathf.Lerp(0f, score, t));
            if(t >= 1f)
            {
                temp = score;
                showScore = false;
                nextButton.interactable = true;
            }
            scoreValue.text = temp.ToString().PadLeft(8, '0');
        }
    }

    public override void Open()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        base.Open();
        Show();
    }

    public override void Close()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        base.Close();
    }

    public void OnNext()
    {
        windowManager.Open(0);
    }

    private IEnumerator CoShowState(string[] labels, string[] values)
    {
        for(int i = 0;  i < labels.Length; i++)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            if(i < 3)
            {
                for (int j = 0; j <= i; j++)
                {
                    sb.AppendLine(labels[j]);
                    sb2.AppendLine(values[j]);
                }
                var labeltext = sb.ToString();
                var valuetext = sb2.ToString();

                yield return new WaitForSeconds(1f);

                leftStatLabel.text = labeltext;
                leftStatValue.text = valuetext;

                
            }
            else
            {
                for (int j = 3; j <= i; j++)
                {
                    sb.AppendLine(labels[j]);
                    sb2.AppendLine(values[j]);
                }
                var labeltext = sb.ToString();
                var valuetext = sb2.ToString();
                
                yield return new WaitForSeconds(1f);

                rightStatLabel.text = labeltext;
                rightStatValue.text = valuetext;

            }
        }

        showScore = true;
    }

    private void Show()
    {
        labels = new string[6];
        values = new string[6];

        for (int i = 0; i < 6; i++)
        {
            labels[i] = $"stat {i}";
            values[i] = $"{Random.Range(0, 10000)}".PadLeft(4, '0');
        }

        coroutine = StartCoroutine(CoShowState(labels, values));
    }
}

using System.Collections;
using System.Collections.Generic;
using CharTween;
using DG;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LessonText : MonoBehaviour
{
    public TMP_Text textMesh;

    private bool charIsPeriod = false;

    private int counter = 0;

    private string[]
        lessons =
        {
            "Stand outside and look around you. You will see land. Land is made out of rocks and soil. Let's spell Rocks.\n'R'. 'O'. 'C'. 'K'. 'S'.",
            "Minerals are tiny solids found in nature.\nThey have never been alive.\nHow do you spell 'alive'?\n 'A'. 'L'. 'I'. 'V'. 'E'.",
            "A lot of the rocks are under the soil.Rocks are solid things made out of one or more minerals. Let's spell minerals.\n'M', 'I', 'N', 'E', 'R', 'A', 'L', 'S'"
        };

    /**
     * Seconds
     */
    public float waitTime = 2000.0f;

    /**
     * Mili-seconds?
     */
    public float fadeDuration = 40.0f;

    private Queue textChars;

    private string text;

    private float timer = 0.0f;

    private CharTweener tweener;

    private bool fadeOut = false;

    private float fadeSpeed = 0.1f;

    private int currentLessonIndex = 0;

    // https://github.com/mdechatech/CharTweener
    // Start is called before the first frame update
    void Start()
    {
        text = lessons[currentLessonIndex];
        textMesh.color = new Color32(255, 255, 255, 0);
        textChars = new Queue();
        PlayLesson();
    }

    void PlayLesson()
    {
        Debug.Log("Play Lesson" + currentLessonIndex.ToString());
        text = lessons[currentLessonIndex];
        textMesh.SetText (text);
        foreach (char i in text.ToCharArray())
        {
            textChars.Enqueue (i);
        }
        timer = 0.0f;
        counter = 0;
        charIsPeriod = false;
        tweener = textMesh.GetCharTweener();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 1000;

        if (textChars.Count == 0)
        {
            Debug.Log("Text Length " + textChars.Count.ToString());
            currentLessonIndex =
                Mathf.Min(lessons.Length - 1, ++currentLessonIndex);
            PlayLesson();
        }
        if (charIsPeriod)
        {
            counter++;
            timer = timer - waitTime;

            // todo play audio
            if (counter > 10)
            {
                charIsPeriod = false;
                counter = 0;
            }
            return;
        }
        if (textChars.Count > 0)
        {
            if (timer > waitTime)
            {
                timer = timer - waitTime;
                Tween fadeTween =
                    tweener
                        .DOFade(text.Length - textChars.Count,
                        255,
                        fadeDuration)
                        .SetEase(Ease.InSine);

                char? nextChar = textChars.Dequeue() as char?;
                charIsPeriod = (nextChar == '.' || nextChar == '?');
                Debug.Log("Next Char is" + nextChar);
            }
        }
    }
}

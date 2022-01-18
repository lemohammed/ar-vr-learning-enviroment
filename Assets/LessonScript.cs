using CharTween;
using DG.Tweening;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextAnimationFadeScript : MonoBehaviour
{
    public TMP_Text textMesh;
    public string text;
   
    /**
     * Seconds
     */
    public float waitTime = 2.0f;
    /**
     * Mili-seconds?
     */
    public float fadeDuration = 40.0f;

    private Queue textChars;
    private float timer = 0.0f;
    private CharTweener tweener;
    // https://github.com/mdechatech/CharTweener
    // Start is called before the first frame update
    void Start()
    {
        textMesh.color = new Color32(255, 255, 255, 0);
        textMesh.SetText(text);
        textChars = new Queue(text.ToCharArray());

        tweener = textMesh.GetCharTweener();
    }

    // Update is called once per frame
    void Update()
    {
        if (textChars.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                timer = timer - waitTime;
                // todo play audio?
                Tween fadeTween = tweener.DOFade(text.Length - textChars.Count, 255, fadeDuration).SetEase(Ease.InSine);

                textChars.Dequeue();
                while (textChars.Count > 0 && textChars.Peek().Equals(' '))
                {
                    textMesh.text += textChars.Dequeue();
                }
            }
        }
    }
}

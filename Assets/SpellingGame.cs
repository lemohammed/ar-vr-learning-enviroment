using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Debug = UnityEngine.Debug;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.UI;
using TMPro;
using S = System;
using System.Diagnostics;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using static file.logger.Logger;


public class SpellingGame : MonoBehaviour
{
    [SerializeField] AudioSource audioData;
    private string[] words = new string[]{"Sand","Rocks","Gravel","Fold","Stone","Pebble","Core"};
    [SerializeField] TextMeshPro buttonText;
    private char[] alphabet = new char[]{'A', 'S', 'L', 'E', 'P', 'W', 'V', 'R', 'I', 'M', 'O', 'Z', 'J', 'K', 'B', 'N', 'H', 'G', 'F', 'D', 'Q', 'X', 'C', 'T', 'U', 'Y'};
    [SerializeField] TextMeshPro wordToSpell;
    [SerializeField] TextMeshPro[] otherButtons;
    private S.Random r = new S.Random();

    private int wordIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick(){
        bool isCorrect  = wordToSpell.text.Substring(0, 1) == buttonText.text;
        char[] chosenChars = new char[]{};
        if(isCorrect){
            audioData.Play(0);
            if(wordIndex == (words.Length-1)){
                wordIndex = 0;
            }else{
                wordIndex++;
            }
            wordToSpell.text = words[wordIndex];
            char nextChar = char.Parse(words[wordIndex].Substring(0, 1));
            var correctIndex = r.Next(0,otherButtons.Length);
            int[] numbers = {0,1,2};
            foreach (int i in numbers)
            {
                if(i==correctIndex){
                    otherButtons[i].text=nextChar.ToString();
                } else{
                int arrayIndex= r.Next(0,alphabet.Length);
                char chosenChar = alphabet[arrayIndex];
                    while(chosenChar==nextChar){
                    arrayIndex = r.Next(0,alphabet.Length);
                    chosenChar = alphabet[arrayIndex];
                    }
                otherButtons[i].text=chosenChar.ToString();
                }
            }
        }
    }
}

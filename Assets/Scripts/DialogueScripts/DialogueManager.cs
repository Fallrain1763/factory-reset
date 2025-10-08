using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueScripts
{
    public class DialogueManager : MonoBehaviour {

        public TMP_Text nameText;
        public TMP_Text dialogueText;
        public GameObject dialogueBox;

        private Queue<string> _sentences;

        // Use this for initialization
        void Start () {
            _sentences = new Queue<string>();
        }

        public void StartDialogue (Dialogue dialogue)
        {
            GlobalGameState.dialogueActive = true;
            
            GlobalGameState.swallowNextSpace = true;
            
            Time.timeScale = 0f;
            
            if (dialogueBox) dialogueBox.SetActive(true);
                
            nameText.text = dialogue.name;

            _sentences.Clear();

            if (dialogue.hacked)
            {
                foreach (string sentence in dialogue.hackedSentences)
                {
                    _sentences.Enqueue(sentence);
                }
            }
            else
            {
                foreach (string sentence in dialogue.sentences)
                {
                    _sentences.Enqueue(sentence);
                }
            }

            DisplayNextSentence();

        }

        public void DisplayNextSentence ()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence (string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && GlobalGameState.dialogueActive)
            {
                if (GlobalGameState.swallowNextSpace)
                {
                    GlobalGameState.swallowNextSpace = false; 
                    return;
                }
                DisplayNextSentence();
            }
        }

        void EndDialogue()
        {
            GlobalGameState.dialogueActive = false;
            
            Time.timeScale = 1f;
            
            if (dialogueBox) dialogueBox.SetActive(false);
            
            
            Debug.Log("Ending dialogue");
        }

    }
}
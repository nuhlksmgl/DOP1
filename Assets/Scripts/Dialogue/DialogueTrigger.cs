using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DialogueTrigger : MonoBehaviour
{
   [Header("Visual Cue")] 
   [SerializeField] private GameObject visualCue;

   [Header("Ink JSON")] 
   [SerializeField] private TextAsset inkJSON;
   
   private bool _playerInRange;
   private void Awake()
   {
      _playerInRange = false;
      visualCue.SetActive(false);
   }

   private void Update()
   {
      if (_playerInRange)
      {
         visualCue.SetActive(true);
         if (InputManager.GetInstance().GetInteractPressed())
         {
          Debug.Log(inkJSON.text);  
         }
      }
      else
      {
         visualCue.SetActive(false);
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.tag == "Player")
      {
         _playerInRange = true;
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.gameObject.tag == "Player")
      {
         _playerInRange = false;  
      }
   }
}

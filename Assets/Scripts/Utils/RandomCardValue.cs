using System;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame.Core.Utils
{
    public class RandomCardValue : MonoBehaviour
    {
        [SerializeField] private Button randomButton;
        
        public void Setup(Action onClick)
        {
            randomButton.onClick.RemoveAllListeners();
            randomButton.onClick.AddListener(() => onClick());
        }
    }
}

using CardGame.Networking.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame.Core.UI
{
    public class UICard : MonoBehaviour
    {
        private int index;
        public int Index => index;
        
        [Header("HEAD")]
        [SerializeField] private RawImage artImage;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [Header("POWER")]
        [SerializeField] private TextMeshProUGUI attackText;
        [SerializeField] private TextMeshProUGUI manaText;
        [SerializeField] private TextMeshProUGUI hpText;

        public void Setup(CardModel dataModel)
        {
            index = dataModel.index;
            
            artImage.texture = dataModel.icon;
            titleText.text = $"{dataModel.hp }{dataModel.mana }{dataModel.attack }";
            descriptionText.text = $"HP: {dataModel.hp} \n" +
                                   $"Mana: {dataModel.mana} \n" +
                                   $"Attack: {dataModel.attack}";
            
            attackText.text = dataModel.attack.ToString();
            manaText.text = dataModel.mana.ToString();
            hpText.text = dataModel.hp.ToString();
        }
    }
}
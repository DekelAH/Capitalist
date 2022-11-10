using Assets.Scripts.Infastructure;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Capitalist.Views
{
    public class ProductionItemView : MonoBehaviour
    {
        #region Editor

        [Header("Unity Components")]
        [SerializeField]
        private TMP_Text _producePriceText;

        [SerializeField]
        private TMP_Text _sellPriceText;

        [SerializeField]
        private TMP_Text _productNameText;

        [SerializeField]
        private TMP_Text _productionTimeText;
        
        [SerializeField]
        private Slider _productionProgressSlider;

        [SerializeField]
        private Image _productPreviewImage;

        [SerializeField]
        private Button _buyButton;
        
        [Header("Item Data")]
        [SerializeField]
        private string _productName;
        
        [SerializeField]
        [Range(1, 2000)]
        private int _producePrice;
        
        [SerializeField]
        [Range(1, 2000)]
        private int _sellPrice;

        [SerializeField]
        private Sprite _productPreviewSprite;
        
        [SerializeField]
        [Range(1, 10)]
        private int _productionTimeSeconds;
        
        #endregion

        #region Methods

        private void Start()
        {
            _buyButton.onClick.AddListener(OnBuyButtonClick);
        }


        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonClick);
        }

        private void OnValidate()
        {
            _producePriceText.text = _producePrice.ToString();
            _sellPriceText.text = _sellPrice.ToString();
            _productNameText.text = _productName;
            _productionTimeText.text = _productionTimeSeconds.ToString();
            _productPreviewImage.sprite = _productPreviewSprite;
        }

        private void OnBuyButtonClick()
        {
            if (CheckBalanceSufficient(_producePrice))
            {
                BeginProduction();
            }
        }

        private bool CheckBalanceSufficient(int producePrice)
        {
            var playerModel = PlayerModelProvider.Instance.GetPlayerModel;
            if (playerModel.CoinsBalance >= producePrice)
            {
                playerModel.WithdrawCoins(producePrice);
                playerModel.AddCoins(_sellPrice);
                return true;
            }

            else
            {
                Debug.LogWarning("Not Enough Coins!");
                return false;
            }
        }

        private void BeginProduction()
        {
            StartCoroutine(ProductionCoroutine(_productionTimeSeconds, OnProductionStart, OnProductionEnd, OnProductionProgress));
        }

        private IEnumerator ProductionCoroutine(float productionTimeProgress, Action beginProductionCallback, Action endProductionCallback, Action<float> progressProductionCallback)
        {
            beginProductionCallback?.Invoke();

            var time = 0f;
            while (time < productionTimeProgress)
            {
                var productionProgress = time / productionTimeProgress;
                progressProductionCallback?.Invoke(productionProgress);
                time += Time.deltaTime;
                yield return null;
            }

            endProductionCallback?.Invoke();
        }

        private void OnProductionProgress(float progress)
        {
            _productionProgressSlider.value = progress;
        }

        private void OnProductionStart()
        {
            IsInProduction = true;
            _buyButton.interactable = false;
        }

        private void OnProductionEnd()
        {
            IsInProduction = false;
            _buyButton.interactable = true;
        }

        #endregion

        #region Properties

        private bool IsInProduction { get; set; }

        #endregion
    }
}
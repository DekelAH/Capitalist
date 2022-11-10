using Assets.Scripts.Infastructure;
using TMPro;
using UnityEngine;

namespace Capitalist.Views
{
    public class CoinsBalanceView : MonoBehaviour
    {
        #region Editor

        [Header("Unity Components")]
        [SerializeField]
        private TMP_Text _coinsBalanceText;
        
        #endregion

        #region Methods

        private void Awake()
        {
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }

        private void Start()
        {
            InitializeCoinBalance();
            SubscribeToEvents();
        }

        private void InitializeCoinBalance()
        {
            var playerModel = PlayerModelProvider.Instance.GetPlayerModel;
            _coinsBalanceText.text = playerModel.CoinsBalance.ToString();
        }

        private void SubscribeToEvents()
        {
            var playerModel = PlayerModelProvider.Instance.GetPlayerModel;
            playerModel.CoinBalanceChange += UpdateBalance;
        }

        private void UnsubscribeToEvents()
        {
            var playereModel = PlayerModelProvider.Instance.GetPlayerModel;
            playereModel.CoinBalanceChange -= UpdateBalance;
        }

        private void UpdateBalance(int coinsAmount)
        {
            _coinsBalanceText.text = coinsAmount.ToString();
        }

        #endregion
    }
}
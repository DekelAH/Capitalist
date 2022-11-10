using System;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [CreateAssetMenu(menuName = "Data Models/Player Model", fileName = "Player Model")]
    public class PlayerModel : ScriptableObject
    {
        #region Events

        public event Action<int> CoinBalanceChange;

        #endregion

        #region Editor

        [SerializeField]
        private int _coinsBalance;

        #endregion

        #region Methods

        public void AddCoins(int coinsToAdd)
        {
            _coinsBalance += coinsToAdd;
            CoinBalanceChange?.Invoke(_coinsBalance);
        }

        public void WithdrawCoins(int coinsToWithdraw)
        {
            _coinsBalance = Mathf.Max(0, _coinsBalance - coinsToWithdraw);
            CoinBalanceChange?.Invoke(_coinsBalance);
        }

        #endregion

        #region Properties

        public int CoinsBalance => _coinsBalance;

        #endregion
    }
}

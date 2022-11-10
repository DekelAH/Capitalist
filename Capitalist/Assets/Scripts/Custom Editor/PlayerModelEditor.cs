using Assets.Scripts.Models;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Custom_Editor
{
    [CustomEditor(typeof(PlayerModel))]
    public class PlayerModelEditor : Editor
    {
        #region Consts

        private const int COINS_TO_ADD = 10;
        private const int COINS_TO_WITHDRAW = 5;

        #endregion

        #region Methods

        public override void OnInspectorGUI()
        {
            PlayerModel playerModel = (PlayerModel)target;

            EditorGUILayout.BeginHorizontal();

            GUILayout.Label("Coins Balance", EditorStyles.boldLabel);
            GUILayout.TextField(playerModel.CoinsBalance.ToString());

            if (GUILayout.Button(" + "))
            {
                playerModel.AddCoins(COINS_TO_ADD);
            }
            if (GUILayout.Button(" - "))
            {
                playerModel.WithdrawCoins(COINS_TO_WITHDRAW);
            }

            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}

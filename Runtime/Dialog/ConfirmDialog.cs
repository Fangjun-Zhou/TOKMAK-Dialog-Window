using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace FinTOKMAK.DialogWindow.Runtime.Dialog
{
    public class ConfirmDialog : MonoBehaviour
    {
        #region Public Field

        public Action onConfirm;

        [BoxGroup("UI Components")]
        public TMP_Text title;

        [BoxGroup("UI Components")]
        public TMP_Text content;

        #endregion

        /// <summary>
        /// Confirm button clicked.
        /// </summary>
        public void Confirm()
        {
            onConfirm?.Invoke();
            Destroy(gameObject);
        }
    }
}
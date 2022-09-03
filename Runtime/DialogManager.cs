using System;
using System.Collections;
using FinTOKMAK.DialogWindow.Runtime.Dialog;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Localization;

namespace FinTOKMAK.DialogWindow.Runtime
{
    public class DialogManager : MonoBehaviour
    {
        #region Singleton

        public static DialogManager Instance = null;

        #endregion

        #region Public Field

        [BoxGroup("Dialog Prefabs")]
        public ConfirmDialog confirmDialog;

        #endregion

        private void Awake()
        {
            // Register singleton.
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            
            // Make singleton don't destroy on load.
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            Destroy(this);
            Instance = null;
        }

        #region Dialog Methods

        public IEnumerator ShowInfoDialog(LocalizedString title, LocalizedString content)
        {
            // Create dialog
            ConfirmDialog dialog = Instantiate(confirmDialog, gameObject.transform);
            bool hasConfirmed = false;

            // Setup dialog
            void OnTitleChanged(string title)
            {
                dialog.title.text = title;
            }

            void OnContentChanged(string content)
            {
                dialog.content.text = content;
            }
            
            dialog.title.text = title.GetLocalizedString();
            title.StringChanged += OnTitleChanged;
            dialog.content.text = content.GetLocalizedString();
            content.StringChanged += OnContentChanged;

            dialog.onConfirm += () =>
            {
                title.StringChanged -= OnTitleChanged;
                content.StringChanged -= OnContentChanged;

                hasConfirmed = true;
            };

            while (!hasConfirmed)
            {
                yield return null;
            }
        }

        #endregion
    }
}
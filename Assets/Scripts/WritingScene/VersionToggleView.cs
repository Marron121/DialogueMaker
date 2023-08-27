using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace DM.Writing
{
    public class VersionToggleView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Toggle toggle;
        [SerializeField] private TextMeshProUGUI versionText;
        private int version;
        private ToggleGroup toggleGroup;

        public void Init(int v, ToggleGroup tg, UnityAction<int> callback)
        {
            version = v;
            toggleGroup = tg;
            versionText.text = version.ToString();
            toggle.onValueChanged.AddListener((value) =>
            {
                if (value) callback.Invoke(version);
            });
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DM.Writing
{
    public class VersionListView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject versionToggle;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private Transform list;
        [Header("Control")]
        [SerializeField] private int numberOfVersions = -1;
        public void Init()
        {

        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using JW.PLink;

namespace XH.SlotScroll {
    public class SlotScrollGrid : MonoBehaviour {
        [SerializeField] private PrefabLink _plink;
        private int _index = 0;//显示的index

        public Transform Root;

        private void Awake()
        {
            _plink = GetComponent<PrefabLink>();
            Root = transform;
        }

        public int Index
        {
            get { return _index; }
            set {
                _index = value;
                (_plink.GetCacheComponent(1) as Text).text = _index.ToString();
            }
        }

        public void Clear()
        {
            _index = 0;
        }

        public PrefabLink GetPrefabLink()
        {
            return _plink;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICSharpCode.SharpZipLib
{
    public class UnZipperMono : MonoBehaviour
    {
        public static UnZipperMono GetUnZipper(string zipPath,string extractDir, System.Action<bool, float> handler)
        {
            GameObject go = new GameObject("UnZipperMono");
            UnZipperMono ret = go.AddComponent<UnZipperMono>();
            ret.Init(zipPath, extractDir, handler);
            return ret;
        }

        private UnZipper _zipper;
        private System.Action<bool, float> _handler;

        void Init(string zipPath, string extractDir, System.Action<bool, float> handler)
        {
            _zipper = new UnZipper();
            _zipper.Begin(zipPath, extractDir, "");
            //
            _handler = handler;
        }

        public void Begin()
        {
            if (_zipper != null)
            {
                _zipper.Start();
            }
        }

        public void Stop()
        {
            if (_zipper != null)
            {
                _zipper.Stop();
            }
            //
            DestroyObject(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (_zipper != null)
            {
                if (_handler != null)
                {
                    _handler(_zipper.IsDone, _zipper.CurProgress);
                }
            }
        }
    }
}

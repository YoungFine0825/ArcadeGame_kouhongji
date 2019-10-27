using UnityEngine;
using System.Collections;

namespace JW.Framework.Network
{
    public interface IReference
    {
        void Retain();

        void Release();

        IReference AutoRelease();

        int GetReferenceCount();

        int GetAutoReleaseCount();
    }

}

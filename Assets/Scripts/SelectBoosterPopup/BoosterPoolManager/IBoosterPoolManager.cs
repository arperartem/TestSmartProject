using System;
using System.Collections.Generic;
using Core.Data;
using UnityEngine;

namespace SelectBoosterPopup.BoosterPoolManager
{
    public interface IBoosterPoolManager
    {
        event Action SelectBoosterChanged;
        List<SelectBoosterView> BoosterPool { get; }
        void Initialize(BoosterType[] initialBoosters, Transform containerBoosters);
        void UpdateBoosters(BoosterType[] newBoosters);
        SelectBoosterView SelectedBooster { get; }
    }
}
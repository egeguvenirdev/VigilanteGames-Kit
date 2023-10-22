﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstantVariables
{
    public static class LevelValue
    {
        public const string Level = nameof(Level);
    }

    public static class LevelStats
    {
        public const string Lv = nameof(Lv);
        public const string SkillLevel = nameof(SkillLevel);
    }

    public static class UpgradeValues
    {
        public const string UpgradeCurrentValue = nameof(UpgradeCurrentValue);
        public const string UpgradeIncrementalValue = nameof(UpgradeIncrementalValue);
    }

    public static class UpgradePrices
    {
        public const string CurrentPrice = nameof(CurrentPrice);
        public const string IncrementalPrice = nameof(IncrementalPrice);
    }

    public static class UpgradeTypes
    {
        public const string Income = nameof(Income);
        public const string Power = nameof(Power);
        public const string Size = nameof(Size);
    }

    public static class BuyBallButton
    {
        public const string BuyButton = nameof(BuyButton);
    }
}

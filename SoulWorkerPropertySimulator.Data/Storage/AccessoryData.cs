﻿using System.Collections.Generic;
using System.Linq;
using SoulWorkerPropertySimulator.Models.Accessory;
using SoulWorkerPropertySimulator.Models.Effects;
using SoulWorkerPropertySimulator.Types;

namespace SoulWorkerPropertySimulator.Data.Storage
{
    internal static class AccessoryData
    {
        private static IReadOnlyCollection<AccessoryBlueprint>? _blueprints;

        private static readonly Dictionary<AccessoryField, IReadOnlyCollection<AccessoryBlueprint>> Result = new();

        internal static IReadOnlyCollection<AccessoryBlueprint> Get(AccessoryField field)
        {
            if (Result.ContainsKey(field)) { return Result[field]; }

            if (_blueprints != null) { return Result[field] = _blueprints.Where(x => x.Field == field).ToList(); }

            var blueprints = new List<AccessoryBlueprint>
            {
                new("閃耀",
                    68,
                    AccessoryField.Earrings,
                    4,
                    new RandomEffect[]
                    {
                        new(StaticEffectContext.Attack, 36, 321),
                        new(StaticEffectContext.Defense, 23, 534),
                        new(StaticEffectContext.AttackSpeedRate, .01m, .07m),
                        new(StaticEffectContext.Stamina, 4, 10),
                        new(StaticEffectContext.MoveSpaceRate, .01m, .07m),
                        new(StaticEffectContext.ExpVolumeRateEnemy, .02m, .15m),
                        new(StaticEffectContext.KillHpRecovery, 20, 180),
                        new(StaticEffectContext.SuperArmorBreakPowerRate, .02m, .08m),
                        new(StaticEffectContext.CooldownShorterRate, .01m, .05m),
                        new(StaticEffectContext.MoneyVolumeRateEnemy, .02m, .08m),
                        new(StaticEffectContext.SoulNovaVolumeRate, .01m, .04m),
                        new(StaticEffectContext.ExtraDamageRateAir, .02m, .12m),
                        new(StaticEffectContext.ExtraDamageRateFall, .02m, .12m)
                    },
                    ItemRare.Legendary,
                    "暮光"),
                new("邊緣",
                    68,
                    AccessoryField.Amulet,
                    4,
                    new RandomEffect[]
                    {
                        new(StaticEffectContext.Attack, 36, 321),
                        new(StaticEffectContext.Defense, 23, 534),
                        new(StaticEffectContext.AttackSpeedRate, .01m, .07m),
                        new(StaticEffectContext.Stamina, 4, 10),
                        new(StaticEffectContext.DamageReductionRate, .01m, .05m),
                        new(StaticEffectContext.ExpVolumeRateEnemy, .02m, .15m),
                        new(StaticEffectContext.KillHpRecovery, 20, 180),
                        new(StaticEffectContext.SuperArmorBreakPowerRate, .02m, .08m),
                        new(StaticEffectContext.CooldownShorterRate, .01m, .05m),
                        new(StaticEffectContext.MoneyVolumeRateEnemy, .02m, .08m),
                        new(StaticEffectContext.SoulNovaVolumeRate, .01m, .04m),
                        new(StaticEffectContext.ExtraDamageRateAir, .02m, .12m),
                        new(StaticEffectContext.ExtraDamageRateFall, .02m, .12m)
                    },
                    ItemRare.Legendary,
                    "暮光")
            };
            var r68 = new AccessoryBlueprint("之冠I",
                68,
                AccessoryField.Ring,
                4,
                new RandomEffect[]
                {
                    new(StaticEffectContext.Attack, 19, 190),
                    new(StaticEffectContext.Defense, 16, 423),
                    new(StaticEffectContext.CriticalRate, .01m, .07m),
                    new(StaticEffectContext.CriticalDamage, 250, 694),
                    new(StaticEffectContext.Accuracy, 10, 108),
                    new(StaticEffectContext.Evade, 7, 36),
                    new(StaticEffectContext.DamageReductionRateBasic, .01m, .1m),
                    new(StaticEffectContext.DamageReductionRateBoss, .01m, .1m),
                    new(StaticEffectContext.DamageReductionRatePartialDamage, .02m, .12m),
                    new(StaticEffectContext.ExpVolumeRateEnemy, .02m, .15m),
                    new(StaticEffectContext.MoneyVolumeRateEnemy, .02m, .08m),
                    new(StaticEffectContext.SoulNovaVolumeRate, .01m, .04m)
                },
                ItemRare.Legendary,
                "暮光");
            blueprints.Add(r68);
            blueprints.Add(r68 with {Name = "之冠II"});

            _blueprints = blueprints;
            return Result[field] = _blueprints.Where(x => x.Field == field).ToList();
        }
    }
}
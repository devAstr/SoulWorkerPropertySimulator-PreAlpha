﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SoulWorkerPropertySimulator.Models
{
    public record AccessoryBlueprint : Blueprint<Accessory>
    {
        public AccessoryBlueprint(string                                   name,
                                  int                                      level,
                                  AccessoryField                           field,
                                  IReadOnlyCollection<EffectRandomContext> randomEffects,
                                  int                                      randomEffectCount,
                                  string?                                  setName      = null,
                                  IReadOnlyCollection<Effect>?             fixedEffects = null) : base(name,
            level,
            randomEffects,
            randomEffectCount,
            null,
            setName,
            fixedEffects) =>
            Field = field;

        public AccessoryField Field { get; }

        public override Accessory Create(IReadOnlyCollection<Effect> randomEffects) => new(this, randomEffects);
    }

    public record Accessory : Item, ICreatable<AccessoryBlueprint>
    {
        internal Accessory(AccessoryBlueprint blueprint, IReadOnlyCollection<Effect> randomEffects) : base(
            blueprint.Name,
            blueprint.SetName)
        {
            if (!blueprint.CheckEffectAllowed(randomEffects)) { throw new InvalidOperationException(); }

            Blueprint      = blueprint;
            SelectedEffect = randomEffects;
        }

        public override IReadOnlyCollection<Effect> Effects => Blueprint.FixedEffects.Concat(SelectedEffect).ToList();

        public AccessoryField              Field          => Blueprint.Field;
        public AccessoryBlueprint          Blueprint      { get; }
        public IReadOnlyCollection<Effect> SelectedEffect { get; }
    }

    public record AccessorySetEffect : Set, IUpgradeable
    {
        public AccessorySetEffect(string name, IReadOnlyDictionary<int, IReadOnlyCollection<Effect>> stepEffect) :
            base(name)
        {
            StepEffects = stepEffect;
            ValidStep   = Enumerable.Range(0, stepEffect.Select(x => x.Key).Max()).ToList();
        }

        public override IReadOnlyCollection<Effect> Effects =>
            StepEffects.Where(x => x.Key <= Step).SelectMany(x => x.Value).ToList();

        public int                                                   Step        { get; init; }
        public IReadOnlyDictionary<int, IReadOnlyCollection<Effect>> StepEffects { get; }
        public IReadOnlyCollection<int>                              ValidStep   { get; }
    }
}
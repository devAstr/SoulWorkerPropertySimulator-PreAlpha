﻿using System;
using System.Collections.Generic;
using System.Linq;
using SoulWorkerPropertySimulator.Extensions;

namespace SoulWorkerPropertySimulator.Models
{
    public record Brooches : Item
    {
        private readonly IReadOnlyDictionary<BroochesRare, IReadOnlyCollection<Effect>> _effects;
        private readonly BroochesRare                                                   _rare;

        public Brooches(string name,
                        BroochesSeries series,
                        BroochesType type,
                        IReadOnlyDictionary<BroochesRare, IReadOnlyCollection<Effect>> effects,
                        BroochesRare rare = BroochesRare.Tera) : base(name)
        {
#if DEBUG
            if (!name.Equals("對策"))
            {
                var cache = 0m;
                foreach (var (_, effect) in effects.OrderBy(x => x.Key))
                {
                    if (cache == 0m) { cache = effect.First().Value; }
                    else
                    {
                        if (cache > effect.First().Value) { throw new IndexOutOfRangeException(); }

                        if (cache == effect.First().Value && effect.Count != 2)
                        {
                            throw new InvalidOperationException();
                        }

                        cache = effect.First().Value;
                    }
                }
            }
#endif

            Series   = series;
            Type     = type;
            _effects = effects;
            Rare     = rare;
        }

        public override IReadOnlyCollection<Effect>       Effects   => _effects[Rare];
        public          IReadOnlyCollection<BroochesRare> ValidRare => _effects.Keys.ToList();

        public BroochesRare Rare
        {
            get => _rare;
            init
            {
                if (!ValidRare.Contains(value)) { throw new IndexOutOfRangeException(); }

                _rare = value;
            }
        }

        public BroochesSeries Series { get; init; }
        public BroochesType   Type   { get; }

        public string DisplayName => $"{Series.GetDescription()}：{Name}";
    }

    public record BroochesSetEffect : Item
    {
        public BroochesSetEffect(string                      name,
                                 BroochesSeries              series,
                                 BroochesRare                rare,
                                 IReadOnlyCollection<Effect> effects) : base(name)
        {
            Series  = series;
            Rare    = rare;
            Effects = effects;
        }

        BroochesSeries                              Series  { get; }
        BroochesRare                                Rare    { get; }
        public override IReadOnlyCollection<Effect> Effects { get; }
    }
}
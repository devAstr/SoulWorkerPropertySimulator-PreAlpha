﻿using System;
using System.Collections.Generic;
using SoulWorkerPropertySimulator.Models;
using SoulWorkerPropertySimulator.Types;

namespace SoulWorkerPropertySimulator.Data.Storage
{
    internal static partial class TagData
    {
        public static IReadOnlyCollection<Tag> Get(TagField field) =>
            field switch
            {
                TagField.Weapon => WeaponTag,
                TagField.Gear   => GearTag,
                _               => throw new ArgumentOutOfRangeException(nameof(field), field, null)
            };
    }
}
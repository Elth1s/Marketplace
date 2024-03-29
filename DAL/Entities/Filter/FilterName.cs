﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class FilterName : BaseEntity, IAggregateRoot
    {
        public int FilterGroupId { get; set; }
        public int? UnitId { get; set; }

        [ForeignKey(nameof(FilterGroupId))]
        public FilterGroup FilterGroup { get; set; }
        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; }

        public ICollection<FilterValue> FilterValues { get; set; }
        public ICollection<FilterNameTranslation> FilterNameTranslations { get; set; }
    }
}

﻿namespace MyResourcePlanning.Data.Models.BaseModels
{
    using System;

    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}

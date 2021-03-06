﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface ICsvService
    {
        Task WriteAsync<T, TClassMap>(IEnumerable<T> rows, string fileName, string container, CancellationToken cancellationToken)
            where TClassMap : ClassMap<T>;
    }
}

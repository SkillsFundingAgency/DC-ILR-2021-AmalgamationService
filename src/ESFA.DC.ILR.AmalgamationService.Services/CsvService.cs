﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class CsvService : ICsvService
    {
        private readonly UTF8Encoding _encoding = new UTF8Encoding(false, true);

        private readonly IFileService _fileService;

        public CsvService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task WriteAsync<T, TClassMap>(IEnumerable<T> rows, string fileName, string container, CancellationToken cancellationToken)
            where TClassMap : ClassMap<T>
        {
            using (Stream stream = await _fileService.OpenWriteStreamAsync(fileName, container, cancellationToken))
            {
                using (TextWriter textWriter = new StreamWriter(stream, _encoding))
                {
                    var configuration = new Configuration();

                    configuration.RegisterClassMap<TClassMap>();

                    using (CsvWriter csvWriter = new CsvWriter(textWriter, configuration))
                    {
                        csvWriter.WriteRecords(rows);
                    }
                }
            }
        }
    }
}
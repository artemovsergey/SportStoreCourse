using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using SportStore.API.Entities;
using SportStore.API.Interfaces;

namespace SportStore.Infrastructure.Services
{
    public class ExportToCsvService : ICsvExporter
    {
        public byte[] ExportUsersToCsv(List<User> users)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream)){
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture);
                csvWriter.WriteRecords(users);
            }

            return memoryStream.ToArray();
        }

    }
}
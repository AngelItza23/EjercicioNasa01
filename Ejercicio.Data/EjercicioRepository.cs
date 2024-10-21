using CsvHelper;
using CsvHelper.Configuration;
using Ejercicio.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Ejercicio.Data
{
    public class EjercicioRepository
    {
        public List<CSV> LeerMovimientosCsv(Stream archivoCsv)
        {
            using (var reader = new StreamReader(archivoCsv))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",", // Separador de columnas
                HasHeaderRecord = false // Indica que el archivo tiene encabezado
            }))
            {
                var movimientos = csv.GetRecords<CSV>().ToList();
                return movimientos;
            }
        }
    }
}

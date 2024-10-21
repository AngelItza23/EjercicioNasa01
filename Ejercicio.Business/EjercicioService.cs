using Ejercicio.Data;
using Ejercicio.Entities;
using System;
using System.IO;
using System.Linq;


namespace Ejercicio.Business
{
    public class EjercicioService
    {
        private readonly EjercicioRepository _ejercicioRepository;

        public EjercicioService()
        {
            _ejercicioRepository = new EjercicioRepository();
        }

        public (decimal total, decimal promedio, int cantidad, String msg) UploadMovimientos(Stream archivoCsv)
        {
            var movimientos = _ejercicioRepository.LeerMovimientosCsv(archivoCsv);
            bool Esfecha = movimientos.Any(x => EsFecha(x.Fecha) == false) ? false : true;
            bool esNumerica = movimientos.Any(x => Double.TryParse(x.Monto, out _) == false) ? false : true;
            bool DescriocionNotNull = movimientos.Any(x => x.Descripcion == null) ? false : true;

            decimal total;
            decimal promedio;
            int cantidad;
            String msg = "";
            if (Esfecha == false)
            {
                total = 0;
                promedio = 0;
                cantidad = 0;
                msg = "Revisar columna de fecha";
            }
            else if (esNumerica == false)
            {
                total = 0;
                promedio = 0;
                cantidad = 0;
                msg = "Revisar columna de Monto";
            }
            else if (DescriocionNotNull == false)
            {
                total = 0;
                promedio = 0;
                cantidad = 0;
                msg = "Revisar columna de descripcion nula";
            }
            else   {
                total = movimientos.Where(x => x.Monto != null && x.Descripcion != null && x.Fecha != null).Sum(x => Convert.ToDecimal(x.Monto));
                cantidad = movimientos.Count(x => x.Monto != null && x.Descripcion != null && x.Fecha != null);
                promedio = movimientos.Any() && cantidad > 0 ? total / cantidad : 0;
                
            }
            return (total, promedio, cantidad, msg);

            //decimal total = movimientos.Sum(m => m.Monto);
            //decimal promedio = movimientos.Any() ? movimientos.Average(m => m.Monto) : 0;
            //int cantidad = movimientos.Count;

            //return (total, promedio, cantidad);
        }
        public static Boolean EsFecha(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}

using SCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web;

namespace Helpers
{
    public class Helper
    {
        private BaseDatosSCAEntities db = new BaseDatosSCAEntities();
        public static string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }

        public static bool RegistrarMovimiento(string Movimiento, string Modulo, string ValorAntiguo, string ValorNuevo, int Id)
        {
            using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                BitacoraMovimiento Entidad = new BitacoraMovimiento();
                Entidad.FechaMovimiento = DateTime.Now;
                Entidad.IdUsuario = Id;
                Entidad.ModuloAfectado = Modulo;
                Entidad.TipoMovimiento = Movimiento;
                Entidad.ValorAntiguo = ValorAntiguo;
                Entidad.ValorNuevo = ValorNuevo;
                db.BitacoraMovimiento.Add(Entidad);
                int Resultado = db.SaveChanges();
                if (Resultado > 0)
                {
                    Ts.Complete();
                    return true;
                }else
                {
                    Ts.Dispose();
                    return false;
                }
            }
        }

    }
}
using SCA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web;

namespace Helpers
{
    public class Helper
    {
        
        public static string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }

        public static bool RegistrarMovimiento(string Movimiento, string Modulo, string ValorAntiguo, string ValorNuevo, int Id)
        {
            BaseDatosSCAEntities db = new BaseDatosSCAEntities();
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

        public static int RegistrarIngresoSalida(BitacoraIngresoSalida obj)//Viene de la vista obj
        {
            try
            {
                BaseDatosSCAEntities db = new BaseDatosSCAEntities();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    BitacoraIngresoSalida Objbd = new BitacoraIngresoSalida();//Viene de la base de datos
                    Objbd.IdUsuario = obj.IdUsuario;
                    Objbd.FechaIngreso = obj.FechaIngreso;
                    db.BitacoraIngresoSalida.Add(Objbd);
                    db.SaveChanges();//Commit
                    int Resultado = Objbd.Id;

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        return Resultado;
                    }
                    else
                    {
                        Ts.Dispose();
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int RegistroSalida(int IdRegistro)
        {
            try
            {
                BaseDatosSCAEntities db = new BaseDatosSCAEntities();
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    var Objbd = db.BitacoraIngresoSalida.Find(IdRegistro);
                    Objbd.FechaSalida = DateTime.Now;
                    db.Entry(Objbd).State = EntityState.Modified;
                    //Guarda los cambios en bd
                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        return Resultado;
                    }
                    else
                    {
                        Ts.Dispose();
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
using ERP.AccesoDatos;
using ERP.Entidades;
using ERP.Negocio;
using System;

namespace ERP.FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ERPContext();
            // AdicionarCia();
            //ActualizarCia();
            //AdicionarRegional(context);
            //AdicionarCentroOperacion(context);
            ModificarCentroOperacion(context);
            //Console.WriteLine(Environment.MachineName);
        }

        private static void ModificarCentroOperacion(ERPContext context)
        {
            var blCentroOperacion = new BLCentroOperacion(context);
            var centroOperacion = blCentroOperacion.Leer("001");
            centroOperacion.Contacto.Telefono = "998";
            blCentroOperacion.Actualizar(centroOperacion);
        }

        private static void AdicionarCentroOperacion(ERPContext context)
        {
            var blCompania = new BLCompania(context);
            var compania1 = blCompania.Leer(1);
            var blRegional = new BLRegional(context);
            var regional = blRegional.Leer("R1");


            var centroOperacion = new CentroOperacion()
            {
                Compania = compania1,
                Regional = regional,
                Id = "002",
                Descripcion = "Sucursal 2",
                Contacto = new Contacto()
                {
                    Nombre = "Jane doe",
                    Direccion = "Av9",
                    Telefono = "2345",
                    Compania = compania1
                }
            };

            var blCentroOperacion = new BLCentroOperacion(context);
            blCentroOperacion.Adicionar(centroOperacion);

            var contacto = centroOperacion.Contacto;
            contacto.Nombre = "Samantha Davis";
            blCentroOperacion.Actualizar(centroOperacion);

        }

        private static void AdicionarRegional(ERPContext context)
        {
            var blCompania = new BLCompania(context);
            var compania1 = blCompania.Leer(1);

            var regional = new Regional()
            {
                Compania = compania1,
                Descripcion = "12345678901234567890123456789012345678901234567890",
                Id = "R41"
            };

            var blRegional = new BLRegional(context);
            blRegional.Adicionar(regional);

        }

        //private static void ActualizarCia()
        //{
        //    var blCompania = new BLCompania();
        //    var compania1 = blCompania.Leer(1);
        //    var compania2 = blCompania.Leer(1);
        //    blCompania.Actualizar(compania1);
        //    blCompania.Actualizar(compania2);
        //}

        //private static void AdicionarCia()
        //{
        //    var compania = new Compania()
        //    {
        //        RazonSocial = "Siesa S.A."
        //    };

        //    var blCompania = new BLCompania();
        //    blCompania.Adicionar(compania);

        //}
    }
}

using ERP.AccesoDatos;
using ERP.Entidades;
using ERP.Negocio;
using Grpc.Net.Client;
using gRPCCentrosOperacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERP.FrontEnd
{
    class Program
    {

        private static readonly Random Random = new Random();

        static async Task Main(string[] args)
        {
            var context = new ERPContext();
            //AdicionarCia();
            //ActualizarCia(context);
            //AdicionarRegional(context);
            //AdicionarCentroOperacion(context);
            //ModificarCentroOperacion(context);
            //Console.WriteLine(Environment.MachineName);
            //LeerCO();
            //LeerCOs();
            await ClientStreamingCallExample();
        }

        private static void LeerCO()
        {
            CentroOperacionServ.CentroOperacionServClient cliente = CrearClientegRPC();

            var leerRequest = new LeerRequest();
            leerRequest.IdCo = "001";
            var resultado = cliente.Leer(leerRequest);
            var Mensaje = resultado.CO;

            Console.WriteLine("Compañia: {0} Id: {1} Descipción: {2} Regional: {3}", Mensaje.Compania, Mensaje.Id, Mensaje.Descripcion, Mensaje.RegionalId);
            Console.WriteLine("Cont. nombre: {0} Cont. Direccion: {1} Cont. Tel: {2} ", Mensaje.Contacto.Nombre, Mensaje.Contacto.Direccion, Mensaje.Contacto.Telefono);
            Console.ReadKey();
        }

        private static CentroOperacionServ.CentroOperacionServClient CrearClientegRPC()
        {
            var url = "https://localhost:5001";
            var canal = GrpcChannel.ForAddress(url);
            var cliente = new CentroOperacionServ.CentroOperacionServClient(canal);
            return cliente;
        }

        private static void LeerCOs()
        {
            CentroOperacionServ.CentroOperacionServClient cliente = CrearClientegRPC();

            var leerTodosRequest = new LeerTodosRequest();
            var resultado = cliente.LeerTodos(leerTodosRequest);
            var listaCO = ConvertirRepliesEnCentrosOperacion(resultado.COs);


            foreach (var centroOperacion in listaCO)
            {
                Console.WriteLine("Compañia: {0} Id: {1} Descipción: {2} Regional: {3}", centroOperacion.Compania, centroOperacion.Id, centroOperacion.Descripcion, centroOperacion.RegionalId);
                Console.WriteLine("Cont. nombre: {0} Cont. Direccion: {1} Cont. Tel: {2} ", centroOperacion.Contacto.Nombre, centroOperacion.Contacto.Direccion, centroOperacion.Contacto.Telefono);
            }
            
            Console.ReadKey();
        }

        private static IEnumerable<CentroOperacion> ConvertirRepliesEnCentrosOperacion(IEnumerable<COMessage> centrosOperacionReply)
        {


            var COReplies = (from centroOperacion in centrosOperacionReply
                             select new CentroOperacion
                             {
                                 CompaniaId = centroOperacion.Compania,
                                 Id = centroOperacion.Id,
                                 RegionalId = centroOperacion.RegionalId,
                                 Descripcion = centroOperacion.Descripcion,
                                 IndEstado = (CentroOperacion.Estado)centroOperacion.IndEstado,
                                 ContactoRowid = centroOperacion.ContactoRowid,
                                 Contacto = new Contacto
                                 {
                                     CompaniaId = centroOperacion.Contacto.Compania,
                                     Nombre = centroOperacion.Contacto.Nombre,
                                     Direccion = centroOperacion.Contacto.Direccion,
                                     Telefono = centroOperacion.Contacto.Telefono
                                 }
                             });

            return COReplies;
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

            var url = "https://localhost:5001";
            var canal = GrpcChannel.ForAddress(url);
            var cliente = new RegionalServ.RegionalServClient(canal);

            var regional = new Regional()
            {
                Compania = compania1,
                Descripcion = "Regional 3",
                Id = "R3"
            };

            string jsonString = JsonSerializer.Serialize(regional);

            var adicRequest = new AdicRequest();
            adicRequest.JsRegional = jsonString;
            var resultado = cliente.Adicionar(adicRequest);
            var Mensaje = resultado.Message;

            Console.WriteLine(Mensaje);

            //var blRegional = new BLRegional(context);
            //blRegional.Adicionar(regional);

        }

        //private static void ActualizarCia()
        //{
        //    var blCompania = new BLCompania();
        //    var compania1 = blCompania.Leer(1);
        //    var compania2 = blCompania.Leer(1);
        //    blCompania.Actualizar(compania1);
        //    blCompania.Actualizar(compania2);
        //}

        private static void AdicionarCia()
        {
            var compania = new Compania()
            {
                RazonSocial = "Siesa S.A."
            };

            var companiaDesSer = new Compania();


            string jsonString = JsonSerializer.Serialize(compania);  
            Console.WriteLine(jsonString);

            Compania CiaDesSer = JsonSerializer.Deserialize<Compania>(jsonString);
            //    var blCompania = new BLCompania();
            //    blCompania.Adicionar(compania);

        }


        private static async Task ClientStreamingCallExample()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new CentroOperacionServ.CentroOperacionServClient(channel);
            using var call = client.AccumulateCount();
            for (var i = 0; i < 100; i++)
            {
                var count = Random.Next(5);
                Console.WriteLine($"{DateTime.Now} Accumulating with {count}");
                await call.RequestStream.WriteAsync(new CounterRequest { Count = count });
                //await Task.Delay(5000);
            }

            await call.RequestStream.CompleteAsync();

            var response = await call;
            Console.WriteLine($"Count: {response.Count}");
        }
    }
}

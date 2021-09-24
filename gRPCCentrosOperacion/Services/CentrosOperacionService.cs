using ERP.AccesoDatos;
using ERP.Entidades;
using ERP.Negocio;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace gRPCCentrosOperacion
{
    public class CentrosOperacionService : CentroOperacionServ.CentroOperacionServBase
    {
        private readonly ILogger<CentrosOperacionService> _logger;
        public CentrosOperacionService(ILogger<CentrosOperacionService> logger) => _logger = logger;

        public override Task<COReply> Leer(GetCORequest request, ServerCallContext context)
        {
            var dbContext = new ERPContext();

            BLCentroOperacion bLCentroOperacion = new BLCentroOperacion(dbContext);
            CentroOperacion centroOperacion = new CentroOperacion();

            try
            {
                centroOperacion = bLCentroOperacion.Leer(request.IdCo);            
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al adicionar regional" + e.Message);
            }

            var respuesta = new COReply
            {
                Compania = centroOperacion.CompaniaId,
                Id = centroOperacion.Id,
                RegionalId = centroOperacion.RegionalId,
                Descripcion = centroOperacion.Descripcion,
                IndEstado = (COReply.Types.EstadoCO)centroOperacion.IndEstado,
                ContactoRowid = centroOperacion.ContactoRowid,
                Contacto = new ContactoCO
                {
                    Compania = centroOperacion.Contacto.CompaniaId,
                    Nombre = centroOperacion.Contacto.Nombre,
                    Direccion = centroOperacion.Contacto.Telefono,
                    Telefono = centroOperacion.Contacto.Telefono
                }
            };

            return Task.FromResult(respuesta);
        }

        public override Task<COReplyLeerTodos> LeerTodos(GetCORequestLeerTodos request, ServerCallContext context)
        {
            var dbContext = new ERPContext();

            BLCentroOperacion bLCentroOperacion = new BLCentroOperacion(dbContext);
            CentroOperacion centroOperacion = new CentroOperacion();

            try
            {
                var centrosOperacion = bLCentroOperacion.LeerTodos();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al adicionar regional" + e.Message);
            }

            var respuesta = new COReply
            {
                Compania = centroOperacion.CompaniaId,
                Id = centroOperacion.Id,
                RegionalId = centroOperacion.RegionalId,
                Descripcion = centroOperacion.Descripcion,
                IndEstado = (COReply.Types.EstadoCO)centroOperacion.IndEstado,
                ContactoRowid = centroOperacion.ContactoRowid,
                Contacto = new ContactoCO
                {
                    Compania = centroOperacion.Contacto.CompaniaId,
                    Nombre = centroOperacion.Contacto.Nombre,
                    Direccion = centroOperacion.Contacto.Telefono,
                    Telefono = centroOperacion.Contacto.Telefono
                }
            };

            //return Task.FromResult(respuesta);
            return null;
        }
    }
}
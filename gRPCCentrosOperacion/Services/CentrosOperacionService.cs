using ERP.AccesoDatos;
using ERP.Entidades;
using ERP.Negocio;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCCentrosOperacion
{
    public class CentrosOperacionService : CentroOperacionServ.CentroOperacionServBase
    {
        private readonly ILogger<CentrosOperacionService> _logger;
        private readonly ERPContext dbContext;
        private readonly BLCentroOperacion bLCentroOperacion;
        public CentrosOperacionService(ILogger<CentrosOperacionService> logger)
        {
            _logger = logger;
            dbContext = new ERPContext();
            bLCentroOperacion = new BLCentroOperacion(dbContext);
        }

        public override Task<LeerResponse> Leer(LeerRequest request, ServerCallContext context)
        {
            var respuesta = new LeerResponse();
            try
            {
                var centroOperacion = bLCentroOperacion.Leer(request.IdCo);
                respuesta.CO = COConverter.ConvertirCOEnCOMessage(centroOperacion);
                respuesta.Mensaje = "C.O. Encontrado";
            }
            catch (Exception e)
            {
                respuesta.Mensaje = "Error al leer C.O.:" + e.Message;
            }

            return Task.FromResult(respuesta);
        }

        public override Task<LeerTodosResponse> LeerTodos(LeerTodosRequest request, ServerCallContext context)
        {
            var respuesta = new LeerTodosResponse();
            try
            {
                var centrosOperacion = bLCentroOperacion.LeerTodos();
                respuesta.COs.AddRange(COConverter.ConvertirMultiplesCOEnCOMessage(centrosOperacion));    
            }
            catch (Exception e)
            {
                respuesta.Mensaje = "Error al leer C.O.:" + e.Message;
            }

            return Task.FromResult(respuesta);
        }

       public override async Task<CounterReply> AccumulateCount(IAsyncStreamReader<CounterRequest> requestStream, ServerCallContext context)
        {
            var _counter = new IncrementingCounter();

            await foreach (var message in requestStream.ReadAllAsync())
            {

                _logger.LogInformation($"Incrementing count by {message.Count}");
                Console.WriteLine(DateTime.Now + " Message count servidor:" + message.Count);
                _counter.Increment(message.Count);
                await Task.Delay(200);
            }


            // Proceso con todos

            return new CounterReply { Count = _counter.Count };
        }

        public override Task<SalvarResponse> Salvar(SalvarRequest request, ServerCallContext context)
        {
            var salvarResponse = new SalvarResponse();
           

            try
            {
                CentroOperacion centroOperacion = COConverter.ConvertirCOMessageEnCO(request.CO);

                if (request.Adicionar)
                {
                    bLCentroOperacion.Adicionar(centroOperacion);
                }
                else
                {
                    bLCentroOperacion.Actualizar(centroOperacion);
                }
                salvarResponse.Mensaje = "Guardado Exitosamente";
            }
            catch (Exception e)
            {
                salvarResponse.Mensaje = "Error al guardar C.O." + e.Message;
            }

            return Task.FromResult(salvarResponse);
        }
    }
}
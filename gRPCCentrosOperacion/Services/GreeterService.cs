using ERP.Negocio;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ERP.Negocio;
using ERP.Entidades;
using ERP.AccesoDatos;
using System;
using System.Text.Json;

namespace gRPCCentrosOperacion
{
    public class GreeterService : RegionalServ.RegionalServBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<AdicReply> Adicionar(AdicRequest request, ServerCallContext context)
        {
            string resultado;
            var dbContext = new ERPContext();
            
            BLRegional bLRegional = new BLRegional(dbContext);
            Regional regional = JsonSerializer.Deserialize<Regional>(request.JsRegional);

            dbContext.Entry(regional.Compania).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                bLRegional.Adicionar(regional);
                resultado = "Regional adiconada";
            }
            catch (Exception e)
            {
                resultado = "Error al adicionar regional" + e.Message;
            }            

            return Task.FromResult(new AdicReply
            {
                Message = resultado
            }) ;
        }
    }
}

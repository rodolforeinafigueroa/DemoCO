using ERP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCCentrosOperacion
{
    public static class COConverter
    {
        public static COMessage ConvertirCOEnCOMessage(CentroOperacion centroOperacion)
        {
            return new COMessage
            {
                Compania = centroOperacion.CompaniaId,
                Id = centroOperacion.Id,
                RegionalId = centroOperacion.RegionalId,
                Descripcion = centroOperacion.Descripcion,
                IndEstado = (COMessage.Types.EstadoCO)centroOperacion.IndEstado,
                ContactoRowid = centroOperacion.ContactoRowid,
                RowVersion = Google.Protobuf.ByteString.CopyFrom(centroOperacion.RowVersion),
                Contacto = new ContactoCO
                {
                    Compania = centroOperacion.Contacto.CompaniaId,
                    Nombre = centroOperacion.Contacto.Nombre,
                    Direccion = centroOperacion.Contacto.Direccion,
                    Telefono = centroOperacion.Contacto.Telefono
                }
            };
        }

        public static CentroOperacion ConvertirCOMessageEnCO(COMessage centroOperacion)
        {
            return new CentroOperacion
            {
                CompaniaId = centroOperacion.Compania,
                Id = centroOperacion.Id,
                RegionalId = centroOperacion.RegionalId,
                Descripcion = centroOperacion.Descripcion,
                IndEstado = (CentroOperacion.Estado)centroOperacion.IndEstado,
                ContactoRowid = centroOperacion.ContactoRowid,
                RowVersion = centroOperacion.RowVersion.ToByteArray(),
                Contacto = new Contacto
                {
                    CompaniaId = centroOperacion.Contacto.Compania,
                    Nombre = centroOperacion.Contacto.Nombre,
                    Direccion = centroOperacion.Contacto.Direccion,
                    Telefono = centroOperacion.Contacto.Telefono
                }
            };
        }

        public static List<COMessage> ConvertirMultiplesCOEnCOMessage(IEnumerable<CentroOperacion> centrosOperacion)
        {
            var listaCOMessage = new List<COMessage>();
            foreach(var centroOperacion in centrosOperacion)
            {
                var coMessage = ConvertirCOEnCOMessage(centroOperacion);
                listaCOMessage.Add(coMessage);
            }

            return listaCOMessage;
        }


    }
}

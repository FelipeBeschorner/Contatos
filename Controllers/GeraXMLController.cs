using contato;
using Contato.Business;
using Contato.Config;
using ContatosApplication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Contato.Models
{
    [Route("API/Contato/[controlLer]")]

    public class GeraXMLController : ControllerBase
    {
        ContatoBusiness ContatoBusiness = new ContatoBusiness();

        [HttpGet]
        public IActionResult getXml()
        {
            return Ok(ContatoBusiness.getXml());
        }

        [Route("Download")]
        [HttpGet]
        public IActionResult Download(string tipo)
        {
            switch (tipo)
            {
                case "xml": return ContatoBusiness.downloadXml();
                case "csv": return ContatoBusiness.downloadCsv(); ;
                default : throw new InternalException(System.Net.HttpStatusCode.BadRequest, "Tipo inválido", "Esse tipo não está cadastrado");
            }
        }
    }
}

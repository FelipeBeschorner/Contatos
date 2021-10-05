using contato;
using Contato.Business;
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

        [HttpGet]
        [Route("Download")]
        public IActionResult downloadXml()
        {
            return ContatoBusiness.downloadXml();
        }
    }
}

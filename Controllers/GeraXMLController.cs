using contato;
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
        AddressBook AddressBook = Program.AddressBook;

        [HttpGet]
        public IActionResult getXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AddressBook));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            FileStream fileStream = new FileStream("teste.xml", FileMode.Create);
            serializer.Serialize( fileStream, AddressBook, ns);
            fileStream.Close();

            return Ok("XML gerado com Sucesso!");
        }

        [HttpGet]
        [Route("Download")]
        public IActionResult downloadXml()
        {
            getXml();
            FileStream arquivo = new FileStream("teste.xml", FileMode.Open);
            FileStreamResult download = new FileStreamResult(arquivo, "application/xml");
            download.FileDownloadName = "teste.xml";
            return download;
        }
    }
}

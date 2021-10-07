using Contato.Business;
using Contato.Config;
using ContatosApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace contato.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class ContatoController : ControllerBase
    {
        ContatoBusiness ContatoBusiness = new ContatoBusiness();

        [HttpGet]
        public IActionResult GetContatos()
        {
            return Ok(ContatoBusiness.GetContatos());
        }

        [HttpPost]
        public IActionResult criarContato(string nome, int numero) {
            return Ok(ContatoBusiness.criarContato(nome, numero));
        }

        [HttpGet]
        [Route("{numero}")]
        public IActionResult getContato(int numero)
        {
            return Ok(ContatoBusiness.getContato(numero));
        }

        [HttpPut]
        public IActionResult editarContato(int numero, string novoNome)
        {
            return Ok(ContatoBusiness.editarContato(numero, novoNome));
        }

        [HttpDelete]
        public IActionResult deletarContato(int numero)
        {
            return Ok(ContatoBusiness.deletarContato(numero));
        }
    }
}

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
            try {
                return Ok(ContatoBusiness.criarContato(nome, numero));
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu algum erro duarante a Execução.");
            }
        }

        [HttpGet]
        [Route("{numero}")]
        public IActionResult getContato(int numero)
        {
            try {
                return Ok(ContatoBusiness.getContato(numero));
            }
            catch (InternalException e)
            {
                //return BadRequest("Ocorreu algum erro durante a Execução. asdasd");
                switch (e.HttpStatus)
                {
                    case System.Net.HttpStatusCode.NotFound : return NotFound(e);
                        break;
                    default : return BadRequest("Ocorreu algum erro duarante a Execução.");
                }
                //return e.HttpStatus(e.Message) //BadRequest(e);
            }
        }

        [HttpPut]
        public IActionResult editarContato(int numero, string novoNome)
        {
            try
            {
                return Ok(ContatoBusiness.editarContato(numero, novoNome));
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu algum erro durante a Execução.");
            }
        }

        [HttpDelete]
        public IActionResult deletarContato(int numero)
        {
            try {
                return Ok(ContatoBusiness.deletarContato(numero));
            }
            catch(Exception e)
            {
                return BadRequest("Ocorreu algum erro durante a Execução.");
            }
        }
    }
}

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
        AddressBook AddressBook = Program.AddressBook;

        [HttpGet]
        public IActionResult GetContatos()
        {
            return Ok(AddressBook);
        }

        [HttpPost]
        public IActionResult criarContato(string nome, int numero) {
            try {
                if (string.IsNullOrEmpty(nome) || nome.Length < 3) return BadRequest("Informe um nome válido");
                Contact contatoAtivo = AddressBook.contacts.Find(r => r.Phone.phonenumber == numero.ToString());
                if (contatoAtivo != null) return BadRequest("Número já cadastrado.");
                else return Ok(AddressBook.contacts.Append(new Contact(nome, numero.ToString())));
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
                Contact contatoAtivo = AddressBook.contacts.Find(r => r.Phone.phonenumber == numero.ToString());
                if (contatoAtivo == null) return NotFound("Número não cadastrado.");
                else return Ok(contatoAtivo);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu algum erro durante a Execução.");
            }
        }

        [HttpPut]
        public IActionResult editarContato(int numero, string novoNome)
        {
            try
            {
                if (string.IsNullOrEmpty(novoNome) || novoNome.Length < 3) return BadRequest("Informe um nome válido");
                if (AddressBook.contacts.Exists(r => r.Phone.phonenumber == numero.ToString())) return Ok(AddressBook.contacts.Find(r => r.Phone.phonenumber == numero.ToString()).FirstName = novoNome);
                else return NotFound("Número não cadastrado.");
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
                if (AddressBook.contacts.Remove(AddressBook.contacts.Find(r => r.Phone.phonenumber == numero.ToString()))) return Ok("Numero excluido!");
                else return NotFound("Número não cadastrado");
            }
            catch(Exception e)
            {
                return BadRequest("Ocorreu algum erro durante a Execução.");
            }
        }
    }
}

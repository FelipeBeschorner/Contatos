using contato;
using Contato.Config;
using ContatosApplication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Contato.Business
{
    public class ContatoBusiness
    {
            AddressBook AddressBook = Program.AddressBook;

            public AddressBook GetContatos() { return AddressBook; }

            public Contact criarContato( string nome, int numero ) {
                try {
                    if ( nome.Length < 3 ) throw new InternalException( System.Net.HttpStatusCode.BadRequest, "Nome inválido", "O nome não atende os requisitos básicos" );
                    Contact contatoAtivo = AddressBook.contacts.Find( r => r.Phone.phonenumber == numero.ToString() );
                    if ( contatoAtivo != null ) throw new InternalException( System.Net.HttpStatusCode.Conflict,"Número já cadastrado.", $"Este número já está em uso por {contatoAtivo.FirstName}." );
                    else return (Contact)AddressBook.contacts.Append(new Contact( nome, numero.ToString()) );
                }
                catch (Exception e) { throw e; }
            }

            public Contact getContato(int numero) {
                try {
                    Contact contatoAtivo = AddressBook.contacts.Find( r => r.Phone.phonenumber == numero.ToString() );
                    if ( contatoAtivo == null ) throw new InternalException( System.Net.HttpStatusCode.NotFound, "Número não cadastrado.", "Esse número não está cadastrado." );
                    else return contatoAtivo;
                }
                catch (InternalException e) { throw e; }
            }

            public string editarContato(int numero, string novoNome) {
                try {
                    if ( novoNome.Length < 3 ) throw new InternalException(System.Net.HttpStatusCode.BadRequest, "Nome inválido", "O nome não atende os requisitos básicos" );
                    if ( AddressBook.contacts.Exists( r => r.Phone.phonenumber == numero.ToString()) ) { AddressBook.contacts.Find(r => r.Phone.phonenumber == numero.ToString()).FirstName = novoNome; return "Nome alterado com Sucesso!"; }
                    else throw new InternalException( System.Net.HttpStatusCode.NotFound, "Número não cadastrado.", "Esse número não está cadastrado." );
                }
                catch (Exception e) { throw e; }
            }

            public string deletarContato(int numero) {
                try {
                    if ( AddressBook.contacts.Remove( AddressBook.contacts.Find( r => r.Phone.phonenumber == numero.ToString()) ) ) return "Numero excluido!";
                    else throw new InternalException( System.Net.HttpStatusCode.NotFound, "Número não cadastrado.", "Esse número não está cadastrado." );
                }
                catch (Exception e) { throw e; }
            }

        public string getXml() {
            XmlSerializer serializer = new XmlSerializer( typeof(AddressBook) );
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            FileStream fileStream = new FileStream( "ArquivoXml.xml", FileMode.Create );
            serializer.Serialize( fileStream, AddressBook, ns );
            fileStream.Close();
            return "XML gerado com Sucesso!";
        }

        public IActionResult downloadXml() {
            getXml();
            FileStream arquivo = new FileStream( "ArquivoXml.xml", FileMode.Open );
            FileStreamResult download = new FileStreamResult( arquivo, "application/xml" );
            download.FileDownloadName = "ArquivoXml.xml";
            return download;
        }

        public IActionResult downloadCsv()
        {
            using StreamWriter file = new StreamWriter("ArquivoCsv.csv");
            file.WriteLine("Name,Number");
            AddressBook.contacts.ForEach(r => file.WriteLine($"{r.FirstName},{r.Phone.phonenumber}"));
            file.Close();
            FileStream arquivo = new FileStream("ArquivoCsv.csv", FileMode.Open);
            FileStreamResult download = new FileStreamResult(arquivo, "text/csv");
            download.FileDownloadName = "ArquivoCsv.csv";
            return download;
        }
    }
}

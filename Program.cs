using ContatosApplication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace contato
{
    public class Program
    {
        public static AddressBook AddressBook;
        public static void Main(string[] args)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AddressBook));
            var FileStream = new FileStream("phonebook.xml", FileMode.Open);
            AddressBook = (AddressBook)serializer.Deserialize(FileStream);
            AddressBook.contacts.ForEach(r => { if (r.FirstName == "") { r.FirstName = r.LastName; r.LastName = null; } });
            FileStream.Close();

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

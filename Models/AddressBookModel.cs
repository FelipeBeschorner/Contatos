using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ContatosApplication
{

    [XmlType("AddressBook")]
    public class AddressBook
    {
        [XmlElement("Contact")]
        public List<Contact> contacts { get; set; }
    }
    public class Contact
    {
        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        public string LastName { get; set; }

        [XmlElement("Phone")]
        public Phone Phone { get; set; }

        public Contact() {}
        public Contact(string nome, string numero)
        {
            this.FirstName = nome;
            this.Phone = new Phone(numero);
        }
    }
    public class Phone
    {
        [XmlAttribute("type")]
        public string type { get; set;}

        [XmlElement("phonenumber")]
        public string phonenumber { get; set; }

        public Phone() { }
        public Phone(string numero)
        {
            this.type = "Work";
            this.phonenumber = numero;
        }
    }
}

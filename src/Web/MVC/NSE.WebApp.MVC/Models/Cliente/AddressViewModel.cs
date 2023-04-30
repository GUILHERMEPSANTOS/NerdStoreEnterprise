using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models.Cliente
{
    public class AddressViewModel
    {
        [Required]
        [DisplayName("Rua")]
        public string StreetAddress { get; set; }
        [Required]
        [DisplayName("Número")]
        public string BuildingNumber { get; set; }
        
        public string SecondaryAddress { get; set; }
        [Required]
        public string Neighborhood { get; set; }
        [Required]
        [DisplayName("CEP")]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        public override string ToString()
        {
            return $"{StreetAddress}, {BuildingNumber} {SecondaryAddress} - {Neighborhood} - {City} - {State}";
        }
    }
}
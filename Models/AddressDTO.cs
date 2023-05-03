using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class AddressDTO
    {
        #region Propriedades
        public int Id { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        [JsonProperty("gia")]
        public int Number { get; set; }
        [JsonProperty("complemento")]
        public string Description { get; set; }
        [JsonProperty("localidade")]
        public string City { get; set; }
        #endregion
    }
}

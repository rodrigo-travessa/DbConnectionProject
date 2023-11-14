using DbConnectionProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnectionProject.Models.ViewModels
{
    public class CarroCreateViewModel
    {

        public List<Pessoa> Pessoas { get; set;} = new List<Pessoa>();
        public Carro Carro { get; set; } = new Carro();
    }
}

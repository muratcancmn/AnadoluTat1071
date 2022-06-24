using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makarnacı
{
   abstract class Fiyat
    {
       public string ExtraMalzeme { get; set; }
      
       public abstract void FiyatBul();

    }
}

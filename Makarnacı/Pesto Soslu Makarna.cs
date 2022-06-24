using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makarnacı
{
    class Pesto_Soslu_Makarna : Makarna
    {
        public override double FiyatBulma(double makarnaFiyat, double extraMalzeme)
        {
            Fiyat = makarnaFiyat + extraMalzeme;
            return Fiyat;
        }
    }
}

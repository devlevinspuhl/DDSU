using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDSUFormTCP
{
    public class MySettings
    {
        public decimal ImportPrice { get; set; }
        public decimal ExportPrice { get; set; }

        public decimal CreditDebitCalc(decimal import, decimal export)
        {
            var importValue = import*ImportPrice;
            var exportValue = export*ExportPrice;
            var creditDebit = exportValue-importValue;
            return creditDebit/ImportPrice;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadUbl.Models
{
    public class Despatch
    {
        public string ActualDespatchDate { get; set; }
        public string ActualDespatchTime { get; set; }

        public DateTime DespatchDate
        {
            get
            {
                if (!string.IsNullOrEmpty(ActualDespatchDate) && !string.IsNullOrEmpty(ActualDespatchTime))
                    return DateTime.ParseExact($"{ActualDespatchDate} {ActualDespatchTime}", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                else if (!string.IsNullOrEmpty(ActualDespatchDate))
                    return DateTime.ParseExact(ActualDespatchDate, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
                return DateTime.MinValue;
            }
        }
    }
}

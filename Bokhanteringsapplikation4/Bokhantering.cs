using System;
using System.Collections.Generic;
using System.Linq;

namespace GymApp
{
    public class Bokhantering
    {
        private List<Pass> passLista;

        public Bokhantering()
        {
            passLista = new List<Pass>();
        }

        public List<Pass> HämtaPass()
        {
            return passLista;
        }

        public void LäggTillPass(Pass nyttPass)
        {
            if (!passLista.Any(p => p.Type == nyttPass.Type && p.Date == nyttPass.Date && p.Time == nyttPass.Time))
            {
                passLista.Add(nyttPass);
            }
        }

        public void TaBortPass(Pass passAttTaBort)
        {
            var pass = passLista.FirstOrDefault(p =>
                p.Type == passAttTaBort.Type &&
                p.Date == passAttTaBort.Date &&
                p.Time == passAttTaBort.Time);

            if (pass != null)
            {
                passLista.Remove(pass);
            }
        }

        public List<Pass> FiltreraPass(string typ, string datum, string tid)
        {
            return passLista.Where(p =>
                (string.IsNullOrEmpty(typ) || p.Type.IndexOf(typ, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (string.IsNullOrEmpty(datum) || p.Date.ToString("yyyy-MM-dd").Contains(datum)) &&
                (string.IsNullOrEmpty(tid) || p.Time.Contains(tid))
            ).ToList();
        }
    }
}

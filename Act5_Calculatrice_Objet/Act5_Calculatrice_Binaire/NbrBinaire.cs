using System;
using System.Collections.Generic;
using System.Text;

namespace Act5_Calculatrice_Binaire
{
    class NbrBinaire
    {
        private ushort[] _nombre;

        public ushort[] Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public NbrBinaire(string nombre)
        {
            _nombre = RemplirTableau(nombre);
        }

        public NbrBinaire()
        {
            _nombre = new ushort[8];
        }

        public ushort[] RemplirTableau(string _nombre)
        {
            ushort[] tabBin = new ushort[8];
            for (int i = 0; i <= 7; i++)
            {
                tabBin[i] = 0;
            }
            for (int i = 0; i <= _nombre.Length - 1; i++)
            {
                tabBin[7 - i] = ushort.Parse(_nombre[_nombre.Length - 1 - i].ToString());
            }
            return tabBin;
        }
        public void Additionne(NbrBinaire t2, out NbrBinaire tRes, out bool ok)
        {
            ok = true;
            ushort report = 0;
            tRes = new NbrBinaire();
            for (int i = 7; i >= 0; i--)
            {
                ushort res = (ushort)(Nombre[i] + t2.Nombre[i] + report);
                if ((int)(res / 2) == 0)
                {
                    report = 0;
                }
                else
                {
                    report = 1;
                }

                if (res == 1)
                {
                    tRes.Nombre[i] = 1;
                }
                else
                {
                    if (res % 2 == 1)
                    {
                        tRes.Nombre[i] = 1;
                    }
                    else
                    {
                        tRes.Nombre[i] = 0;
                    }
                }
            }
            if (report == 1)
            {
                ok = false;
            }
        }

        public bool Soustrait(NbrBinaire t2, out NbrBinaire tRes)
        {
            int[] tTemp = new int[8];
            bool ok = true;
            tRes = new NbrBinaire();

            for (int i = 0; i < 8; i++)
            {
                tTemp[i] = Nombre[i] - t2.Nombre[i];
            }
            for (int i = 7; i >= 1; i--)
            {
                if (tTemp[i] == -1)
                {
                    t2.Nombre[i - 1] = (ushort)(t2.Nombre[i - 1] + 1);
                    Nombre[i] = (ushort)(Nombre[i] + 2);
                }

                if (Nombre[i] < t2.Nombre[i])
                {
                    t2.Nombre[i - 1] = (ushort)(t2.Nombre[i - 1] + 1);
                    Nombre[i] = (ushort)(Nombre[i] + 2);
                }
                tRes.Nombre[i] = (ushort)(Nombre[i] - t2.Nombre[i]);
            }
            if (Nombre[0] >= t2.Nombre[0])
            {
                tRes.Nombre[0] = (ushort)(Nombre[0] - t2.Nombre[0]);
            }
            else
            {
                ok = false;
            }
            return ok;
        }

        public string AffichageResultat()
        {
            string Reponse = "Le résultat est ";
            for (int i = 0; i < Nombre.Length; i++)
            {
                Reponse = Reponse + Nombre[i];
            }
            return Reponse;
        }
    }
}

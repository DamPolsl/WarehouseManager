using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAppManager.DAL.Encje
{
    class Operacja
    {
        //(id_operacji, typ_operacji, data, id_pracownika)
        //(id_operacji, numer_pozycji, typ_operacji, ilość, id_produktu, id_magazynu, regał)
        public int? Id { get; set; }
        public int NumerPozycji { get; set; }
        public string TypOperacji { get; set; }
        public int Ilosc { get; set; }
        public int? IdPracownika { get; set; }
        public int? IdProduktu { get; set; }
        public int? IdMagazynu { get; set; }
        public string Regal { get; set; }
        public Operacja(int id_operacji, Towar towar, int id_pracownika, string typ_operacji, int numer_pozycji=1)
        {
            Id = id_operacji;
            NumerPozycji = numer_pozycji;
            TypOperacji = typ_operacji;
            if(typ_operacji == "dodanie")
                Ilosc = towar.DodanaIlosc;
            if (typ_operacji == "pobranie")
                Ilosc = towar.IloscDoKoszyka;
            IdPracownika = id_pracownika;
            IdProduktu = towar.Id;
            IdMagazynu = towar.IdMagazynu;
            Regal = towar.Regal;
        }
        public string ToInsert()
        {
            //(id_operacji, typ_operacji, data, id_pracownika)
            return $"({Id}, '{TypOperacji}', now(), {IdPracownika})";
        }
        public string TowarToInsert()
        {
            //(id_operacji, numer_pozycji, ilość, id_produktu, id_magazynu, regał)
            return $"({Id}, {NumerPozycji}, {Ilosc}, {IdProduktu}, {IdMagazynu}, '{Regal}')";
        }
    }
}

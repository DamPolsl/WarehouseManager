using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAppManager.Model
{
    using DAL.Encje;
    using DAL.Repozytoria;
    using System.Collections.ObjectModel;
    using System.Windows;

    class Model
    {
        public ObservableCollection<Towar> Towary { get; set; } = new ObservableCollection<Towar>();
        public ObservableCollection<Magazyn> Magazyny { get; set; } = new ObservableCollection<Magazyn>();
        public ObservableCollection<Produkt> Produkty { get; set; } = new ObservableCollection<Produkt>();
        public ObservableCollection<Towar> Koszyk { get; set; } = new ObservableCollection<Towar>();

        public Model()
        {
            var towary = RepozytoriumTowar.GetTowary();
            foreach (var t in towary)
            {
                Towary.Add(t);
            }
            var magazyny = RepozytoriumMagazyn.GetMagazyny();
            foreach (var m in magazyny)
            {
                Magazyny.Add(m);
            }
            var produkty = RepozytoriumProdukt.GetProdukty();
            foreach(var p in produkty)
            {
                Produkty.Add(p);
            }
        }
        
        public ObservableCollection<Towar> GetTowarMagazynu(int? id_magazynu)
        {
            ObservableCollection<Towar> towary = new ObservableCollection<Towar>();
            foreach (var t in Towary)
            {
                if (id_magazynu == t.IdMagazynu)
                {
                    towary.Add(t);
                }
            }
            return towary;
        }

        public ObservableCollection<Towar> GetTowarKoszyka(int? id_magazynu)
        {
            ObservableCollection<Towar> towary = new ObservableCollection<Towar>();
            foreach (var t in Koszyk)
            {
                if (id_magazynu == t.IdMagazynu)
                {
                    towary.Add(t);
                }
            }
            return towary;
        }

        public bool AktualizujTowar(Towar towar, int id_pracownika)
        {
            if (RepozytoriumTowar.EdytujTowar(towar))
            {
                int nowe_id_operacji = RepozytoriumOperacja.GetNextId();
                Operacja operacja = new Operacja(nowe_id_operacji, towar, id_pracownika, "dodanie");
                RepozytoriumOperacja.NowaOperacja(operacja);
                RepozytoriumOperacja.NowyTowarOperacji(operacja);
                for (int i = 0; i < Towary.Count; i++)
                {
                    if (Towary[i].Id == towar.Id && Towary[i].Regal == towar.Regal && Towary[i].IdMagazynu == towar.IdMagazynu)
                    {
                        Towary[i] = new Towar(towar);
                    }
                }
                return true;
            }
            return false;
        }
        public bool DodajTowar(Towar towar, bool czyIstniejacyProdukt, int id_pracownika)
        {
            if (!czyIstniejacyProdukt)
            {
                Produkt produkt = new Produkt(towar.Nazwa);
                if (RepozytoriumProdukt.DodajProdukt(produkt))
                {
                    Produkty.Add(produkt);
                    towar.Id = Produkty[Produkty.Count - 1].Id;
                }
            }
            if (RepozytoriumTowar.AddTowar(towar))
            {
                int nowe_id_operacji = RepozytoriumOperacja.GetNextId();
                Operacja operacja = new Operacja(nowe_id_operacji, towar, id_pracownika, "dodanie");
                RepozytoriumOperacja.NowaOperacja(operacja);
                RepozytoriumOperacja.NowyTowarOperacji(operacja);
                Towary.Add(towar);
                return true;
            }  
            return false;
        }
        public bool UsunProduktyZKoszyka(ObservableCollection<Towar> przedmioty_z_koszyka)
        {
            int nowe_id_operacji = RepozytoriumOperacja.GetNextId();
            int id_pracownika = 1;
            int numer_pozycji;
            bool operacja_added = false;
            for (int i = 0; i < przedmioty_z_koszyka.Count; i++)
            {
                numer_pozycji = i + 1;
                Operacja operacja = new Operacja(nowe_id_operacji, przedmioty_z_koszyka[i], id_pracownika, "pobranie", numer_pozycji);
                if (!operacja_added)
                {
                    RepozytoriumOperacja.NowaOperacja(operacja);
                    operacja_added = true;
                }
                RepozytoriumOperacja.NowyTowarOperacji(operacja);
                Koszyk.Remove(przedmioty_z_koszyka[i]);
                int result = RepozytoriumTowar.OdejmijTowar(przedmioty_z_koszyka[i]);
                if (result == 1)
                {
                    Towary.Remove(przedmioty_z_koszyka[i]);
                }
                else if (result == 2)
                {
                    przedmioty_z_koszyka[i].Ilosc = przedmioty_z_koszyka[i].Ilosc - przedmioty_z_koszyka[i].IloscDoKoszyka;
                    przedmioty_z_koszyka[i].IloscDoKoszyka = 0;
                }
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAppManager.ViewModel
{
    using BaseClass;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using WarehouseAppManager.DAL.Encje;
    using WarehouseAppManager.Model;
    using WarehouseAppManager.View;
    class AddingViewModel : BaseViewModel
    {
        private Model model = null;
        private Magazyn magazyn = null;
        public Magazyn Magazyn
        {
            get { return magazyn; }
            set
            {
                magazyn = value;
                OnPropertyChanged(nameof(magazyn));
            }
        }
        public AddingViewModel() { }
        public AddingViewModel(Model model)
        {
            this.model = model;
            Towary = model.Towary;
        }

        #region towary

        private string nazwa_towaru, jednostka, regal;
        private int? id_pracownika, ilosc;
        private bool moznaDodacTowar = false;
        private bool isNotInDB = true;
        public Towar BiezacyTowar { get; set; }
        private int indeksZaznaczonegoTowaru = -1;
        private ObservableCollection<Towar> towary = null;
        public ObservableCollection<Towar> Towary
        {
            get { return towary; }
            set
            {
                towary = value;
                OnPropertyChanged(nameof(Towary));
            }
        }
        private ObservableCollection<Produkt> produkty = null;
        public ObservableCollection<Produkt> Produkty
        {
            get { return produkty; }
            set
            {
                produkty = value;
                OnPropertyChanged(nameof(Produkty));
            }
        }
        public string NazwaTowaru
        {
            get { return nazwa_towaru; }
            set
            {
                nazwa_towaru = value;
                OnPropertyChanged(nameof(NazwaTowaru));
                sprawdzCzyWBazie();
                sprawdzFormularz();
            }
        }
        public string Jednostka
        {
            get { return jednostka; }
            set
            {
                jednostka = value;
                OnPropertyChanged(nameof(Jednostka));
                sprawdzFormularz();
            }
        }
        public string Regal
        {
            get { return regal; }
            set
            {
                regal = value;
                regal = regal.ToUpper();
                OnPropertyChanged(nameof(Regal));
                sprawdzFormularz();
            }
        }
        public int? IdPracownika
        {
            get { return id_pracownika; }
            set
            {
                id_pracownika = value;
                OnPropertyChanged(nameof(IdPracownika));
                sprawdzFormularz();
            }
        }
        public int? Ilosc
        {
            get { return ilosc; }
            set
            {
                ilosc = value;
                OnPropertyChanged(nameof(Ilosc));
                sprawdzFormularz();
            }
        }
        public bool MoznaDodacTowar
        {
            get { return moznaDodacTowar; }
            set
            {
                moznaDodacTowar = value;
                OnPropertyChanged(nameof(moznaDodacTowar));
            }
        }
        public bool IsNotInDB
        {
            get { return isNotInDB; }
            set
            {
                isNotInDB = value;
                OnPropertyChanged(nameof(IsNotInDB));
            }
        }
        public int IndeksZaznaczonegoTowaru
        {
            get { return indeksZaznaczonegoTowaru; }
            set
            {
                indeksZaznaczonegoTowaru = value;
                OnPropertyChanged(nameof(IndeksZaznaczonegoTowaru));
            }
        }

        private void sprawdzCzyWBazie()
        {
            bool towar_istnieje = false;
            foreach (Towar t in Towary)
            {
                if (t.Nazwa == NazwaTowaru)
                {
                    towar_istnieje = true;
                }
            }
            if (towar_istnieje)
            {
                IsNotInDB = false;
            }
            else
            {
                IsNotInDB = true;
            }
        } 

        private void wyczyscFormularz()
        {
            NazwaTowaru = "";
            Ilosc = null;
            Jednostka = "";
            Regal = "";
            IdPracownika = null;
        }

        private void sprawdzFormularz()
        {
            if ((NazwaTowaru.Trim() != "") && (Ilosc.ToString() != "")
                    && (Jednostka == "kg" || Jednostka == "szt" || Jednostka == "m")
                    && (Regal.Trim() != "")
                    && (IdPracownika.ToString() != ""))
                MoznaDodacTowar = true;
            else
                MoznaDodacTowar = false;
        }

        #endregion

        #region polecenia

        private ICommand zaladujTowar = null;
        public ICommand ZaladujTowar
        {
            get
            {
                if (zaladujTowar == null)
                {
                    zaladujTowar = new RelayCommand(
                        arg => {
                            if (Magazyn != null)
                                Towary = model.GetTowarMagazynu(Magazyn.Id);
                        },
                        arg => true
                        );
                }
                return zaladujTowar;
            }
        }

        private ICommand dodajTowarDoBazy = null;
        public ICommand DodajTowarDoBazy
        {
            get
            {
                dodajTowarDoBazy = new RelayCommand(
                    arg => {
                        bool czyJest = false;
                        Towar towar = new Towar(int.Parse(Ilosc.ToString()), Jednostka, NazwaTowaru, Regal, Magazyn.Id);
                        bool czyProduktWBazie = false;
                        Produkt temp_p = new Produkt(towar.Nazwa);
                        foreach (Produkt p in Produkty)
                        {
                            if (p.Equals(temp_p))
                            {
                                czyProduktWBazie = true;
                                towar.Id = p.Id;
                                break;
                            }
                        }
                        towar.DodanaIlosc = towar.Ilosc;
                        foreach (Towar t in Towary)
                        {
                            if (t.Equals(towar))
                            {
                                czyJest = true;
                                towar.Ilosc += t.Ilosc;
                                break;
                            }
                        }
                        if (czyJest)
                        {
                            //aktualizuj ilosc towaru
                            model.AktualizujTowar(towar, int.Parse(IdPracownika.ToString()));
                        }
                        else
                        {
                            //dodaj towar do bazy
                            model.DodajTowar(towar, czyProduktWBazie, int.Parse(IdPracownika.ToString()));
                        }
                        AddWindow window = arg as AddWindow;
                        window.Close();
                    },
                    arg => true
                    );
                wyczyscFormularz();
                return dodajTowarDoBazy;
            }
        }

        private ICommand uzupelnijFormularz = null;
        public ICommand UzupelnijFormularz
        {
            get
            {
                uzupelnijFormularz = new RelayCommand(
                    arg =>
                    {
                        if(BiezacyTowar != null)
                        {
                            Jednostka = BiezacyTowar.Jednostka;
                            Regal = BiezacyTowar.Regal;
                            IsNotInDB = false;
                        }
                    },
                    arg => true
                    );
                return uzupelnijFormularz;
            }
        }
        #endregion
    }
}

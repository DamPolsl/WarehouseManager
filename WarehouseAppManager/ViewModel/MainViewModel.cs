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

    class MainViewModel:BaseViewModel
    {
        private Model model = new Model();

        #region TOWARY
        private int indeksZaznaczonegoTowaru = -1;
        private ObservableCollection<Towar> towary = null;
        public Towar BiezacyTowar { get; set; }
        public ObservableCollection<Towar> Towary {
            get { return towary; }
            set
            {
                towary = value;
                OnPropertyChanged(nameof(Towary));
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
        #endregion

        #region MAGAZYNY
        public ObservableCollection<Magazyn> Magazyny { get; set; }
        public Magazyn BiezacyMagazyn { get; set; }
        private string nazwa_magazynu;
        public string NazwaMagazynu
        {
            get { return nazwa_magazynu; }
            set
            {
                nazwa_magazynu = value;
                OnPropertyChanged(nameof(NazwaMagazynu));
            }
        }
        #endregion

        #region koszyk

        private int countKoszyk = 0;
        public int CountKoszyk
        {
            get { return countKoszyk; }
            set 
            { 
                countKoszyk = value;
                OnPropertyChanged(nameof(CountKoszyk));
            }
        }
        private ObservableCollection<Towar> koszyk = null;
        public ObservableCollection<Towar> Koszyk
        {
            get { return koszyk; }
            set
            {
                koszyk = value;
                OnPropertyChanged(nameof(Koszyk));
            }
        }


        #endregion

        #region produkty
        public ObservableCollection<Produkt> Produkty { get; set; }

        #endregion

        public AddingViewModel AddingVM { get; set; }
        public MainViewModel()
        {
            Towary = model.Towary;
            Magazyny = model.Magazyny;
            Produkty = model.Produkty;
            AddingVM = new AddingViewModel(model)
            {
                Magazyn = BiezacyMagazyn
            };
        }

        #region POLECENIA

        private ICommand zaladujTowar = null;
        public ICommand ZaladujTowar
        {
            get
            {
                if (zaladujTowar == null)
                {
                    zaladujTowar = new RelayCommand(
                        arg => {
                            if (BiezacyMagazyn != null)
                            {
                                Towary = model.GetTowarMagazynu(BiezacyMagazyn.Id);
                                Koszyk = model.GetTowarKoszyka(BiezacyMagazyn.Id);
                                AddingVM.Magazyn = BiezacyMagazyn;
                                AddingVM.Towary = Towary;
                                AddingVM.Produkty = Produkty;
                            }
                        },
                        arg => true
                        );
                }
                return zaladujTowar;
            }
        }
        private ICommand zaladujKoszyk = null;
        public ICommand ZaladujKoszyk
        {
            get
            {
                if (zaladujKoszyk == null)
                {
                    zaladujKoszyk = new RelayCommand(
                        arg => {
                            if (BiezacyMagazyn != null)
                            {
                                Koszyk = model.GetTowarKoszyka(BiezacyMagazyn.Id);
                                CountKoszyk = Koszyk.Count;
                            }
                        },
                        arg => true
                        );
                }
                return zaladujKoszyk;
            }
        }

        private ICommand otworzOknoDodawania = null;
        public ICommand OtworzOknoDodawania
        {
            get
            {
                otworzOknoDodawania = new RelayCommand(
                    arg =>
                    {
                        AddWindow ekranDodawania = new AddWindow();
                        MainWindow ekranGlowny = arg as MainWindow;
                        ekranDodawania.Show();
                        ekranDodawania.Owner = ekranGlowny;
                        ekranDodawania.DataContext = ekranGlowny.DataContext;
                    },
                    arg => true
                    );
                return otworzOknoDodawania;
            }
        }

        private ICommand dodajDoKoszyka = null;
        public ICommand DodajDoKoszyka
        {
            get
            {
                dodajDoKoszyka = new RelayCommand(
                    arg =>
                    {
                        if (BiezacyTowar.IloscDoKoszyka <= BiezacyTowar.Ilosc && BiezacyTowar.IloscDoKoszyka > 0)
                        {
                            if (!Koszyk.Contains(BiezacyTowar))
                            {
                                Koszyk.Add(BiezacyTowar);
                                model.Koszyk.Add(BiezacyTowar);
                                CountKoszyk += 1;
                            }
                        }
                        else if (BiezacyTowar.IloscDoKoszyka > BiezacyTowar.Ilosc)
                        {
                            BiezacyTowar.IloscDoKoszyka = BiezacyTowar.Ilosc;
                            if (!Koszyk.Contains(BiezacyTowar))
                            {
                                Koszyk.Add(BiezacyTowar);
                                model.Koszyk.Add(BiezacyTowar);
                                CountKoszyk += 1;
                            }
                        }
                        else
                        {
                            if (BiezacyTowar.IloscDoKoszyka == 0 && Koszyk.Contains(BiezacyTowar))
                            {
                                Koszyk.Remove(BiezacyTowar);
                                model.Koszyk.Remove(BiezacyTowar);
                                CountKoszyk -= 1;
                            }
                        }
                    },
                    arg => true
                    );
                return dodajDoKoszyka;
            }
        }

        private ICommand usunZawartoscKoszyka = null;
        public ICommand UsunZawartoscKoszyka
        {
            get
            {
                usunZawartoscKoszyka = new RelayCommand(
                    arg =>
                    {
                        model.UsunProduktyZKoszyka(Koszyk);
                        Towary = model.GetTowarMagazynu(BiezacyMagazyn.Id);
                        Koszyk = new ObservableCollection<Towar>();
                        CountKoszyk = Koszyk.Count();
                    },
                    arg => true
                    );
                return usunZawartoscKoszyka;
            }
        }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Projekt_Poig.ViewModel
{
    using Model;
    using DAL.Encje;
    using BaseClass;
    using System.Windows.Input;
    class TypyViewModel : ViewModelBase
    {
        private Model model = null;
        private ObservableCollection<Typ> typ = null;
        private string nazwa_gracza="",opis="";
        private ICommand dodaj = null;
        private ICommand usun = null;
        public Typ BiezacyTyp { get; set; }
        public ObservableCollection<Typ> Typ
        {
            get { return typ; }
            set
            {
                typ = value;
                OnPropertyChanged(nameof(Typ));
            }
        }
        public ICommand Dodaj
        {

            get
            {
                if (dodaj == null)
                    dodaj = new RelayCommand(
                        arg =>
                        {
                            var osoba = new Typ(Nazwa_gracza,Opis);

                            if (model.DodajTyp_GraczaDoBazy(osoba))
                            {
                                CzyscFormularz();
                                System.Windows.MessageBox.Show("Pomyslnie dodales typ gracza");
                            }
                        }
                        ,
                        arg => (Nazwa_gracza != "") && (Opis != "") 
                        );


                return dodaj;
            }

        }
        public ICommand Usun
        {

            get
            {
                if (usun == null)
                    usun = new RelayCommand(
                        arg =>
                        {
                            var osoba = new Typ(Nazwa_gracza, Opis);
                           

                            if (model.UsunTyp_GraczaZBazy(BiezacyTyp))
                            {

                                System.Windows.MessageBox.Show("Pomyslnie Usunoles typ gracza");
                            }
                        }
                        ,
                        arg => (BiezacyTyp != null)
                        );


                return usun;
            }

        }
        public string Nazwa_gracza
        {
            get { return nazwa_gracza; }
            set
            {
                nazwa_gracza = value;
                OnPropertyChanged(nameof(Nazwa_gracza));
            }

        }
        public string Opis
        {
            get { return opis; }
            set
            {
                opis = value;
                OnPropertyChanged(nameof(Opis));
            }

        }
        public TypyViewModel(Model model)
        {
            this.model = model;
            typ = model.Typy;
        }
        
        public void CzyscFormularz()
        {
            Nazwa_gracza = "";
            Opis = "";
        }
        public void Formularz()
        {
            Nazwa_gracza = nazwa_gracza;
            Opis = opis;
        }
        public void OdswiezTypy() => Typ = model.Typy;

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using HealthFitness.Models;

namespace HealthFitness.ViewModels
{
    [QueryProperty("PegarIDNavegacao", "parametro_id")]
    internal class CadastroExercicioViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string descricao, observacoes;
        int id;
        DateTime data;
        double? peso;

        public string PegarIDNavegacao
        {
            set
            {
                int id_parametro = Convert.ToInt32(Uri.UnescapeDataString(value));

                VerExercicio.Execute(id_parametro);

                //Carregar do BD
            }
            
        }

        public string Descricao
        {
            get => descricao;
            set
            {
                descricao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Descricao"));
            }
        }

        public string Observacoes
        {
            get => observacoes;
            set
            {
                observacoes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Observacoes"));
            }
        }

        public int ID
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }

        public DateTime Data
        {
            get => data;
            set
            {
                data = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Data"));
            }
        }

        public double? Peso
        {
            get => peso;
            set
            {
                peso = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Peso"));
            }
        }

        public ICommand NovoExercicio
        {
            get => new Command(() =>
            {
                ID = 0;
                Descricao = String.Empty;
                Data = DateTime.Now;
                Peso = null;
                Observacoes = String.Empty;
            });

        }

        public ICommand SalvarExercicio
        {
            get => new Command(async () =>
            {
                try
                {
                    Exercicio model = new Exercicio()
                    {
                        Descricao = this.Descricao,
                        Data = this.Data,
                        Peso = this.Peso,
                        Observacoes = this.Observacoes
                    };
                    if(this.ID == 0)
                    {
                        await App.Database.Insert(model);
                    } else 
                    {
                        model.ID = this.ID;

                        await App.Database.Update(model);
                    }

                    await Application.Current.MainPage.DisplayAlert("Atenção!", "Exercício Salvo!", "OK");
                    await Shell.Current.GoToAsync("//ListaExercicio");

                } catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção!", ex.Message, "OK");
                }    
            });
        }

        public ICommand VerExercicio
        {
            get => new Command<int>(async (int id) =>
            {
                try
                {
                    Exercicio model = await App.Database.GetById(id);

                    this.ID = model.ID;
                    this.Descricao = model.Descricao;
                    this.Peso = model.Peso;
                    this.Data = model.Data;
                    this.Observacoes = model.Observacoes;

                }catch(Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção", ex.Message, "OK");
                }
               
            });
        }
        
    }
}


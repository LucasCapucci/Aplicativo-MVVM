using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using HealthFitness.Models;
using Xamarin.Forms;

namespace HealthFitness.ViewModels
{
    internal class ListaExercicioViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /**
         * Pega o que for digitado pelo usuário na área de busca
         */
        public string ParametroBusca { get; set; }
        /**
         * Configurações do RefreshView
         */
        bool Atualizando = false;
        public bool atualizando
        {
            get => Atualizando;
            set
            {
                Atualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("atualizando"));
            }
        }
        /**
         * Armazena todas atividades do usuário.
         */
        ObservableCollection<Exercicio> listaExercicio = new ObservableCollection<Exercicio>();
        public ObservableCollection<Exercicio> ListaExercicio
        {
            get => listaExercicio;
            set => listaExercicio = value; 
        }

        public ICommand Buscar
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (atualizando)
                            return;

                        atualizando = true;

                        List<Exercicio> tmp = await App.Database.Search(ParametroBusca);

                        ListaExercicio.Clear();

                        tmp.ForEach(i => ListaExercicio.Add(i));
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Atenção", ex.Message, "OK");
                    }finally
                    {
                        atualizando = false;
                    }
                    
                }); 
            }
        }

        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (atualizando)
                            return;

                        atualizando = true;

                        List<Exercicio> tmp = await App.Database.GetAllRows();

                        ListaExercicio.Clear();

                        tmp.ForEach(i => ListaExercicio.Add(i));
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Atenção", ex.Message, "OK");
                    }
                    finally
                    {
                        atualizando = false;
                    }

                });
            }
        }

        public ICommand AbrirDetalhesExercicio
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    await Shell.Current.GoToAsync($"//CadastroExercicio?parametro_id={id}");
                });
            }
        }

        public ICommand Remover
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    try
                    {
                        bool confirmacao = await Application.Current.MainPage.DisplayAlert("Tem Certeza?", "Excluir", "Sim", "Não");

                        if (confirmacao)
                        {
                            await App.Database.Delete(id);
                            AtualizarLista.Execute(null);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Atenção", ex.Message, "OK");
                    }
                    finally
                    {
                        atualizando = false;
                    }

                });
            }
        }
    }
}

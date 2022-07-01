using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthFitness.ViewModels;

namespace HealthFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaExercicio : ContentPage
    {
        public ListaExercicio()
        {
            BindingContext = new ListaExercicioViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var vm = (ListaExercicioViewModel)BindingContext;

            vm.AtualizarLista.Execute(null);
        }
    }
}
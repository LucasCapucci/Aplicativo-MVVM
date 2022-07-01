using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthFitness.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroExercicio : ContentPage
    {
        public CadastroExercicio()
        {
            InitializeComponent();

            BindingContext = new CadastroExercicioViewModel();
        }

        protected override async void OnAppearing()
        {
            var vm = (CadastroExercicioViewModel)BindingContext;

            if(vm.ID == 0)
            {
                vm.NovoExercicio.Execute(null);
            }
        }
    }
}
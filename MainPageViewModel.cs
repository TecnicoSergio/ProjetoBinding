using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace AppBindingCommands
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        //digitar ctor + TAB + TAB
        public MainPageViewModel()
        {
            ShowMessageCommand = new Command(ShowMessage);
            CountCommand = new Command(async () => await CountCharacters());
            CleanCommand = new Command(async () => await CleanConfirmation());
            OptionCommand = new Command(async () => await ShowOptions());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string name = string.Empty;//CTRL + R, E(para aparecer o n minusculo tem que clicar CTRL + R, E)
        public string Name { get => name;
            set
            {
                if (name == null)
                    return;

                name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string DisplayName =>
            $"Nome digitado: {Name}";


        string displayMessage = string.Empty;
        public string DisplayMessage
        {
            get => displayMessage;
            set
            {
                if (displayMessage == null)
                    return;

                displayMessage = value;
                OnPropertyChanged(nameof(DisplayMessage));
            }
        }

        public ICommand ShowMessageCommand { get; }

        public void ShowMessage()
        {
            //using xamarin.Forms
            string dataProperty = Application.Current.Properties["dtAtual"].ToString();

            DisplayMessage = $"Boa noite {Name}, Hoje é {dataProperty}";
        }

        public async Task CountCharacters()//using System.Threading.Tasks;
        {
            string nameLenght =
                string.Format("Seu nome tem {0} Letras", name.Length);

            await Application.Current.MainPage.DisplayAlert("informação", nameLenght, "Ok");
        }

        public ICommand CountCommand { get; }
        public ICommand CleanCommand { get; }
        public ICommand OptionCommand { get; }

      public async Task CleanConfirmation()
        {
            if (await Application.Current.MainPage.DisplayAlert("Confirmação", "Confirma Limpeza dos dados?", "YES", "No"))
            {
                Name = string.Empty;
                DisplayMessage = string.Empty;
                OnPropertyChanged(Name);
                OnPropertyChanged(DisplayMessage);

                await Application.Current.MainPage.DisplayAlert("Informação", "Limpeza realizada com sucesso", "Ok");
            }
        }

        public async Task ShowOptions()
        {
            string result;
            result = await Application.Current.MainPage
                .DisplayActionSheet("Seleção", "seleciona uma opção: ", "Cancelar", "Limpar", "Contar Caracteres", "Exibir Saudação");
            if (result != null)
            {
                if (result.Equals("Limpar"))
                    await CleanConfirmation();

                if (result.Equals("Contar Caracteres"))
                    await CountCharacters();

                if (result.Equals("Exibir Saudação"))
                    ShowMessage();
            }

        }


       


    }
}

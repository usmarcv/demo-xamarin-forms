using DemoSprintXamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoSprintXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientesView : ContentPage
    {
        public ClientesView()
        {
            InitializeComponent();
        }

        ClienteService clienteService = new ClienteService();

        protected async override void OnAppearing()
        {
            await ExibeClientes();
        }

        private async void BtnExibir_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Erro", "Id do cliente inválido!", "Ok");
            }
            else
            {
                try
                {
                    var cliente = await clienteService.GetCliente(Convert.ToInt32(txtId.Text));
                    if(cliente != null)
                    {
                        txtId.Text = cliente.Id.ToString();
                        txtNome.Text = cliente.Nome;
                        txtCpf.Text = cliente.Cpf;
                        txtEndereco.Text = cliente.Endereco;
                        txtLimite.Text = cliente.Limite;
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Não Existe contato com esse Id", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", "Cliente não encontrado!"    +    ex.Message, "Ok");
                }
            }
        }

        private async void BtnInserir_Clicked(object sender, EventArgs e)
        {
            await clienteService.AddCliente(Convert.ToInt32(txtId.Text), txtNome.Text, txtCpf.Text, txtEndereco.Text, txtLimite.Text);

            txtId.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCpf.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtLimite.Text = string.Empty;

            await DisplayAlert("Success", "Contato incluído com sucesso!", "Ok");

            await ExibeClientes();

        }


        private async Task ExibeClientes()
        {
            var clientes = await clienteService.GetClientes();
            listaClientes.ItemsSource = clientes;
        }

        private async void BtnAtualizar_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Erro", "Id do cliente inválido!", "Ok");
            }
            else
            {
                try
                {
                    await clienteService.UpdateCliente(Convert.ToInt32(txtId.Text), txtNome.Text, txtCpf.Text, txtEndereco.Text, txtLimite.Text);

                    txtId.Text = string.Empty;
                    txtNome.Text = string.Empty;
                    txtCpf.Text = string.Empty;
                    txtEndereco.Text = string.Empty;
                    txtLimite.Text = string.Empty;

                    await DisplayAlert("Success", "Cliente atualizado com sucesso!", "Ok");

                    await ExibeClientes();

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", "Cliente não atualizado!" + ex.Message, "Ok");
                    
                }
            }
        }

        private async void BtnDeletar_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Erro", "Id do cliente inválido!", "Ok");
            }
            else
            { 
                try
                {
                    await clienteService.DeleteCliente(Convert.ToInt32(txtId.Text));

                    txtId.Text = string.Empty;
                     
                    await DisplayAlert("Success", "Cliente deletado com sucesso!", "Ok");

                    await ExibeClientes();

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", "Cliente não deletado!" + ex.Message, "Ok");

                }
            }

        }
    }
}
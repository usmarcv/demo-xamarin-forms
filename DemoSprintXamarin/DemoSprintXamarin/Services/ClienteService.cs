using DemoSprintXamarin.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSprintXamarin.Services
{
    public class ClienteService
    {
        FirebaseClient firebase =
            new FirebaseClient("https://demosprintxamarin-default-rtdb.firebaseio.com/");

        public async Task AddCliente(int clienteId, string nome, string cpf, string endereco, string limite)
        {
            await firebase.Child("Clientes")
                .PostAsync(new Cliente() { Id = clienteId, Nome = nome, Cpf = cpf, Endereco = endereco, Limite = limite });
        }


        public async Task<List<Cliente>> GetClientes()
        {
            return (await firebase
                .Child("Clientes")
                .OnceAsync<Cliente>()).Select(item => new Cliente
                {
                    Nome = item.Object.Nome,
                    Cpf = item.Object.Cpf,
                    Endereco = item.Object.Endereco,
                    Limite = item.Object.Limite
                }).ToList();
        }


        public async Task<Cliente> GetCliente (int clienteId)
        {
            try
            {
                var cliente = (await firebase
                    .Child("Clientes")
                    .OnceAsync<Cliente>())
                    .Where(a => a.Object.Id == clienteId).FirstOrDefault();

                return await firebase.Child("Clientes")
                    .Child(cliente.Key).OnceSingleAsync<Cliente>();
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public async Task UpdateCliente(int clienteId, string nome, string cpf, string endereco, string limite)
        {
            try
            {
                var toUpdateCliente = (await firebase
                    .Child("Clientes")
                    .OnceAsync<Cliente>())
                    .Where(a => a.Object.Id == clienteId).FirstOrDefault();

                await firebase
                    .Child("Clientes")
                    .Child(toUpdateCliente.Key)
                    .PutAsync(new Cliente()
                    {
                        Id = clienteId, Nome = nome, Cpf = cpf, Endereco = endereco, Limite = limite
                    });
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public async Task DeleteCliente(int Id)
        {
            try
            {
                var toDeleteCliente = (await firebase
                    .Child("Clientes")
                    .OnceAsync<Cliente>())
                    .Where(a => a.Object.Id == Id).FirstOrDefault();

                await firebase
                    .Child("Clientes")
                    .Child(toDeleteCliente.Key)
                    .DeleteAsync(); 
            }
            catch (System.Exception)
            {
                throw;
            }
        }

      
    }
}

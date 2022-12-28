using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeClientes
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public int cpf;
            //Sempre que for criar uma variavel global usar statics.
        }
        static List<Cliente> clientes = new List<Cliente>();
           

        enum Menu {Listagem = 1, Adicionar, Remover, Sair }

        static void Main(string[] args)
        {
            Leituradedados();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistemas de clientes - Bem vindo!!");
                Console.WriteLine("Por gentileza selecione uma opção abaixo:");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int OpcaoSelecionada = int.Parse(Console.ReadLine());
                Menu Opcao = (Menu)OpcaoSelecionada;

                switch (Opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                    default:
                        Console.WriteLine("Opção invalida!!");
                        break;
                }
                Console.Clear();
            }
        }
  
     static void Adicionar()
        {

            // formulario
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente: ");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do cliente: ");
            cliente.cpf = int.Parse(Console.ReadLine());

            clientes.Add(cliente); // -> para adicionar o cliente na lista.
            Salvarosdados();


            Console.WriteLine("Cadastro Concluido, aperte enter para voltar ao menu inicial");
            Console.ReadLine();
        }
        static void Listagem()
        {   if (clientes.Count > 0) // Se tem pelo menos um cliente cadastrado
            {
                Console.WriteLine("Lista de clientes: ");
                int id = 1;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID do cliente: {id}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("==========================");
                    id++;
                }
            }
        else
            {
                Console.WriteLine("Não há clientes cadastrados");
            }
                Console.WriteLine("Aperte ENTER para voltar ao menu inicial");
                 Console.ReadLine();
            
        }

        static void Salvarosdados()
        {
            FileStream stream = new FileStream("dadosdosclientes", FileMode.OpenOrCreate);
            BinaryFormatter dados = new BinaryFormatter();

            dados.Serialize(stream, clientes);

            stream.Close();

        }
        static void Leituradedados()
        {
            FileStream stream = new FileStream("dadosdosclientes", FileMode.OpenOrCreate);
            try // Tenta executar um bloco de código, se acontecer um erro o programa não para, executa o CATCH.
            {
                
                BinaryFormatter dados = new BinaryFormatter();

                clientes = (List<Cliente>)dados.Deserialize(stream); // se eu tentar ler e tiver vazio
                if(clientes == null) // Cria uma nova lista de clientes caso seja null
                {

                    clientes = new List<Cliente>();
                }

                stream.Close();
            }
            catch (Exception)
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        } 
        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do cliente que voce deseja remover:");
           int id = int.Parse(Console.ReadLine());
            if (id > 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id); // remove o id do cliente e salva novamente
                Salvarosdados();

            }
            else {
                Console.WriteLine("Id digitado inválido, tente novamente");
                Console.ReadLine();
            }
        }
    } 

    }



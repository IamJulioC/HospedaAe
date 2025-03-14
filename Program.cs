using System;
using System.Collections.Generic;
using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

// Cria os modelos de hóspedes e cadastra na lista de hóspedes
List<Pessoa> hospedes = new List<Pessoa>();

Console.WriteLine("Digite o número de hóspedes:");
int numeroHospedes = int.Parse(Console.ReadLine());

for (int i = 0; i < numeroHospedes; i++)
{
    Console.WriteLine($"Digite o nome do hóspede {i + 1}:");
    string nome = Console.ReadLine();
    Console.WriteLine($"Digite o sobrenome do hóspede {i + 1}:");
    string sobrenome = Console.ReadLine();
    hospedes.Add(new Pessoa(nome, sobrenome));
}

// Cria as suítes e as adiciona a uma lista
List<Suite> suites = new List<Suite>();
suites.Add(new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 30));
suites.Add(new Suite(tipoSuite: "Master", capacidade: 3, valorDiaria: 150));
suites.Add(new Suite(tipoSuite: "Executive", capacidade: 4, valorDiaria: 200));
suites.Add(new Suite(tipoSuite: "Standard", capacidade: 2, valorDiaria: 100));
suites.Add(new Suite(tipoSuite: "Basic", capacidade: 1, valorDiaria: 50));

// Exibe as opções de suíte para o cliente
Console.WriteLine("Escolha a suíte desejada:");
for (int i = 0; i < suites.Count; i++)
{
    Console.WriteLine($"{i + 1}. {suites[i].TipoSuite} - Capacidade: {suites[i].Capacidade}, Valor da diária: {suites[i].ValorDiaria}");
}

// Lê a escolha do cliente
int escolhaSuite = int.Parse(Console.ReadLine()) - 1;

// Valida a escolha do cliente
if (escolhaSuite < 0 || escolhaSuite >= suites.Count)
{
    Console.WriteLine("Escolha de suíte inválida.");
    return;
}

// Lê o número de dias da reserva
Console.WriteLine("Digite o número de dias da reserva:");
int diasReservados = int.Parse(Console.ReadLine());

// Cria a reserva com a suíte escolhida
Reserva reserva = new Reserva(diasReservados);
reserva.CadastrarSuite(suites[escolhaSuite]);

try
{
    reserva.CadastrarHospedes(hospedes);

    // Calcula os valores da diária
    decimal valorOriginal = reserva.CalcularValorDiaria();
    decimal valorComDesconto = valorOriginal;

    if (diasReservados > 30)
    {
        valorComDesconto *= 0.88M; // 12% de desconto
    }
    else if (diasReservados >= 10)
    {
        valorComDesconto *= 0.9M; // 10% de desconto
    }

    // Exibe os valores da diária
    Console.WriteLine($"Valor original da diária: {valorOriginal:C}");
    Console.WriteLine($"Valor da diária com desconto: {valorComDesconto:C}");

    Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}
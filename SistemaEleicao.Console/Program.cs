using SistemaEleicao.Console.Services;

namespace SistemaEleicao.Console;

public class Program
{
    private static readonly EleicaoManager _manager = new();

    public static void Main(string[] args)
    {
        bool executando = true;

        while (executando)
        {
            System.Console.Clear();
            System.Console.WriteLine("=====================================");
            System.Console.WriteLine("      SISTEMA DE ELEIÇÃO DIGITAL     ");
            System.Console.WriteLine("=====================================");
            System.Console.WriteLine("1. Cadastrar Candidato");
            System.Console.WriteLine("2. Votar");
            System.Console.WriteLine("3. Exibir Apuração e Vencedor");
            System.Console.WriteLine("0. Sair");
            System.Console.WriteLine("=====================================");
            System.Console.Write("Escolha uma opção: ");

            string? opcao = System.Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    MenuCadastrarCandidato();
                    break;
                case "2":
                    MenuVotar();
                    break;
                case "3":
                    MenuApuracao();
                    break;
                case "0":
                    executando = false;
                    System.Console.WriteLine("\nEncerrando o sistema...");
                    break;
                default:
                    PressionarParaContinuar("Opção inválida! Pressione qualquer tecla para tentar novamente.");
                    break;
            }
        }
    }

    private static void MenuCadastrarCandidato()
    {
        System.Console.Clear();
        System.Console.WriteLine("--- Cadastro de Candidato ---");

        System.Console.Write("Digite o número do candidato: ");
        if (!int.TryParse(System.Console.ReadLine(), out int numero) || numero <= 0)
        {
            PressionarParaContinuar("Número inválido! Digite um número positivo maior que zero.");
            return;
        }

        System.Console.Write("Digite o nome do candidato: ");
        string? nome = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            PressionarParaContinuar("O nome não pode ser vazio.");
            return;
        }

        if (_manager.CadastrarCandidato(numero, nome))
        {
            PressionarParaContinuar($"Candidato '{nome}' ({numero}) cadastrado com sucesso!");
        }
        else
        {
            PressionarParaContinuar($"Erro: O número {numero} já está cadastrado.");
        }
    }

    private static void MenuVotar()
    {
        System.Console.Clear();
        System.Console.WriteLine("--- Registro de Voto ---");

        System.Console.Write("Digite o Título/CPF do Eleitor: ");
        string? documento = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(documento))
        {
            PressionarParaContinuar("Documento inválido.");
            return;
        }

        if (_manager.EleitorJaVotou(documento))
        {
            PressionarParaContinuar("ERRO: Este eleitor já registrou seu voto anteriormente!");
            return;
        }

        System.Console.WriteLine("\nCandidatos Disponíveis:");
        if (_manager.Candidatos.Count == 0)
        {
            System.Console.WriteLine(" (Nenhum candidato cadastrado até o momento)");
        }
        else
        {
            foreach (var cand in _manager.Candidatos)
            {
                System.Console.WriteLine($" [{cand.Numero}] - {cand.Nome}");
            }
        }

        System.Console.WriteLine(" [0] - Voto em Branco");
        System.Console.WriteLine(" (Qualquer outro número resultará em voto NULO)\n");

        System.Console.Write("Digite o número da sua opção/candidato: ");
        if (!int.TryParse(System.Console.ReadLine(), out int numeroVoto))
        {
            PressionarParaContinuar("Entrada inválida.");
            return;
        }

        if (_manager.RegistrarVoto(documento, numeroVoto))
        {
            PressionarParaContinuar("Voto registrado com sucesso!");
        }
    }

    private static void MenuApuracao()
    {
        System.Console.Clear();
        System.Console.WriteLine("=====================================");
        System.Console.WriteLine("         APURAÇÃO DOS VOTOS          ");
        System.Console.WriteLine("=====================================");

        var apuracao = _manager.ObterApuracao();

        System.Console.WriteLine($"Total de Votos Registrados: {apuracao.TotalVotos}\n");

        System.Console.WriteLine("Votos por Candidato:");
        foreach (var cand in apuracao.Candidatos)
        {
            System.Console.WriteLine($" - {cand.Nome} ({cand.Numero}): {cand.Votos} voto(s)");
        }

        System.Console.WriteLine($"\n- Votos em Branco: {apuracao.VotosBrancos}");
        System.Console.WriteLine($"- Votos Nulos: {apuracao.VotosNulos}");
        System.Console.WriteLine("-------------------------------------");

        if (apuracao.TotalVotos == 0)
        {
            System.Console.WriteLine("Nenhum voto foi computado até o momento.");
        }
        else if (apuracao.Vencedores.Count == 1)
        {
            var vencedor = apuracao.Vencedores.First();
            System.Console.WriteLine($"RESULTADO: O vencedor foi {vencedor.Nome} com {vencedor.Votos} voto(s)!");
        }
        else if (apuracao.Vencedores.Count > 1)
        {
            System.Console.WriteLine("RESULTADO: Empate entre os seguintes candidatos:");
            foreach (var v in apuracao.Vencedores)
            {
                System.Console.WriteLine($" - {v.Nome} ({v.Votos} votos)");
            }
        }
        else
        {
            System.Console.WriteLine("RESULTADO: Não houve vencedor (somente votos nulos/brancos).");
        }

        PressionarParaContinuar();
    }

    private static void PressionarParaContinuar(string? mensagem = null)
    {
        if (!string.IsNullOrEmpty(mensagem))
        {
            System.Console.WriteLine($"\n{mensagem}");
        }
        System.Console.WriteLine("\nPressione qualquer tecla para continuar...");
        System.Console.ReadKey();
    }
}

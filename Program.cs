using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<string> lista1 = new List<string>();
        List<string> lista2 = new List<string>();
        bool listasDefinidas = false; // Indica se a configuração das listas foi concluída

        while (true) // Loop para manter o programa em execução
        {
            if (!listasDefinidas)
            {
                Console.WriteLine("\nDigite as palavras da primeira lista (use ';' para separar linhas e ',' para separar palavras). Pressione Enter duas vezes para finalizar:");
                string input = LerEntradaMultiline();

                if (input.ToLower() == "sair")
                {
                    break;
                }

                if (input.ToLower() == "mostrar listas")
                {
                    MostrarListas(lista1, lista2);
                    continue;
                }

                if (input.ToLower() == "reiniciar listas")
                {
                    lista1.Clear();
                    lista2.Clear();
                    Console.WriteLine("\nListas foram reiniciadas. Digite novamente a primeira e segunda lista.");
                    continue;
                }

                // Processar a entrada da primeira lista
                lista1 = ProcessarEntrada(input);

                Console.WriteLine("\nDigite as palavras da segunda lista (use ';' para separar linhas e ',' para separar palavras). Pressione Enter duas vezes para finalizar:");
                input = LerEntradaMultiline();

                if (input.ToLower() == "sair")
                {
                    break;
                }

                if (input.ToLower() == "mostrar listas")
                {
                    MostrarListas(lista1, lista2);
                    continue;
                }

                if (input.ToLower() == "reiniciar listas")
                {
                    lista1.Clear();
                    lista2.Clear();
                    Console.WriteLine("\nListas foram reiniciadas. Digite novamente a primeira e segunda lista.");
                    continue;
                }

                // Processar a entrada da segunda lista
                lista2 = ProcessarEntrada(input);

                listasDefinidas = true; // Indica que as listas foram definidas

                Console.WriteLine("\nDigite as palavras da terceira lista (use ';' para separar linhas e ',' para separar palavras). Pressione Enter duas vezes para finalizar:");
            }

            // Solicitar a terceira lista
            string terceiraListaInput = LerEntradaMultiline();

            if (terceiraListaInput.ToLower() == "sair")
            {
                break;
            }

            if (terceiraListaInput.ToLower() == "mostrar listas")
            {
                MostrarListas(lista1, lista2);
                continue;
            }

            if (terceiraListaInput.ToLower() == "reiniciar listas")
            {
                lista1.Clear();
                lista2.Clear();
                listasDefinidas = false; // Voltar para o estado de definição das listas
                Console.WriteLine("\nListas foram reiniciadas. Digite novamente a primeira e segunda lista.");
                continue;
            }

            List<string> lista3 = ProcessarEntrada(terceiraListaInput);

            // Comparar a lista3 com lista1 e lista2
            List<string> palavrasNaoContidas = lista3.Where(palavra =>
                !lista1.Contains(palavra) && !lista2.Contains(palavra)).ToList();

            if (palavrasNaoContidas.Count == 0)
            {
                Console.WriteLine("\nTodas as palavras da terceira lista já estão na primeira ou na segunda lista.");
            }
            else
            {
                Console.WriteLine("\nPalavras que não estão nem na primeira nem na segunda lista:");
                foreach (var palavra in palavrasNaoContidas)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; // Cor Azul para palavras que não estão nas listas
                    Console.WriteLine(palavra);
                }
                Console.ResetColor();
            }

            Console.WriteLine("\nVerificação individual das palavras da terceira lista:");
            foreach (var palavra in lista3)
            {
                if (lista1.Contains(palavra))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Cor Amarela para a primeira lista
                    Console.WriteLine($"A palavra '{palavra}' está na primeira lista.");
                }
                else if (lista2.Contains(palavra))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta; // Cor Roxa para a segunda lista
                    Console.WriteLine($"A palavra '{palavra}' está na segunda lista.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; // Cor Azul para palavras que não estão nas listas
                    Console.WriteLine($"A palavra '{palavra}' não está nem na primeira nem na segunda lista.");
                }

                Console.ResetColor();
            }

            foreach (var palavra in palavrasNaoContidas)
            {
                if (palavra.StartsWith("_"))
                {
                    if (!lista2.Contains(palavra))
                    {
                        lista2.Add(palavra);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"A palavra '{palavra}' foi adicionada à segunda lista.");
                    }
                }
                else
                {
                    if (!lista1.Contains(palavra))
                    {
                        lista1.Add(palavra);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"A palavra '{palavra}' foi adicionada à primeira lista.");
                    }
                }

                Console.ResetColor();
            }

            Console.WriteLine("\n------------------------------------------");
        }
    }

    static List<string> ProcessarEntrada(string entrada)
    {
        // Processa a entrada para remover espaços extras e separar palavras
        return entrada.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(p => p.Trim())
                      .Where(p => !string.IsNullOrEmpty(p))
                      .ToList();
    }

    static void MostrarListas(List<string> lista1, List<string> lista2)
    {
        Console.WriteLine("\nConteúdo da primeira lista:");
        if (lista1.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // Cor Amarela para a primeira lista
            Console.WriteLine(string.Join(", ", lista1));
        }
        else
        {
            Console.WriteLine("A primeira lista está vazia.");
        }
        Console.ResetColor();

        Console.WriteLine("\nConteúdo da segunda lista:");
        if (lista2.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Magenta; // Cor Roxa para a segunda lista
            Console.WriteLine(string.Join(", ", lista2));
        }
        else
        {
            Console.WriteLine("A segunda lista está vazia.");
        }
        Console.ResetColor();
    }

    static string LerEntradaMultiline()
    {
        // Lê a entrada até que o usuário pressione Enter duas vezes consecutivas
        Console.WriteLine("Digite seu texto (pressione Enter duas vezes para finalizar):");
        var entrada = "";
        string linha;
        while (!string.IsNullOrEmpty(linha = Console.ReadLine()))
        {
            entrada += linha + "\n";
        }
        return entrada.Trim();
    }
}

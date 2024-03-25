using System;
using System.Collections.Generic;

class Excursionista
{
    public string Nombre { get; set; }
    public int Peso { get; set; }
    public int Calorias { get; set; }

    public Excursionista(string nombre, int peso, int calorias)
    {
        Nombre = nombre;
        Peso = peso;
        Calorias = calorias;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Solicitar al usuario el número de elementos
        Console.WriteLine("Ingrese el número de elementos:");
        int numElementos = int.Parse(Console.ReadLine());

        // Definir los elementos disponibles
        Dictionary<string, Excursionista> elementos = new Dictionary<string, Excursionista>();
        for (int i = 1; i <= numElementos; i++)
        {
            Console.WriteLine($"Ingrese los detalles del elemento {i}:");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Peso: ");
            int peso = int.Parse(Console.ReadLine());
            Console.Write("Calorías: ");
            int calorias = int.Parse(Console.ReadLine());
            elementos.Add(nombre, new Excursionista(nombre, peso, calorias));
        }

        // Parámetros de usuario
        Console.WriteLine("Ingrese el mínimo de calorías:");
        int minCalorias = int.Parse(Console.ReadLine());
        Console.WriteLine("Ingrese el peso máximo:");
        int maxPeso = int.Parse(Console.ReadLine());

        // Encontrar conjunto óptimo de elementos
        List<Excursionista> conjuntoOptimo = EncontrarConjuntoOptimo(elementos, minCalorias, maxPeso);

        // Mostrar resultados
        Console.WriteLine("Conjunto óptimo de elementos:");
        foreach (var item in conjuntoOptimo)
        {
            Console.WriteLine($"Elemento: {item.Nombre}, Peso: {item.Peso}, Calorías: {item.Calorias}");
        }
    }

    static List<Excursionista> EncontrarConjuntoOptimo(Dictionary<string, Excursionista> elementos, int minCalorias, int maxPeso)
    {
        List<Excursionista> conjuntoOptimo = new List<Excursionista>();
        // Crear una matriz para almacenar los resultados de la programación dinámica
        int[,] matriz = new int[elementos.Count + 1, maxPeso + 1];

        for (int i = 1; i <= elementos.Count; i++)
        {
            for (int j = 1; j <= maxPeso; j++)
            {
                Excursionista elemento = elementos.ElementAt(i - 1).Value;
                if (elemento.Peso > j)
                {
                    matriz[i, j] = matriz[i - 1, j];
                }
                else
                {
                    matriz[i, j] = Math.Max(matriz[i - 1, j], matriz[i - 1, j - elemento.Peso] + elemento.Calorias);
                }
            }
        }

        // Reconstruir la solución
        int pesoActual = maxPeso;
        for (int i = elementos.Count; i > 0; i--)
        {
            Excursionista elemento = elementos.ElementAt(i - 1).Value;
            if (matriz[i, pesoActual] != matriz[i - 1, pesoActual])
            {
                conjuntoOptimo.Add(elemento);
                pesoActual -= elemento.Peso;
            }
        }

        return conjuntoOptimo;
    }
}
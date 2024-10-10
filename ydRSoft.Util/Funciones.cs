using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Util
{
    public class Funciones
    {
        public static string LetraMayuscula(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static string json = @"
        [
            {
                'Id': 1,
                'Nombre': 'Ensalada de zanahoria y manzana',
                'NivelDificultad': 4,
                'Tiempo': 12,
                'PasosPreparacion': [
                    'Mezclar todos los ingredientes en un tazón grande.',
                    'Ajustar la sal',
                    'jugo de limón al gusto.'
                ],
                'Ingredientes': [
                    '2 zanahorias, ralladas',
                    '1 manzana, picada en cubos',
                    '1/4 de taza de pasas',
                    '1/4 de taza de nueces, picadas',
                    '1/2 taza de yogur',
                    '2 cucharadas de miel',
                    'jugo de 1 limón',
                    'sal al gusto'
                ]
            },
            {
                'Id': 2,
                'Nombre': 'Sopa de zanahoria y manzana',
                'NivelDificultad': 2,
                'Tiempo': 5,
                'PasosPreparacion': [
                    'En una olla, calentar el aceite y sofreír la cebolla.',
                    'Agregar la zanahoria y la manzana, luego el caldo.',
                    'Cocinar hasta que estén tiernos',
                    'luego licuar hasta obtener una crema suave.',
                    'Ajustar sal y pimienta al gusto.',
                ],
                'Ingredientes': [
                    '3 zanahorias, peladas y picadas',
                    '1 manzana, pelada y picada',
                    '1 cebolla, picada',
                    '4 tazas de caldo de verduras',
                    '2 cucharadas de aceite de oliva',
                    'sal al gusto',
                    'pimienta al gusto',
                    'especias opcionales (jengibre o cúrcuma)'
                ]
            }
        ]";

    }
}

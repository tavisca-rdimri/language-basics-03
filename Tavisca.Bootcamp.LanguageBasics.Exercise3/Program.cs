using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            int totalMeals = protein.Length;
            int totalConsumers = dietPlans.Length;
            int[] menuSelectionIndex = new int[totalConsumers];

            int[] cal = new int[totalMeals];
            for(int i=0;i<totalMeals;i++)
            {
                cal[i] = 5 * (protein[i] + carbs[i]) + 9 * fat[i];
            }
            for(int i=0;i<totalConsumers;i++)
            {
                string dietPlanOfIndividual = dietPlans[i];
                if(dietPlanOfIndividual.Length == 0)
                {
                    menuSelectionIndex[i] = 0;
                }
                else
                {
                    List<int> temporaryListOfIndexes = new List<int>();
                    for(int j=0;j<totalMeals;j++)
                    {
                        temporaryListOfIndexes.Add(j);
                    }

                    for(int j=0;j<dietPlanOfIndividual.Length;j++)
                    {
                        switch(dietPlanOfIndividual[j])
                        {
                            case 'P':
                                temporaryListOfIndexes = recommendMealOfIndividual(temporaryListOfIndexes,true,protein);
                                break;
                            case 'p':
                                temporaryListOfIndexes = recommendMealOfIndividual(temporaryListOfIndexes, false, protein);
                                break;
                            case 'C':
                                temporaryListOfIndexes = recommendMealOfIndividual(temporaryListOfIndexes, true, carbs);
                                break;
                            case 'c':
                                temporaryListOfIndexes = recommendMealOfIndividual(temporaryListOfIndexes, false, carbs);
                                break;
                            case 'F':
                                temporaryListOfIndexes = recommendMealOfIndividual(temporaryListOfIndexes, true, fat);
                                break;
                            case 'f':
                                temporaryListOfIndexes = recommendMealOfIndividual(temporaryListOfIndexes, false, fat);
                                break;
                            case 'T':
                                temporaryListOfIndexes = recommendMealOfIndividual(temporaryListOfIndexes, true, cal);
                                break;
                            case 't':
                                temporaryListOfIndexes = recommendMealOfIndividual(temporaryListOfIndexes, false, cal);
                                break;
                        }
                    }
                    menuSelectionIndex[i] = temporaryListOfIndexes[0];
                }
            }
            return menuSelectionIndex;
            throw new NotImplementedException();
        }

        public static List<int> recommendMealOfIndividual(List<int> tempIndexes, bool isUpperCase, int[] ingredient)
        {
            int keyToBeOptimized = ingredient[tempIndexes[0]];
            if(tempIndexes.Count > 0 )
            {
                if(isUpperCase == true)
                {
                    for(int i=1;i<tempIndexes.Count;i++)
                    {
                        if(keyToBeOptimized < ingredient[tempIndexes[i]])
                        {
                            keyToBeOptimized = ingredient[tempIndexes[i]];
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < tempIndexes.Count; i++)
                    {
                        if (keyToBeOptimized > ingredient[tempIndexes[i]])
                        {
                            keyToBeOptimized = ingredient[tempIndexes[i]];
                        }
                    }
                }
            }

            List<int> newIndexes = new List<int>();
            for(int i=0;i<tempIndexes.Count;i++)
            {
                if(ingredient[tempIndexes[i]] == keyToBeOptimized) {
                    newIndexes.Add(tempIndexes[i]);
                }
            }
            return newIndexes;
        }
    }
}
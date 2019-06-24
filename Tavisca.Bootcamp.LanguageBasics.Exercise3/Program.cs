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

		public static List<int> FindAllIndex(int[] elements, List<int> indexLists, int elem) {
			

			var query = from i in indexLists 
				where elements[i] == elem 
				select i; 
			var indexList = query.ToList();
			return indexList;
		}

		public static int GetMax(int[] elements, List<int> indexList) {

			int max = elements[indexList[0]];

			for (int i = 1; i < indexList.Count; i++) {
				if (elements[indexList[i]] > max)
				max = elements[indexList[i]];
			}

			return max;
		}
		public static int GetMin(int[] elements, List<int> indexList) {
			
			int min = elements[indexList[0]];

			for (int i = 1; i < indexList.Count; i++) {
				if (elements[indexList[i]] < min) 
				min = elements[indexList[i]];
			}

			return min;
		}

		public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
		{  

			 int[] cal = new int[protein.Length];
			 int[] item = new int[dietPlans.Length];

			 for (int i = 0; i < protein.Length; i++) {
			 	cal[i] = (protein[i] + carbs[i]) * 5 + fat[i] * 9;
			 }

			 for (int i = 0; i < dietPlans.Length; i++) {
			 	string dietPlan = dietPlans[i];
			 	List<int> indexList = new List<int>();
			 	int max, min;

			 	if (dietPlan.Length == 0) {
			 		item[i] = 0;
			 	}

			 	indexList=Enumerable.Range(0, protein.Length).ToList();

			 	foreach (char ch in dietPlan) {

			 		switch(ch) {

			 			case 'p':
			 			min = GetMin(protein, indexList);
			 			indexList = FindAllIndex(protein, indexList, min);
			 			break;
			 			case 'P':
			 			max = GetMax(protein, indexList);
			 			indexList = FindAllIndex(protein, indexList, max);
			 			break;

			 			case 'c':
			 			min = GetMin(carbs, indexList);
			 			indexList = FindAllIndex(carbs, indexList, min);
			 			break;
			 			case 'C':
			 			max = GetMax(carbs, indexList);
			 			indexList = FindAllIndex(carbs, indexList, max);
			 			break;

			 			case 'f':
			 			min = GetMin(fat, indexList);
			 			indexList = FindAllIndex(fat, indexList, min);
			 			break;
			 			case 'F':
			 			max = GetMax(fat, indexList);
			 			indexList = FindAllIndex(fat, indexList, max);
			 			break;

			 			case 't':
			 			min = GetMin(cal, indexList);
			 			indexList = FindAllIndex(cal, indexList, min);
			 			break;
			 			case 'T':
			 			max = GetMax(cal, indexList);
			 			indexList = FindAllIndex(cal, indexList, max);
			 			break;

			 		}
			 	}

			 	item[i] = indexList[0];
			 }

			 return item;
			}
		}
	}
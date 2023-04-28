using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelCroMut {
	public static DNA Selection(DNA[] PopulationPool)
	{
		int k=10;
		float maxFitness = 0;
		int selectedIndex = 0;
		for(int i=0;i<k;i++)
		{
			int randIndex = Random.Range(0, PopulationPool.Length);
			if(PopulationPool[randIndex].FitnessValue>maxFitness)
			{
				selectedIndex = randIndex;
				maxFitness = PopulationPool[randIndex].FitnessValue;
			}
		}
		return PopulationPool[selectedIndex];
	}
	public static DNA CrossOver(DNA Parent1, DNA Parent2)
	{
		DNA Child = new DNA(Parent1.Genes.Length);
		int choice = Random.Range(0, Parent1.Genes.Length);
		for(int i=0;i<Parent1.Genes.Length;i++)
		{
			if(i<choice)
			{
				Child.Genes[i] = Parent1.Genes[i];
			}
			else
			{
				Child.Genes[i] = Parent2.Genes[i];
			}
		}
		return Child;
	}
	public static void Mutation(DNA Child, float MutationRate)
	{
		for(int i=0;i<Child.Genes.Length;i++)
		{
			float Probability = Random.Range(0, 1f);
			if(Probability<MutationRate)
			{
				Child.Genes[i] = Child.GenerateRandomGene();
			}
		}
	}
}
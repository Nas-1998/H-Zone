using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population {
	public int PopulationSize;
	public int Generation=1;
	public int GeneSize;
	public float MutationRate;
	public DNA[] PopulationPool;
	public DNA BestInPopulation;
	public float AverageFitness;

	public Population(int PopulationSize, int GeneSize, float MutationRate)
	{
		this.PopulationSize = PopulationSize;
		this.MutationRate = MutationRate;
		this.GeneSize = GeneSize;
		this.GenerateInitialPopulation();
	}
	public void GenerateInitialPopulation()
	{
		this.PopulationPool = new DNA[this.PopulationSize];
		for(int i=0;i<this.PopulationSize;i++)
		{
			this.PopulationPool[i] = new DNA(this.GeneSize);
		}
	}
	public void GenerateNewPopulation()
	{
		DNA[] newPool = new DNA[this.PopulationSize];
		for(int i=0;i<this.PopulationSize;i++)
		{
			DNA Parent1 = SelCroMut.Selection(this.PopulationPool);
			DNA Parent2 = SelCroMut.Selection(this.PopulationPool);
			DNA Child = SelCroMut.CrossOver(Parent1, Parent2);
			SelCroMut.Mutation(Child, this.MutationRate);
			newPool[i] = Child;
		}
		this.PopulationPool = newPool;
		this.Generation += 1;
	}
}

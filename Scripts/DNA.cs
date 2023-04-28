using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA {
	public int[] Genes;
	public float FitnessValue;
	public DNA(int ChromosomeSize)
	{
		this.GenerateChromosome(ChromosomeSize);
	}
	public void GenerateChromosome(int ChromosomeSize)
	{
		this.Genes = new int[ChromosomeSize];
		for(int i=0;i<ChromosomeSize;i++)
		{
			this.Genes[i] = this.GenerateRandomGene();
		}
	}
	public int GenerateRandomGene()
	{
		int Choice = Random.Range(0, 2);
		return Choice;
	}
}

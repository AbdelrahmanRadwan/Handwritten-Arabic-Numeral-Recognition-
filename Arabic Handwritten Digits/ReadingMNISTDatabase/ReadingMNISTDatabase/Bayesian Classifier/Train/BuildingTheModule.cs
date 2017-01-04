using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using System.Windows.Forms;

namespace ReadingMNISTDatabase.Bayesian_Classifier.Train
{
    public class BuildingTheModule
    {
        MNIST_Database TrainData;
        public double[] Instants;

        public  int TrainingInstants;
        public int NumberOfFeatures;
        public int NumberOfClasses;
        public int[] NumberOfActiveFeatures;
        public double[,] FeaturesSum;
        public double[,] FeaturesMean;
        public double[, ,] Covariance;
        public double[, ,] CovarianceInverse;
        public double[]       CovarianceDeterminant;
        List<List<List<double>>> Data;

        public BuildingTheModule(MNIST_Database Data)
        {

            NumberOfClasses = 10;
            NumberOfFeatures = 28 * 28;

            this.TrainData = Data;

            TrainingInstants = TrainData.m_pImagePatterns.Count();

            Instants = new double[NumberOfClasses];
            FeaturesSum            = new double [NumberOfClasses, NumberOfFeatures];
            FeaturesMean           = new double [NumberOfClasses, NumberOfFeatures];
            Covariance             = new double [NumberOfClasses, NumberOfFeatures, NumberOfFeatures];
            CovarianceInverse      = new double [NumberOfClasses, NumberOfFeatures, NumberOfFeatures];
            CovarianceDeterminant  = new double [NumberOfClasses];
            NumberOfActiveFeatures = new int    [NumberOfClasses];
            this.Data = new List<List<List<double>>>();
        }
        public void FillData()
        {
            for (int i = 0; i < NumberOfClasses; i++)
                Data.Add(new List<List<double>>());

           for(int i=0;i<TrainingInstants;i++)
           {
               List<double> temp = new List<double>();
               for (int j = 0; j < NumberOfFeatures; j++)
               {
                   if (TrainData.m_pImagePatterns[i].pPattern[j]==255)
                       temp.Add(1.0);
                   else
                       temp.Add(0.0);
               }
               Data[TrainData.m_pImagePatterns[i].nLabel].Add(temp);
           }
        }
        public void FillInstants()
        {
            for (int i = 0; i < TrainingInstants; i++)
                Instants[TrainData.m_pImagePatterns[i].nLabel]++;
        }
        public void FillFeaturesSum()
        {
            for (int i = 0; i < TrainingInstants; i++)
                for (int j = 0; j < NumberOfFeatures; j++)
                {
                    if(TrainData.m_pImagePatterns[i].pPattern[j]==255)
                     FeaturesSum[TrainData.m_pImagePatterns[i].nLabel, j]++;
                }
        }
        public void FillFeaturesMean()
        {
            for (int i = 0; i < NumberOfClasses; i++)
                for (int j = 0; j < NumberOfFeatures; j++)
                {
                    FeaturesMean[i, j] = FeaturesSum[i, j] / Instants[i];
                }
        }  
        public void FillCovariance()
        {
            double sum;
            for(int i=0;i<NumberOfClasses;i++)
            {
                for(int j=0;j<NumberOfFeatures;j++)
                    for(int k=0;k<NumberOfFeatures;k++)
                    {
                        Covariance[i, j, k] = 0;
                        sum=0;
                        if (j==k)
                        {
                            for (int w = 0; w < Instants[i]; w++)
                            {
                                sum = sum + (Data[i][w][k] - FeaturesMean[i, k]) * (Data[i][w][k] - FeaturesMean[i, k]);                                                        
                            }
                            Covariance[i, j, k] = sum / Instants[i];
                           
                        }
                    }
            }

        }
        public void FillCovarianceInverse()
        {
            for(int i=0;i<NumberOfClasses;i++)
            {
                 for(int j=0;j<NumberOfFeatures;j++)
                     for(int k=0;k<NumberOfFeatures;k++)
                     {
                         CovarianceInverse[i, j, k] = 0;
                         if (j == k)
                         {
                             if (Covariance[i, j, k] <= 0)
                                 CovarianceInverse[i, j, k] = 0;
                             else
                                 CovarianceInverse[i, j, k] = 1.0 / Covariance[i, j, k];
                         }
                     }
            }
        }
        public void FillCovarianceDeterminant()
        {
            double mul;
            for(int i=0;i<NumberOfClasses;i++)
            {
                NumberOfActiveFeatures[i] = NumberOfFeatures;
                mul = 1;
                for (int j = 0; j < NumberOfFeatures; j++)
                {
                    if (Covariance[i, j, j] == 0)
                        NumberOfActiveFeatures[i]--;
                    if (Covariance[i, j, j] != 0)
                        mul = mul * Covariance[i, j, j];
                }
                CovarianceDeterminant[i] = mul;
            }        
        }
        public void CreateTheModel()
        {
            FillData();
            FillInstants();
            FillFeaturesSum();
            FillFeaturesMean();
            FillCovariance();
            FillCovarianceInverse();
            FillCovarianceDeterminant();
        }
    }
}
